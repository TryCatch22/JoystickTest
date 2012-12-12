using System;
using GenericGamePad;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

		private bool usingXInput;

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		private KeyboardState keyboardState;
		private GamePadState gamePadStateP1;
		private DirectInputGamePad diGamePadP1;

		private Vector2 rightJoystickValue;
		private Point rightJoystickRelativePosition;
		private bool[,] rightPaint;

		private Vector2 rightBoxTopLeft;
		private Vector2 rightBoxCenter;

		private Vector2 leftJoystickValue;
		private Point leftJoystickRelativePosition;
		private bool[,] leftPaint;

		private Vector2 leftBoxTopLeft;
		private Vector2 leftBoxCenter;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			TargetElapsedTime = TimeSpan.FromSeconds(1f / Fps);

			ScreenSize = new Vector2(1280, 800);

			graphics.IsFullScreen = false;
			graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
			graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;
			
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();

			if (GamePad.GetState(PlayerIndex.One).IsConnected)
				usingXInput = true;
			else
				diGamePadP1 = DirectInputGamePad.GamePads[0];

			rightBoxTopLeft = new Vector2(ScreenSize.X - BoxSize - BoxPadding, 2 * BoxPadding);
			rightBoxCenter = new Vector2(rightBoxTopLeft.X + BoxSize / 2f, rightBoxTopLeft.Y + BoxSize / 2f);

			leftBoxTopLeft = new Vector2(BoxPadding, 2 * BoxPadding);
			leftBoxCenter = new Vector2(leftBoxTopLeft.X + BoxSize / 2f, leftBoxTopLeft.Y + BoxSize / 2f);

			ClearBoxes();
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

			if (keyboardState.IsKeyDown(Keys.Escape))
				this.Exit();

			if (keyboardState.IsKeyDown(Keys.Space))
				ClearBoxes();

			if (usingXInput)
			{
				if ((gamePadStateP1.Buttons.Back == ButtonState.Pressed))
					this.Exit();

				if ((gamePadStateP1.Buttons.Start == ButtonState.Pressed))
					ClearBoxes();

				rightJoystickValue = gamePadStateP1.ThumbSticks.Right;
				rightJoystickRelativePosition = new Point((int)(BoxSize / 2f + rightJoystickValue.X * BoxSize / 2f), (int)(BoxSize / 2f - rightJoystickValue.Y * BoxSize / 2f));
				rightPaint[rightJoystickRelativePosition.X, rightJoystickRelativePosition.Y] = true;

				leftJoystickValue = gamePadStateP1.ThumbSticks.Left;
				leftJoystickRelativePosition = new Point((int)(BoxSize / 2f + leftJoystickValue.X * BoxSize / 2f), (int)(BoxSize / 2f - leftJoystickValue.Y * BoxSize / 2f));
				leftPaint[leftJoystickRelativePosition.X, leftJoystickRelativePosition.Y] = true;
			}
			else
			{
				rightJoystickValue = diGamePadP1.ThumbSticks.Right;
				rightJoystickRelativePosition = new Point((int)(BoxSize / 2f + rightJoystickValue.X * BoxSize / 2f), (int)(BoxSize / 2f + rightJoystickValue.Y * BoxSize / 2f));
				rightPaint[rightJoystickRelativePosition.X, rightJoystickRelativePosition.Y] = true;

				leftJoystickValue = diGamePadP1.ThumbSticks.Left;
				leftJoystickRelativePosition = new Point((int)(BoxSize / 2f + leftJoystickValue.X * BoxSize / 2f), (int)(BoxSize / 2f + leftJoystickValue.Y * BoxSize / 2f));
				leftPaint[leftJoystickRelativePosition.X, leftJoystickRelativePosition.Y] = true;
			}

			base.Update(gameTime);
		}

		private void ClearBoxes()
		{
			rightPaint = new bool[BoxSize + 1, BoxSize + 1];
			leftPaint = new bool[BoxSize + 1, BoxSize + 1];
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			// right stick
			spriteBatch.DrawString(Font, "X: " + (rightJoystickValue.X < 0 ? "" : " ") + rightJoystickValue.X, rightBoxTopLeft - new Vector2(0, 2 * BoxPadding), Color.Black);
			spriteBatch.DrawString(Font, "Y: " + (rightJoystickValue.Y < 0 ? "" : " ") + rightJoystickValue.Y, rightBoxTopLeft - new Vector2(0, BoxPadding), Color.Black);

			for (int i = 0; i < rightPaint.GetLength(0); i++)
				for (int j = 0; j < rightPaint.GetLength(1); j++)
					if (rightPaint[i, j])
						spriteBatch.Draw(Pixel, new Rectangle((int)(rightBoxTopLeft.X + i ) - 1, (int)(rightBoxTopLeft.Y + j) - 1, LineWeight, LineWeight), Color.Red);

			spriteBatch.Draw(Pixel, new Rectangle((int)rightBoxTopLeft.X, (int)rightBoxTopLeft.Y, BoxSize, LineWeight), Color.Black);
			spriteBatch.Draw(Pixel, new Rectangle((int)rightBoxTopLeft.X, (int)rightBoxTopLeft.Y, LineWeight, BoxSize), Color.Black);
			spriteBatch.Draw(Pixel, new Rectangle((int)rightBoxTopLeft.X, (int)(rightBoxTopLeft.Y + BoxSize), BoxSize, LineWeight), Color.Black);
			spriteBatch.Draw(Pixel, new Rectangle((int)(rightBoxTopLeft.X + BoxSize), (int)rightBoxTopLeft.Y, LineWeight, BoxSize), Color.Black);

			spriteBatch.Draw(Pixel, new Rectangle((int)(rightBoxTopLeft.X + rightJoystickRelativePosition.X), (int)(rightBoxTopLeft.Y + rightJoystickRelativePosition.Y), 2 * LineWeight, 2 * LineWeight), Color.Green);

			// left stick
			spriteBatch.DrawString(Font, "X: " + (leftJoystickValue.X < 0 ? "" : " ") + leftJoystickValue.X, leftBoxTopLeft - new Vector2(0, 2 * BoxPadding), Color.Black);
			spriteBatch.DrawString(Font, "Y: " + (leftJoystickValue.Y < 0 ? "" : " ") + leftJoystickValue.Y, leftBoxTopLeft - new Vector2(0, BoxPadding), Color.Black);

			for (int i = 0; i < leftPaint.GetLength(0); i++)
				for (int j = 0; j < leftPaint.GetLength(1); j++)
					if (leftPaint[i, j])
						spriteBatch.Draw(Pixel, new Rectangle((int)(leftBoxTopLeft.X + i) - 1, (int)(leftBoxTopLeft.Y + j) - 1, LineWeight, LineWeight), Color.Red);

			spriteBatch.Draw(Pixel, new Rectangle((int)leftBoxTopLeft.X, (int)leftBoxTopLeft.Y, BoxSize, LineWeight), Color.Black);
			spriteBatch.Draw(Pixel, new Rectangle((int)leftBoxTopLeft.X, (int)leftBoxTopLeft.Y, LineWeight, BoxSize), Color.Black);
			spriteBatch.Draw(Pixel, new Rectangle((int)leftBoxTopLeft.X, (int)(leftBoxTopLeft.Y + BoxSize), BoxSize, LineWeight), Color.Black);
			spriteBatch.Draw(Pixel, new Rectangle((int)(leftBoxTopLeft.X + BoxSize), (int)leftBoxTopLeft.Y, LineWeight, BoxSize), Color.Black);

			spriteBatch.Draw(Pixel, new Rectangle((int)(leftBoxTopLeft.X + leftJoystickRelativePosition.X), (int)(leftBoxTopLeft.Y + leftJoystickRelativePosition.Y), 2 * LineWeight, 2 * LineWeight), Color.Green);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
