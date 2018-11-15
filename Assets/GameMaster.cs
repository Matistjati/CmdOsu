using System.Drawing;
using System.Collections.Generic;
using Uncoal.Engine;
using System;
using System.IO;

namespace CmdOsu.Assets
{
	class GameMaster : GameObject
	{
		Sprite hitCircle;
		MapParser mapInfo;
		public GameMaster()
		{
			mapInfo = new MapParser(GetMapPath());

			int width = Console.BufferWidth;
			int height = Console.BufferHeight;

			float xScale = width / 512f;
			float yScale = height / 384f;

			foreach (KeyValuePair<float, CircleInfo> circle in mapInfo.hitObjects)
			{
				circle.Value.position.X *= (int)Math.Round(xScale, 0);
				if (circle.Value.position.X > width)
				{
					circle.Value.position.X = width;
				}

				circle.Value.position.Y *= (int)Math.Round(yScale, 0);
				if (circle.Value.position.Y > height)
				{

				}
			}

			Bitmap circleImage = (Bitmap)Image.FromFile($"{GetSkinPath()}\\hitcircleoverlay.png");

			float circleRadius = (float)(54.4 - 4.48 * mapInfo.circleSize);

			float circleScale = circleRadius / circleImage.Width;

			hitCircle = new Sprite(circleImage, circleScale);


			CircleSpawner circleSpawner = AddComponent<CircleSpawner>();
			circleSpawner.hitCircle = hitCircle;
			circleSpawner.mapInfo = mapInfo;
			circleSpawner.radius = circleRadius;
		}

		string GetSkinPath()
		{
			return "Skins\\Rafis Normal";
		}

		string GetMapPath()
		{
			return Directory.GetFiles(Directory.GetDirectories(@"C:\Users\Matis\source\repos\CmdOsu\bin\Debug\Songs")[0], "*.osu")[0];
		}
	}
}
