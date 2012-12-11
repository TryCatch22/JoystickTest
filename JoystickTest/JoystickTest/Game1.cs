using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace JoystickTest
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		public const int Fps = 60;

		public const int BoxSize = 500;
		public const int BoxPadding = 50;
		public const int LineWeight = 3;

		public Vector2 ScreenSize { get; private set; }

		public SpriteFont Font;
		public Texture2D Pixel;

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		private GamePadState gamePadStateP1;
		private KeyboardState keyboardState;

		private Vector2 rightJoystickValue;
		private Point rightJoystickRelativePosition;
		private bool[,] paint;

		private Vector2 boxTopLeft;
		private Vector2 boxCenter;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			TargetElapsedTime = TimeSpan.FromSeconds(1f / Fps);

			ScreenSize = new Vector2(1000, 600);

			graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
			graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;

			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();

			boxTopLeft = new Vector2(ScreenSize.X - BoxSize - BoxPadding, BoxPadding);
			boxCenter = new Vector2(boxTopLeft.X + BoxSize / 2f, boxTopLeft.Y + BoxSize / 2f);

			ClearBox();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			Font = Content.Load<SpriteFont>(@"Fonts/Font");

			Pixel = new Texture2D(GraphicsDevice, 1, 1);
			Pixel.SetData(new[] { Color.White });
		}

		protected override void Update(GameTime gameTime)
		{
			gamePadStateP1 = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
			keyboardState = Keyboard.GetState();

			if ((gamePadStateP1.Buttons.Back == ButtonState.Pressed) || (keyboardState.IsKeyDown(Keys.Escape)))
				this.Exit();

			if ((gamePadStateP1.Buttons.Start == ButtonState.Pressed) || (keyboardState.IsKeyDown(Keys.Space)))
				ClearBox();

			rightJoystickValue = gamePadStateP1.ThumbSticks.Right;
			rightJoystickRelativePosition = new Point((int)(BoxSize / 2f + rightJoystickValue.X * BoxSize / 2f), (int)(BoxSize / 2f - rightJoystickValue.Y * BoxSize / 2f));

			paint[rightJoystickRelativePosition.X, rightJoystickRelativePosition.Y] = true;

			base.Update(gameTime);
		}

		private void ClearBox()
		{
			paint = new bool[BoxSize + 1, BoxSize + 1];
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			spriteBatch.DrawString(Font, "X: " + (rightJoystickValue.X < 0 ? "" : " ") + rightJoystickValue.X, new Vector2(10, 10), Color.Black);
			spriteBatch.DrawString(Font, "Y: " + (rightJoystickValue.Y < 0 ? "" : " ") + rightJoystickValue.Y, new Vector2(10, 50), Color.Black);

			for (int i = 0; i < paint.GetLength(0); i++)
				for (int j = 0; j < paint.GetLength(1); j++)
					if (paint[i, j])
						spriteBatch.Draw(Pixel, new Rectangle((int)(boxTopLeft.X + i ) - 1, (int)(boxTopLeft.Y + j) - 1, LineWeight, LineWeight), Color.Red);

			spriteBatch.Draw(Pixel, new Rectangle((int)boxTopLeft.X, (int)boxTopLeft.Y, BoxSize, LineWeight), Color.Black);
			spriteBatch.Draw(Pixel, new Rectangle((int)boxTopLeft.X, (int)boxTopLeft.Y, LineWeight, BoxSize), Color.Black);
			spriteBatch.Draw(Pixel, new Rectangle((int)boxTopLeft.X, (int)(boxTopLeft.Y + BoxSize), BoxSize, LineWeight), Color.Black);
			spriteBatch.Draw(Pixel, new Rectangle((int)(boxTopLeft.X + BoxSize), (int)boxTopLeft.Y, LineWeight, BoxSize), Color.Black);

			spriteBatch.Draw(Pixel, new Rectangle((int)(boxTopLeft.X + rightJoystickRelativePosition.X), (int)(boxTopLeft.Y + rightJoystickRelativePosition.Y), 2 * LineWeight, 2 * LineWeight), Color.Green);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
