using System.Drawing;
using System.Collections.Generic;
using Uncoal.Engine;
using System;
using System.IO;

namespace CmdOsu.Assets
{
	class GameMaster : GameObject
	{
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

				circle.Value.position.Y *= (int)Math.Round(yScale, 0);
			}

			Bitmap circleImage = (Bitmap)Image.FromFile($"{GetSkinPath()}\\hitcircleoverlay.png");

			Bitmap approachImage = (Bitmap)Image.FromFile($"{GetSkinPath()}\\approachcircle.png");


			float circleRadius = (float)(54.4 - 4.48 * mapInfo.circleSize);

			float circleScale = circleRadius / circleImage.Width;


			// The circle doesn't begin perfectly at the file edge
			// Scale calculation?
			circleRadius -= 20;

			float approachCircleScale = circleScale * 3;
			float approachCircleRadius = circleRadius * 3;

			CircleSpawner circleSpawner = AddComponent<CircleSpawner>();
			circleSpawner.approachCircle = Sprite.ResizeImage(approachImage, (int)(approachImage.Width * approachCircleScale), (int)(approachImage.Height * approachCircleScale));
			circleSpawner.approachRadius = approachCircleRadius;
			circleSpawner.hitCircle = new Sprite(circleImage, circleScale).colorValues;
			circleSpawner.mapInfo = mapInfo;
			circleSpawner.hitRadius = circleRadius;
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
