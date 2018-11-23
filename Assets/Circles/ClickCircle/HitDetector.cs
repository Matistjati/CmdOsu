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

			if (Input.GetButtonDown(Input.ButtonPress.left) || Input.GetKeyDown('x') || Input.GetKeyDown('y'))
			{
				// Checking if the click is within the area of the circle
				// Check here for the formula https://math.stackexchange.com/questions/198764/how-to-know-if-a-point-is-inside-a-circle
				Coord mousePos = Input.mousePosition;
				CoordF circlePos = gameObject.physicalState.Position;

				double d = Math.Sqrt(
				    Math.Pow(mousePos.X - circlePos.X, 2) +
					Math.Pow(mousePos.Y - circlePos.Y, 2));

				if (d <= radius)
				{
					((HitCircle)gameObject).PerformOnHit(new HitInfo(
						instantiationTime: instantiationTime,
						hitTime: GameObject.Time,
						position: Input.mousePosition));

					GameObject.Destroy(gameObject);
				}
			}
		}

		static Coord RotatePoint(Coord centerPoint, Coord pointToRotate, double angleInDegrees)
		{
			double angleInRadians = angleInDegrees * (Math.PI / 180);
			double cosTheta = Math.Cos(angleInRadians);
			double sinTheta = Math.Sin(angleInRadians);
			return new Coord
			{
				X =
					(int)
					(cosTheta * (pointToRotate.X - centerPoint.X) -
					sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
				Y =
					(int)
					(sinTheta * (pointToRotate.X - centerPoint.X) +
					cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
			};
		}
	}
}

