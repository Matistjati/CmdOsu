using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class HitResultDestroyer : Component
	{
		public static float lifeTime;
		public float instantiationTime;
		float deathTime;

		void Start()
		{
			deathTime = lifeTime + instantiationTime;
		}

		void Update()
		{
			if (GameObject.Time > deathTime)
			{
				GameObject.Destroy(this.gameObject);
			}
		}
	}
}

