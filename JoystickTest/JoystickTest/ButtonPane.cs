using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JoystickTest
{
	class ButtonPane
	{
		public const int Padding = 25;

		public string Name { get; private set; }

		public bool Pressed { get; private set; }

		private Vector2 boxTopLeft;

		public ButtonPane(string name, Vector2 boxTopLeft)
		{
			Name = name;
			this.boxTopLeft = boxTopLeft;

			Pressed = false;
		}

		public void Update(bool pressed)
		{
			Pressed = pressed;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TestGame.Pixel, new Rectangle((int)boxTopLeft.X, (int)boxTopLeft.Y, Padding, Padding), Pressed ? Color.Red : Color.Black);
			spriteBatch.DrawString(TestGame.Font, Name, boxTopLeft, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
		}
	}
}
