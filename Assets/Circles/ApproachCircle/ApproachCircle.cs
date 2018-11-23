using Uncoal.Engine;

namespace CmdOsu.Assets
{
	[IsPrefab]
	class ApproachCircle : GameObject
	{
		public delegate void OnMissHanlder(HitInfo hitInfo);
		public event OnMissHanlder OnMiss;

		public void PerformOnMiss(HitInfo miss) => OnMiss?.Invoke(miss);

		public ApproachCircle()
		{
			ApproachCircleResizer resizer = AddComponent<ApproachCircleResizer>();
			resizer.instantiationTime = Time;

			SpriteDisplayer sprite = AddComponent<SpriteDisplayer>();
			sprite.SetImage(ApproachCircleResizer.approachSizes[0]);
		}
	}
}
