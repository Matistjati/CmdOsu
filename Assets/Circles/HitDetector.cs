using System;
using Uncoal.Engine;

namespace CmdOsu.Assets
{
	class HitDetector : Component
	{
		public delegate void OnHitHandler(HitInfo hitInfo);
		public event OnHitHandler OnHit;

		public float radius;

		void Update()
		{
			if (Input.GetButtonDown(Input.ButtonPress.left) || Input.GetKeyDown('x') || Input.GetKeyDown('y'))
			{
				// Checking if the click is within the area of the circle
				// Check here for the formula https://math.stackexchange.com/questions/198764/how-to-know-if-a-point-is-inside-a-circle
				Coord mousePos = Input.mousePosition;
				CoordF circlePos = gameObject.physicalState.Position;

				double d = Math.Sqrt(
				    (Math.Pow((mousePos.X - circlePos.X), 2) +
					Math.Pow((mousePos.Y - circlePos.Y), 2))
					);

				if (d <= radius)
				{
					OnHit(new HitInfo(GameObject.Time));
				}
			}
		}
	}
}

