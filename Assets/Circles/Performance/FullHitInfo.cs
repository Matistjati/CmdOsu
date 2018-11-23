using Uncoal.Engine;

namespace CmdOsu.Assets
{
	struct FullHitInfo
	{
		public readonly float instantiationTime;
		public readonly float hitTime;
		public readonly HitType hitType;
		public readonly Coord position;

		public enum HitType
		{
			Miss,
			Fifty,
			Hundred,
			Perfect
		}

		public FullHitInfo(float hitTime, float instantiationTime, Coord position, HitType hitType)
		{
			this.instantiationTime = instantiationTime;
			this.hitTime = hitTime;
			this.hitType = hitType;
			this.position = position;
		}
	}
}
