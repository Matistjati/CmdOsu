using System.Drawing;
using Uncoal.Engine;

namespace CmdOsu.Assets
{
	[IsPrefab]
	class HitCircle : GameObject
	{
		public HitCircle(float radius, string[,] spriteMap)
		{
			HitDetector hitDetector = AddComponent<HitDetector>();
			hitDetector.radius = radius;
			SpriteDisplayer sprite = AddComponent<SpriteDisplayer>();
			sprite.SetImage(spriteMap);
		}
	}
}
