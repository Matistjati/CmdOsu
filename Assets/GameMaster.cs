using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class GameMaster : GameObject
	{
		MapParser mapInfo;
		const int approachImagesPerSecond = 240;


		public GameMaster()
		{
			mapInfo = new MapParser(GetMapPath())
			{
				overallDifficulty = 4
			};

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

			SetUpHitImages(circleScale);

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

			WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();

			player.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
			player.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);
			player.URL = Directory.GetParent(GetMapPath()) + "\\" + mapInfo.audioFilename;

			player.controls.play();

			//a.
			// WAV
			//System.Media.SoundPlayer s = new System.Media.SoundPlayer();
		}

		private void Player_PlayStateChange(int NewState)
		{

		}

		private void Player_MediaError(object pMediaObject)
		{
			
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

		void SetUpHitImages(float scale)
		{
			using (Image perfectHit = Image.FromFile($"{GetSkinPath()}\\hit300.png"))
			{
				int xSize = (int)(perfectHit.Width * scale);
				int ySize = (int)(perfectHit.Height * scale);

				if (xSize <= 0)
					xSize = 1;

				if (ySize <= 0)
					ySize = 1;

				HitResultObject.perfect = BitMapToStringArray(Sprite.ResizeImage(
					perfectHit,
					xSize,
					ySize));
			}

			using (Image hundredHit = Image.FromFile($"{GetSkinPath()}\\hit100.png"))
			{
				int xSize = (int)(hundredHit.Width * scale);
				int ySize = (int)(hundredHit.Height * scale);

				if (xSize <= 0)
					xSize = 1;

				if (ySize <= 0)
					ySize = 1;

				HitResultObject.hundred = BitMapToStringArray(Sprite.ResizeImage(
					hundredHit,
					xSize,
					ySize));
			}

			using (Image fiftyHit = Image.FromFile($"{GetSkinPath()}\\hit50.png"))
			{
				int xSize = (int)(fiftyHit.Width * scale);
				int ySize = (int)(fiftyHit.Height * scale);

				if (xSize <= 0)
					xSize = 1;

				if (ySize <= 0)
					ySize = 1;

				HitResultObject.fifty = BitMapToStringArray(Sprite.ResizeImage(
					fiftyHit,
					xSize,
					ySize));
			}

			using (Image miss = Image.FromFile($"{GetSkinPath()}\\hit0.png"))
			{
				int xSize = (int)(miss.Width * scale);
				int ySize = (int)(miss.Height * scale);

				if (xSize <= 0)
					xSize = 1;

				if (ySize <= 0)
					ySize = 1;

				HitResultObject.miss = BitMapToStringArray(Sprite.ResizeImage(
					miss,
					xSize,
					ySize));
			}
		}

		unsafe string[,] BitMapToStringArray(Bitmap bitmap)
		{
			string[,] result = new string[bitmap.Width, bitmap.Height];

			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

			int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
			int heightInPixels = bitmapData.Height;
			int widthInBytes = bitmapData.Width * bytesPerPixel;
			byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

			for (int y = 0; y < heightInPixels; y++)
			{
				byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
				for (int x = 0; x < widthInBytes; x += bytesPerPixel)
				{
					int blue = currentLine[x];
					int green = currentLine[x + 1];
					int red = currentLine[x + 2];


					if (blue == 0 && green == 0 && red == 0) //|| rgb.A < 10)
					{
						result[x / bytesPerPixel, y] = whiteSpace;
					}
					else
					{
						colorStringBuilder.Append(escapeStartRGB);
						colorStringBuilder.Append(red);
						colorStringBuilder.Append(colorSeparator);
						colorStringBuilder.Append(green);
						colorStringBuilder.Append(colorSeparator);
						colorStringBuilder.Append(blue);
						colorStringBuilder.Append(escapeEnd);

						result[x / bytesPerPixel, y] = colorStringBuilder.ToString();
						colorStringBuilder.Clear();
					}
				}
			}
			bitmap.UnlockBits(bitmapData);

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
