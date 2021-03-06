﻿using System;
using System.Configuration;

namespace CmdOsu
{
	class Program
	{
		static void Main()
		{
			// Parsing fontisze
			string[] fontSize;
			try
			{
				fontSize = ConfigurationManager.AppSettings["Console Font Size"].Split(';');
			}
			catch (NullReferenceException exception)
			{
				throw new NullReferenceException("Whomst've removed my Console Font Size key in app.config", exception);
			}

			ushort fontSizeX;
			ushort fontSizeY;
			try
			{
				fontSizeX = ushort.Parse(fontSize[0]); // X
				fontSizeY = ushort.Parse(fontSize[1]); // Y
			}
			catch (FormatException exception)
			{
				throw new FormatException($"X or Y in App.config/Console Font Size was an invalid number." +
					$" X was \"{fontSize[0]}\", y was \"{fontSize[1]}\"", exception);
			}


			// Parsing game name
			string gameName;
			try
			{
				gameName = ConfigurationManager.AppSettings["Game Name"];
			}
			catch (NullReferenceException exception)
			{
				throw new NullReferenceException("Whomst've removed my Game Name key in app.config", exception);
			}

			Uncoal.Runner.Program.Start(fontSizeX, fontSizeY, gameName);
		}
	}
}
