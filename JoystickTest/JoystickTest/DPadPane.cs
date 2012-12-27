using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JoystickTest
{
	class DPadPane
	{
		public const int Padding = 25;

		public bool Up { get; private set; }
		public bool Down { get; private set; }
		public bool Left { get; private set; }
		public bool Right { get; private set; }

		private Vector2 boxTopLeft;

		private Rectangle upBounds, downBounds, leftBounds, rightBounds;

		public DPadPane(Vector2 boxTopLeft)
		{
			this.boxTopLeft = boxTopLeft;

			Up = Down = Left = Right = false;

			upBounds = new Rectangle((int)boxTopLeft.X + Padding, (int)boxTopLeft.Y, Padding, Padding);
			downBounds = new Rectangle((int)boxTopLeft.X + Padding, (int)boxTopLeft.Y + 2 * Padding, Padding, Padding);
			leftBounds = new Rectangle((int)boxTopLeft.X, (int)boxTopLeft.Y + Padding, Padding, Padding);
			rightBounds = new Rectangle((int)boxTopLeft.X + 2 * Padding, (int)boxTopLeft.Y + Padding, Padding, Padding);
		}

		public void Update(ButtonState up, ButtonState down, ButtonState left, ButtonState right)
		{
			this.Up = up == ButtonState.Pressed;
			this.Down = down == ButtonState.Pressed;
			this.Left = left == ButtonState.Pressed;
			this.Right = right == ButtonState.Pressed;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TestGame.Pixel, upBounds, Up ? Color.Red : Color.Black);
			spriteBatch.Draw(TestGame.Pixel, downBounds, Down ? Color.Red : Color.Black);
			spriteBatch.Draw(TestGame.Pixel, leftBounds, Left ? Color.Red : Color.Black);
			spriteBatch.Draw(TestGame.Pixel, rightBounds, Right ? Color.Red : Color.Black);
		}
	}
}
