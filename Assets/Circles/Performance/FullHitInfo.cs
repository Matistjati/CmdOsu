namespace CmdOsu.Assets
{
	struct FullHitInfo
	{
		public readonly float time;
		public readonly HitType hitType;

		public enum HitType
		{
			Miss,
			Fifty,
			Hundred,
			Perfect
		}

		public FullHitInfo(float Time, HitType hitType)
		{
			time = Time;
			this.hitType = hitType;
		}
	}
}
