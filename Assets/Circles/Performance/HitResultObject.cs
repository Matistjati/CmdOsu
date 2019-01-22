using Uncoal.Engine;
using static Uncoal.Internal.NativeMethods;

namespace CmdOsu.Assets
{
	[IsPrefab]
	class HitResultObject : GameObject
	{
		public static CHAR_INFO[,] perfect;
		public static CHAR_INFO[,] hundred;
		public static CHAR_INFO[,] fifty;
		public static CHAR_INFO[,] miss;


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
