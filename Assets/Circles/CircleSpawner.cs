using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class CircleSpawner : Component
	{
		public MapParser mapInfo;
		public string[,] hitCircle;
		public List<string[,]> approachCircleSizes;

		public float hitRadius;

		Queue<float> hitObjectsToRemove = new Queue<float>();
		List<FullHitInfo> hitInfos = new List<FullHitInfo>();

		float lifeTime;
		float hitWindow300;
		float hitWindow100;
		float hitWindow50;

		GameObject.ObjectActivator<HitCircle> circleActivator;
		GameObject.ObjectActivator<ApproachCircle> approachActivator;

		void Start()
		{
			ConstructorInfo circleCtor = typeof(HitCircle).GetConstructor(new Type[0]);
			ConstructorInfo approachCtor = typeof(ApproachCircle).GetConstructor(new Type[0]);

			circleActivator = GameObject.GetActivator<HitCircle>(circleCtor);
			approachActivator = GameObject.GetActivator<ApproachCircle>(approachCtor);

			hitWindow300 = DifficultyCalc.GetHitWindow300(mapInfo.overallDifficulty);
			hitWindow100 = DifficultyCalc.GetHitWindow100(mapInfo.overallDifficulty);
			hitWindow50 = DifficultyCalc.GetHitWindow50(mapInfo.overallDifficulty);
			// more Leniency
			//hitWindow300 += 0.05f;
			//hitWindow100 += 0.075f;
			//hitWindow50 += 0.1f;


			lifeTime = DifficultyCalc.GetObjectLifeTime(mapInfo.approachRate);

			ApproachCircleResizer.lifeTime = lifeTime;
			ApproachCircleResizer.approachSizes = approachCircleSizes;
			ApproachCircleResizer.safeApproachSizesCount = approachCircleSizes.Count - 1;

			HitDetector.radius = hitRadius;
			HitDetector.radiusSquared = hitRadius * hitRadius;
			HitDetector.spriteMap = hitCircle;

			HitResultDestroyer.lifeTime = 1f;
		}

		void OnMiss(HitInfo hitInfo)
		{
			hitInfos.Add(new FullHitInfo(hitInfo.hitTime, hitInfo.instantiationTime, hitInfo.mousePosition, hitInfo.circlePosition, FullHitInfo.HitType.Miss));
			GameObject.Instantiate<HitResultObject>(hitInfo.circlePosition, FullHitInfo.HitType.Miss);
		}

		void OnHit(HitInfo hitInfo)
		{
			// OD calculations
			FullHitInfo.HitType hitType;

			float timeDiff = Math.Abs(hitInfo.instantiationTime + lifeTime - hitInfo.hitTime);

			if (timeDiff < hitWindow300)
			{
				hitType = FullHitInfo.HitType.Perfect;
			}
			else if (timeDiff < hitWindow100)
			{
				hitType = FullHitInfo.HitType.Hundred;
			}
			else if (timeDiff < hitWindow50)
			{
				hitType = FullHitInfo.HitType.Fifty;
			}
			else
			{
				hitType = FullHitInfo.HitType.Miss;
			}
			hitInfos.Add(new FullHitInfo(hitInfo.hitTime, hitInfo.instantiationTime, hitInfo.mousePosition, hitInfo.circlePosition, hitType));
			GameObject.Instantiate<HitResultObject>(hitInfo.circlePosition, hitType);
		}

		void Update()
		{
			foreach (KeyValuePair<float, CircleInfo> key in mapInfo.hitObjects)
			{
				if (GameObject.Time > key.Key)
				{
					HitCircle.OnHitHandler onHit = new HitCircle.OnHitHandler(OnHit);
					ApproachCircle.OnMissHanlder onMiss = new ApproachCircle.OnMissHanlder(OnMiss);

					hitObjectsToRemove.Enqueue(key.Key);
					HitCircle hitObject = (HitCircle)GameObject.InstantiateActivator<HitCircle>(key.Value.position, circleActivator);

					ApproachCircle approachObject = (ApproachCircle)GameObject.InstantiateActivator<ApproachCircle>(key.Value.position, approachActivator);

					onHit += approachObject.GetComponent<ApproachCircleResizer>().OnHit;
					onMiss += hitObject.GetComponent<HitDetector>().OnMiss;

					hitObject.OnHit += onHit;
					approachObject.OnMiss += onMiss;
				}
				else // The values are laid out sequentially, so it's safe to break when the first one "fails"
				{
					break;
				}
			}


			while (hitObjectsToRemove.Count != 0)
			{
				mapInfo.hitObjects.Remove(hitObjectsToRemove.Dequeue());
			}
		}
	}
}

