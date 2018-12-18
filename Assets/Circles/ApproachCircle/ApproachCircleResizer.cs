using System.Collections.Generic;
using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class ApproachCircleResizer : Component
	{
		public void OnHit(HitInfo ignore)
		{
			GameObject.Destroy(this.gameObject);
		}

		public static float lifeTime;
		public static List<string[,]> approachSizes;
		public static int safeApproachSizesCount;
		public float instantiationTime;
		private float deathTime;
		int oldIndex = -1;

		private Sprite sprite;

		void Start()
		{
			sprite = gameObject.GetComponent<SpriteDisplayer>().Sprite;
			deathTime = instantiationTime + lifeTime;
		}

		void Update()
		{
			float percentLifeTimePassed = ((GameObject.Time - instantiationTime) / lifeTime);
			if (percentLifeTimePassed > 1)
				percentLifeTimePassed = 1;

			int index = (int)(percentLifeTimePassed * safeApproachSizesCount);

			if (index != oldIndex)
			{
				GameObject.SetSpriteSafe(sprite, approachSizes[index]);
				oldIndex = index;
			}

			if (GameObject.Time > deathTime)
			{
				((ApproachCircle)gameObject).PerformOnMiss(new HitInfo(
					instantiationTime: instantiationTime,
					hitTime: GameObject.Time,
					mousePosition: Input.mousePosition,
					circlePosition: (Coord)this.physicalState.Position));

				GameObject.Destroy(this.gameObject);
			}
		}
	}
}

