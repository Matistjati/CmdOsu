using Uncoal.Engine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace CmdOsu.Assets
{
	class MapParser
	{
		// [General]
		public string audioFilename;
		public int audioLeadIn;
		public int preViewTime;
		public bool countDown;
		public string sampleSet;
		public decimal stackLeniency;
		public int mode;
		public bool letterboxInBreaks;
		public bool widescreenStoryboard;
		// Unused mostly
		//public bool storyFireInFront;
		//public bool specialStyle;
		//public bool epilepsyWarning;
		//public bool useSkinSprites;

		// [Editor]
		public List<int> bookMarks;
		public decimal distanceSpacing;
		public int beatDivisor;
		public int gridSize;
		public int timeLineZoom;

		// [MetaData]
		public string title;
		public string titleUnicode;
		public string artist;
		public string artistUnicode;
		public string creator;
		public string version;
		public string source;
		public string[] tags;
		public int beatMapId;
		public int beatmapSetId;

		// [Difficulty]
		public float hpDrain;
		public float circleSize;
		public float overallDifficulty;
		public float approachRate;
		public float SliderMultiplier;
		public float SliderTickRate;

		// [Events]
		public string backGround;

		// [Colors]
		List<Color> comboColours;

		// [HitObjects]
		Dictionary<float, CircleInfo> hitObjects;

		StreamReader mapStream;
		public MapParser(string mapPath)
		{
			using (mapStream = new StreamReader(mapPath))
			{


				if (mapStream.ReadLine() != "osu file format v14")
				{
					// Inform user of possible error?
				}

				string line;
				while (!((line = mapStream.ReadLine()) is null))
				{
					switch (line)
					{
						// Empty Line
						case "":
							break;

						case "[General]":
							ParseGeneral();
							break;

						case "[Editor]":
							ParseEditor();
							break;

						case "[Metadata]":
							ParseMetaData();
							break;

						case "[Difficulty]":
							ParseDifficulty();
							break;

						case "[Events]":
							ParseEvents();
							break;

						case "[TimingPoints]":
							ParseTimingPoints();
							break;

						case "[Colours]":
							ParseColours();
							break;

						case "[HitObjects]":
							ParseHitObjects();
							break;

						default:
							break;
					}
				}
				Console.Read();
			}

			void ParseGeneral()
			{
				audioFilename = mapStream.ReadLine().Substring("AudioFilename: ".Length);
				audioLeadIn = int.Parse(mapStream.ReadLine().Substring("AudioLeadIn: ".Length));
				preViewTime = int.Parse(mapStream.ReadLine().Substring("PreviewTime: ".Length));
				countDown = mapStream.ReadLine().Substring("Countdown: ".Length) == "1";
				sampleSet = mapStream.ReadLine().Substring("SampleSet: ".Length);
				stackLeniency = decimal.Parse(mapStream.ReadLine().Substring("StackLeniency: ".Length));
				mode = int.Parse(mapStream.ReadLine().Substring("Mode: ".Length));
				letterboxInBreaks = mapStream.ReadLine().Substring("LetterboxInBreaks: ".Length) == "1";
				widescreenStoryboard = mapStream.ReadLine().Substring("WidescreenStoryboard: ".Length) == "1";
			}

			void ParseEditor()
			{
				// Fuck that
			}

			void ParseMetaData()
			{
				title = mapStream.ReadLine().Substring("Title:".Length);
				titleUnicode = mapStream.ReadLine().Substring("TitleUnicode:".Length);
				artist = mapStream.ReadLine().Substring("Artist:".Length);
				artistUnicode = mapStream.ReadLine().Substring("ArtistUnicode:".Length);
				creator = mapStream.ReadLine().Substring("Creator:".Length);
				version = mapStream.ReadLine().Substring("Version:".Length);
				source = mapStream.ReadLine().Substring("Source:".Length);
				tags = mapStream.ReadLine().Substring("Tags:".Length).Split(' ');
				beatMapId = int.Parse(mapStream.ReadLine().Substring("BeatmapID:".Length));
				beatmapSetId = int.Parse(mapStream.ReadLine().Substring("BeatmapSetID:".Length));
			}

			void ParseDifficulty()
			{
				hpDrain = float.Parse(mapStream.ReadLine().Substring("HPDrainRate:".Length));
				circleSize = float.Parse(mapStream.ReadLine().Substring("CircleSize:".Length));
				overallDifficulty = float.Parse(mapStream.ReadLine().Substring("OverallDifficulty:".Length));
				approachRate = float.Parse(mapStream.ReadLine().Substring("ApproachRate:".Length));
				SliderMultiplier = float.Parse(mapStream.ReadLine().Substring("SliderMultiplier:".Length));
				SliderTickRate = float.Parse(mapStream.ReadLine().Substring("SliderTickRate:".Length));
			}

			void ParseEvents()
			{
				// Fuck this too
			}

			void ParseTimingPoints()
			{
				// Fuck this at the moment
			}

			void ParseColours()
			{
				string line;
				// Reading while the string starts with combo
				while ((line = mapStream.ReadLine()).Substring(0, "Combo".Length) == "Combo")
				{
					string[] colorStrings = line.Substring("Combo1 : ".Length).Split(',');
					for (int i = 0; i < colorStrings.Length; i++)
					{
						comboColours.Add(Color.FromArgb(int.Parse(colorStrings[0]),
							int.Parse(colorStrings[1]),
							int.Parse(colorStrings[2])));

					}
				}
			}

			void ParseHitObjects()
			{
				hitObjects = new Dictionary<float, CircleInfo>();
				string line;
				while (!((line = mapStream.ReadLine()) is null))
				{
					string[] hitInfo = line.Split(',');

					hitObjects.Add(
						key: (int.Parse(hitInfo[2]) / 1000f), // Time
						value: new CircleInfo(
							position: new Coord(int.Parse(hitInfo[0]), int.Parse(hitInfo[1])), // Position
							type: byte.Parse(hitInfo[3]), // Circle type
							hitSounds: byte.Parse(hitInfo[4])) // Hitsound
							);
				}
			}
		}
	}
}
