using System.Collections.Generic;
using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class CircleSpawner : Component
	{
		public MapParser mapInfo;
		public Sprite hitCircle;
		public float radius;

		Queue<float> hitObjectsToRemove = new Queue<float>();

		void Update()
		{
			foreach (KeyValuePair<float, CircleInfo> key in mapInfo.hitObjects)
			{
				if (GameObject.Time > key.Key)
				{
					hitObjectsToRemove.Enqueue(key.Key);
					GameObject.Instantiate<HitCircle>(key.Value.position, radius, hitCircle.colorValues);
				}
				else // The values are laid out sequentially, so it's safe to break when the first fails
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

