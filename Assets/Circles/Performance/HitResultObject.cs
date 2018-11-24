using Uncoal.Engine;

namespace CmdOsu.Assets
{
	[IsPrefab]
	class HitResultObject : GameObject
	{
		public static string[,] perfect;
		public static string[,] hundred;
		public static string[,] fifty;
		public static string[,] miss;


		public HitResultObject(FullHitInfo.HitType hitType)
		{
			HitResultDestroyer resultDestroyer = AddComponent<HitResultDestroyer>();
			resultDestroyer.instantiationTime = Time;

			SpriteDisplayer sprite = AddComponent<SpriteDisplayer>();

			switch (hitType)
			{
				case FullHitInfo.HitType.Miss:
					sprite.SetImage(miss);
					break;
				case FullHitInfo.HitType.Fifty:
					sprite.SetImage(fifty);
					break;
				case FullHitInfo.HitType.Hundred:
					sprite.SetImage(hundred);
					break;
				case FullHitInfo.HitType.Perfect:
					sprite.SetImage(perfect);
					break;
			}
		}
	}
}
