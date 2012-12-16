using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JoystickTest
{
	class JoystickPane
	{
		public const int BoxSize = 500;
		public const int BoxPadding = 50;
		public const int LineWeight = 3;

		private Vector2 joystickValue;
		private Point joystickRelativePosition;
		private bool[,] paint;

		private Vector2 boxTopLeft;
		private Vector2 boxCenter;

		public JoystickPane(Vector2 boxTopLeft)
		{
			this.boxTopLeft = boxTopLeft;
			this.boxCenter = boxTopLeft + new Vector2(BoxSize) / 2f;

			ClearBox();
		}

		public void ClearBox()
		{
			paint = new bool[BoxSize + 1, BoxSize + 1];
		}

		public void Update(Vector2 value)
		{
			joystickValue = value;
			joystickRelativePosition = new Point((int)(BoxSize / 2f + joystickValue.X * BoxSize / 2f), (int)(BoxSize / 2f - joystickValue.Y * BoxSize / 2f));
			paint[joystickRelativePosition.X, joystickRelativePosition.Y] = true;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(TestGame.Font, "X: " + (joystickValue.X < 0 ? "" : " ") + joystickValue.X, boxTopLeft - new Vector2(0, 2 * BoxPadding), Color.Black);
			spriteBatch.DrawString(TestGame.Font, "Y: " + (joystickValue.Y < 0 ? "" : " ") + joystickValue.Y, boxTopLeft - new Vector2(0, BoxPadding), Color.Black);

			for (int i = 0; i < paint.GetLength(0); i++)
				for (int j = 0; j < paint.GetLength(1); j++)
					if (paint[i, j])
						spriteBatch.Draw(TestGame.Pixel, new Rectangle((int)(boxTopLeft.X + i) - 1, (int)(boxTopLeft.Y + j) - 1, LineWeight, LineWeight), Color.Red);

			spriteBatch.Draw(TestGame.Pixel, new Rectangle((int)boxTopLeft.X, (int)boxTopLeft.Y, BoxSize, LineWeight), Color.Black);
			spriteBatch.Draw(TestGame.Pixel, new Rectangle((int)boxTopLeft.X, (int)boxTopLeft.Y, LineWeight, BoxSize), Color.Black);
			spriteBatch.Draw(TestGame.Pixel, new Rectangle((int)boxTopLeft.X, (int)(boxTopLeft.Y + BoxSize), BoxSize, LineWeight), Color.Black);
			spriteBatch.Draw(TestGame.Pixel, new Rectangle((int)(boxTopLeft.X + BoxSize), (int)boxTopLeft.Y, LineWeight, BoxSize), Color.Black);

			spriteBatch.Draw(TestGame.Pixel, new Rectangle((int)(boxTopLeft.X + joystickRelativePosition.X), (int)(boxTopLeft.Y + joystickRelativePosition.Y), 2 * LineWeight, 2 * LineWeight), Color.Green);

		}
	}
}
