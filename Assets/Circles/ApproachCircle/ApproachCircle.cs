using System.Drawing;
using Uncoal.Engine;

namespace CmdOsu.Assets
{
	[IsPrefab]
	class ApproachCircle : GameObject
	{
		public delegate void OnMissHanlder(HitInfo hitInfo);
		public event OnMissHanlder OnMiss;

		public void PerformOnMiss(HitInfo miss) => OnMiss?.Invoke(miss);

		public ApproachCircle(float hitRadius, float approachRadius, int hitSize, Bitmap spriteMap)
		{
			ApproachCircleResizer resizer = AddComponent<ApproachCircleResizer>();
			resizer.hitRadius = hitRadius;
			resizer.hitSize = hitSize;

			SpriteDisplayer sprite = AddComponent<SpriteDisplayer>();
			sprite.SetImage(spriteMap);
		}
	}
}
