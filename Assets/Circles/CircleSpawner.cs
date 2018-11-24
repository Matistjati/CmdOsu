using System;
using System.Collections.Generic;
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

		void Start()
		{
			hitWindow300 = DifficultyCalc.GetHitWindow300(mapInfo.overallDifficulty);
			hitWindow100 = DifficultyCalc.GetHitWindow100(mapInfo.overallDifficulty);
			hitWindow50 = DifficultyCalc.GetHitWindow50(mapInfo.overallDifficulty);

			lifeTime = DifficultyCalc.GetObjectLifeTime(mapInfo.approachRate);
			ApproachCircleResizer.lifeTime = lifeTime;
			ApproachCircleResizer.approachSizes = approachCircleSizes;
			ApproachCircleResizer.safeApproachSizesCount = approachCircleSizes.Count - 1;

			HitDetector.radius = hitRadius;
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
					HitCircle hitObject = (HitCircle)GameObject.Instantiate<HitCircle>(key.Value.position);

					ApproachCircle approachObject = (ApproachCircle)GameObject.Instantiate<ApproachCircle>(key.Value.position);

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

