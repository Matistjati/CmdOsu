using Uncoal.Engine;

namespace CmdOsu.Assets
{
	[IsPrefab]
	class HitCircle : GameObject
	{
		public HitCircle(float radius)
		{
			HitDetector hitDetector = AddComponent<HitDetector>();
			hitDetector.radius = radius;
		}
	}
}
