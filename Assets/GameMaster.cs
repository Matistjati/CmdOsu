using System.Drawing;
using System.Collections.Generic;
using Uncoal.Engine;
using System;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;

namespace CmdOsu.Assets
{
	class GameMaster : GameObject
	{
		MapParser mapInfo;
		const int approachImagesPerSecond = 144;


		public GameMaster()
		{
			mapInfo = new MapParser(GetMapPath());

			int consoleWidth = Console.BufferWidth;
			int consoleHeight = Console.BufferHeight;

			float xScale = consoleWidth / 512f;
			float yScale = consoleHeight / 384f;

			foreach (KeyValuePair<float, CircleInfo> circle in mapInfo.hitObjects)
			{
				circle.Value.position.X *= (int)Math.Round(xScale, 0);

				circle.Value.position.Y *= (int)Math.Round(yScale, 0);
			}

			Bitmap circleImage = (Bitmap)Image.FromFile($"{GetSkinPath()}\\hitcircleoverlay.png");

			float circleRadius = DifficultyCalc.GetCircleArea(mapInfo.circleSize);

			float circleScale = circleRadius / circleImage.Width;


			// The circle doesn't begin perfectly at the file edge
			// Scale calculation?
			//circleRadius -= 20;

			float approachCircleScale = circleScale * 3;

			List<string[,]> approachCircleSizes = new List<string[,]>();

			Bitmap approachImage = (Bitmap)Image.FromFile($"{GetSkinPath()}\\approachcircle.png");

			float circleLifeTime = DifficultyCalc.GetObjectLifeTime(mapInfo.approachRate);
			int imageAmount = Convert.ToInt32(circleLifeTime * approachImagesPerSecond);

			for (float i = imageAmount - 1; i >= 0; i--)
			{
				float newScale = Lerp(circleScale, approachCircleScale, (i / imageAmount));

				approachCircleSizes.Add(BitMapToStringArray(Sprite.ResizeImage(
					image: approachImage,
					width: (int)(approachImage.Width * newScale),
					height: (int)(approachImage.Height * newScale))));
			}


			CircleSpawner circleSpawner = AddComponent<CircleSpawner>();

			circleSpawner.approachCircleSizes = approachCircleSizes;
			circleSpawner.hitCircle = new Sprite(circleImage, circleScale).colorValues;
			circleSpawner.mapInfo = mapInfo;
			circleSpawner.hitRadius = circleRadius;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		float Lerp(float lower, float higher, float amount)
		{
			return lower * (1 - amount) + (higher * amount);
		}

		StringBuilder colorStringBuilder = new StringBuilder(24);

		const string whiteSpace = " ";
		const string escapeStartRGB = "\x1b[38;2;";
		const string escapeEnd = "m█";
		const char colorSeparator = ';';

		string[,] BitMapToStringArray(Bitmap bitmap)
		{
			Coord bitmapSize = new Coord(bitmap.Width, bitmap.Height);

			string[,] result = new string[bitmapSize.X, bitmapSize.Y];

			for (int x = 0; x < result.GetLength(0); x++)
			{
				for (int y = 0; y < result.GetLength(1); y++)
				{
					Color rgb = bitmap.GetPixel(x, y);

					if (rgb.R == 0 && rgb.G == 0 && rgb.B == 0) //|| rgb.A < 10)
					{
						result[x, y] = whiteSpace;
					}
					else
					{
						colorStringBuilder.Append(escapeStartRGB);
						colorStringBuilder.Append(rgb.R);
						colorStringBuilder.Append(colorSeparator);
						colorStringBuilder.Append(rgb.G);
						colorStringBuilder.Append(colorSeparator);
						colorStringBuilder.Append(rgb.B);
						colorStringBuilder.Append(escapeEnd);

						result[x, y] = colorStringBuilder.ToString();
						colorStringBuilder.Clear();
					}
				}
			}

			bitmap.Dispose();

			return result;
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
