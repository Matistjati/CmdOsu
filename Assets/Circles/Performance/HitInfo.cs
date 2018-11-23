using Uncoal.Engine;

namespace CmdOsu.Assets
{
	struct HitInfo
	{
		public readonly float hitTime;
		public readonly float instantiationTime;
		public readonly Coord position;

		public HitInfo(float hitTime, float instantiationTime, Coord position)
		{
			this.hitTime = hitTime;
			this.instantiationTime = instantiationTime;
			this.position = position;
		}
	}
}
