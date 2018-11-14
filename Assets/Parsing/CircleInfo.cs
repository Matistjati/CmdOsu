using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	}
}
