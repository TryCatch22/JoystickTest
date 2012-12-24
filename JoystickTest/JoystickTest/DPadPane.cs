using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JoystickTest
{
	class DPadPane
	{
		public const int Padding = 25;

		private Vector2 boxTopLeft;

		private bool up, down, left, right;

		private Rectangle upBounds, downBounds, leftBounds, rightBounds;

		public DPadPane(Vector2 boxTopLeft)
		{
			this.boxTopLeft = boxTopLeft;

			up = down = left = right = false;

			upBounds = new Rectangle((int)boxTopLeft.X + Padding, (int)boxTopLeft.Y, Padding, Padding);
			downBounds = new Rectangle((int)boxTopLeft.X + Padding, (int)boxTopLeft.Y + 2 * Padding, Padding, Padding);
			leftBounds = new Rectangle((int)boxTopLeft.X, (int)boxTopLeft.Y + Padding, Padding, Padding);
			rightBounds = new Rectangle((int)boxTopLeft.X + 2 * Padding, (int)boxTopLeft.Y + Padding, Padding, Padding);
		}

		public void Update(ButtonState up, ButtonState down, ButtonState left, ButtonState right)
		{
			this.up = up == ButtonState.Pressed;
			this.down = down == ButtonState.Pressed;
			this.left = left == ButtonState.Pressed;
			this.right = right == ButtonState.Pressed;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TestGame.Pixel, upBounds, up ? Color.Red : Color.Black);
			spriteBatch.Draw(TestGame.Pixel, downBounds, down ? Color.Red : Color.Black);
			spriteBatch.Draw(TestGame.Pixel, leftBounds, left ? Color.Red : Color.Black);
			spriteBatch.Draw(TestGame.Pixel, rightBounds, right ? Color.Red : Color.Black);
		}
	}
}
