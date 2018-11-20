using Uncoal.Engine;

namespace CmdOsu.Assets
{
	[IsPrefab]
	class HitCircle : GameObject
	{
		public delegate void OnHitHandler(HitInfo hitInfo);
		public event OnHitHandler OnHit;

		public void PerformOnHit(HitInfo hit) => OnHit?.Invoke(hit);

		public HitCircle(float hitRadius, float instantiationTime, string[,] spriteMap)
		{
			HitDetector hitDetector = AddComponent<HitDetector>();
			hitDetector.radius = hitRadius;
			hitDetector.instantiationTime = instantiationTime;

			SpriteDisplayer sprite = AddComponent<SpriteDisplayer>();
			sprite.SetImage(spriteMap);
		}
	}
}
