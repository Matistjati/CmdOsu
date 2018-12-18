using System;
using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class HitDetector : Component
	{
		public void OnMiss(HitInfo ignore)
		{
			GameObject.Destroy(this.gameObject);
		}

		public static string[,] spriteMap;
		public static float radius;
		public static float radiusSquared;
		public float instantiationTime;

		//static Coord middle = new Coord(300, 300);
		//int rotation = 0;

		void Update()
		{
			//physicalState.Position = (CoordF)RotatePoint(middle, (Coord)physicalState.Position, rotation * GameObject.TimeDelta);
			//rotation += 30;
			//if (rotation == 360)
			//{
			//	rotation = 0;
			//}

			if (Input.leftMouseButtonPressed || Input.GetKeyDown('x') || Input.GetKeyDown('z'))
			{
				// Checking if the click is within the area of the circle
				// Check here for the formula https://math.stackexchange.com/questions/198764/how-to-know-if-a-point-is-inside-a-circle
				Coord mousePos = Input.mousePosition;
				CoordF circlePos = gameObject.physicalState.Position;

				float xDifference = mousePos.X - circlePos.X;
				float yDifference = mousePos.Y - circlePos.Y;

				double distance = 
					(xDifference * xDifference) +
					(yDifference * yDifference);

				if (distance <= radiusSquared)
				{
					((HitCircle)gameObject).PerformOnHit(new HitInfo(
						instantiationTime: instantiationTime,
						hitTime: GameObject.Time,
						mousePosition: Input.mousePosition,
						circlePosition: (Coord)this.physicalState.Position));

					GameObject.Destroy(gameObject);
				}
			}
		}
	}
}

