using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class CircleSpawner : Component
	{
		public	MapParser mapInfo;
		public Sprite hitCircle;

		bool instatiate = true;
		void Update()
		{
			if (instatiate)
			{
				GameObject.Instantiate<HitCircle>(new Coord(300, 100), new object[] { hitCircle.Size.X / 2f, hitCircle.colorValues });

				GameObject.Instantiate<HitCircle>(new Coord(310, 100), new object[] { hitCircle.Size.X / 2f, hitCircle.colorValues });
				GameObject.Instantiate<HitCircle>(new Coord(325, 100), new object[] { hitCircle.Size.X / 2f, hitCircle.colorValues });
				GameObject.Instantiate<HitCircle>(new Coord(350, 100), new object[] { hitCircle.Size.X / 2f, hitCircle.colorValues });

				GameObject.Instantiate<HitCircle>(new Coord(310, 110), new object[] { hitCircle.Size.X / 2f, hitCircle.colorValues });
				GameObject.Instantiate<HitCircle>(new Coord(320, 120), new object[] { hitCircle.Size.X / 2f, hitCircle.colorValues });
				GameObject.Instantiate<HitCircle>(new Coord(340, 140), new object[] { hitCircle.Size.X / 2f, hitCircle.colorValues });
				instatiate = false;
			}
		}
	}
}

