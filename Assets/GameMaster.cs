using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class GameMaster : GameObject
	{
		public GameMaster()
		{
			MapParser mapInfo = new MapParser(GetMapPath());
		}

		string GetMapPath()
		{
			return "620973 simo - Nanairo no Nico Nico Douga\\simo - Nanairo no Nico Nico Douga (maziari1105) [All-Star].osu";
		}
	}
}
