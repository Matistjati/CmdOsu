using Uncoal.Engine;

namespace CmdOsu.Assets
{
	struct FullHitInfo
	{
		public readonly float instantiationTime;
		public readonly float hitTime;
		public readonly HitType hitType;
		public readonly Coord mousePosition;
		public readonly Coord circlePosition;

		public enum HitType
		{
			Miss,
			Fifty,
			Hundred,
			Perfect
		}

		public FullHitInfo(float hitTime, float instantiationTime, Coord mousePosition, Coord circlePosition, HitType hitType)
		{
			this.instantiationTime = instantiationTime;
			this.hitTime = hitTime;
			this.hitType = hitType;
			this.mousePosition = mousePosition;
			this.circlePosition = circlePosition;
		}
	}
}
