using Uncoal.Engine;

namespace CmdOsu.Assets
{
	struct HitInfo
	{
		public readonly float hitTime;
		public readonly float instantiationTime;
		public readonly Coord mousePosition;
		public readonly Coord circlePosition;

		public HitInfo(float hitTime, float instantiationTime, Coord mousePosition, Coord circlePosition)
		{
			this.hitTime = hitTime;
			this.instantiationTime = instantiationTime;
			this.mousePosition = mousePosition;
			this.circlePosition = circlePosition;
		}
	}
}
