using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class ApproachCircleResizer : Component
	{
		public void OnHit(HitInfo ignore)
		{
			GameObject.Destroy(this.gameObject);
		}

		public float hitRadius;
		public float lifeTime = 600;
		float changeRate;

		public int hitSize;


		SpriteDisplayer sprite;

		void Start()
		{
			sprite = gameObject.GetComponent<SpriteDisplayer>();
			if (true)
			{
				changeRate = 1f;
			}
		}

		void Update()
		{
			this.physicalState.Scale -= changeRate * GameObject.TimeDelta;

			if (sprite.Width < hitSize)
			{
				((ApproachCircle)gameObject).PerformOnMiss(new HitInfo(GameObject.Time));
				GameObject.Destroy(this.gameObject);
			}
		}
	}
}

