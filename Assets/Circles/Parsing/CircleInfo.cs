using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class CircleInfo
	{
		public Coord position;
		public byte type;
		public byte hitSounds;

		// Extras?
		/*
		public int 
		*/

		public CircleInfo(Coord position, byte type, byte hitSounds)
		{
			this.position = position;
			this.type = type;
			this.hitSounds = hitSounds;
		}

		public override string ToString()
		{
			return $"X: {position.X} Y: {position.Y} Type: {type}";
		}
	}
}
