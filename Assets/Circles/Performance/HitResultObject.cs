using System.Text;
using Uncoal.Engine;

namespace CmdOsu.Assets
{
	[IsPrefab]
	class HitResultObject : GameObject
	{
		public static StringBuilder[,] perfect;
		public static StringBuilder[,] hundred;
		public static StringBuilder[,] fifty;
		public static StringBuilder[,] miss;


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
