using System.Drawing;
using System.Collections.Generic;
using Uncoal.Engine;
using System;

namespace CmdOsu.Assets
{
	class GameMaster : GameObject
	{
		Sprite hitCircle;
		MapParser mapInfo;
		public GameMaster()
		{
			mapInfo = new MapParser(GetMapPath());

			float xScale = Console.BufferWidth / 512f;
			float yScale = Console.BufferHeight / 384f;

			foreach (KeyValuePair<float, CircleInfo> circle in mapInfo.hitObjects)
			{
				circle.Value.position.X *= (int)Math.Round(xScale, 0);
				circle.Value.position.Y *= (int)Math.Round(yScale, 0);
			}

			Bitmap circleImage = (Bitmap)Image.FromFile($"{GetSkinPath()}\\hitcircleoverlay.png");

			float circleRadius = (float)(54.4 - 4.48 * mapInfo.circleSize);
			circleRadius = circleRadius / circleImage.Width;

			hitCircle = new Sprite(circleImage, circleRadius);


			CircleSpawner circleSpawner = AddComponent<CircleSpawner>();
			circleSpawner.hitCircle = hitCircle;
		}

		string GetSkinPath()
		{
			return "Skins\\Rafis Normal";
		}

		string GetMapPath()
		{
			return "620973 simo - Nanairo no Nico Nico Douga\\simo - Nanairo no Nico Nico Douga (maziari1105) [All-Star].osu";
		}
	}
}
