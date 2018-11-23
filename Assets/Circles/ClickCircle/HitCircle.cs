using Uncoal.Engine;

namespace CmdOsu.Assets
{
	[IsPrefab]
	class HitCircle : GameObject
	{
		public delegate void OnHitHandler(HitInfo hitInfo);
		public event OnHitHandler OnHit;

		public void PerformOnHit(HitInfo hit) => OnHit?.Invoke(hit);

		public HitCircle()
		{
			HitDetector hitDetector = AddComponent<HitDetector>();
			hitDetector.instantiationTime = Time;

			SpriteDisplayer sprite = AddComponent<SpriteDisplayer>();
			sprite.SetImage(HitDetector.spriteMap);
		}
	}
}
