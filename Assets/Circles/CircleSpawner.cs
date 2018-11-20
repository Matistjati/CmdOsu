using System.Collections.Generic;
using System.Drawing;
using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class CircleSpawner : Component
	{
		public MapParser mapInfo;
		public string[,] hitCircle;
		public Bitmap approachCircle;

		public float hitRadius;
		public float approachRadius;

		Queue<float> hitObjectsToRemove = new Queue<float>();
		readonly List<FullHitInfo> hitInfos = new List<FullHitInfo>();

		void OnMiss(HitInfo hitInfo)
		{

		}

		void OnHit(HitInfo hitInfo)
		{

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
					HitCircle hitObject = (HitCircle)GameObject.Instantiate<HitCircle>(key.Value.position, hitRadius, key.Key, hitCircle);

					ApproachCircle approachObject = (ApproachCircle)GameObject.Instantiate<ApproachCircle>(key.Value.position,
					   /* No, hitRadius here isn't a bug*/ hitRadius, approachRadius, hitCircle.GetLength(0), approachCircle);

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

