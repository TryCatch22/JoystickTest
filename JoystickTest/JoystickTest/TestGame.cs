using System;
using GenericGamePad;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JoystickTest
{
	public class TestGame : Microsoft.Xna.Framework.Game
	{
		public const int Fps = 60;

		public static Vector2 ScreenSize { get; private set; }

		public static bool usingXInput;

		public static SpriteFont Font;
		public static Texture2D Pixel;

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		private KeyboardState keyboardState;
		private GamePadState gamePadStateP1;
		private DirectInputGamePad diGamePadP1;

		private JoystickPane leftPane, rightPane;

		public TestGame()
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

			leftPane = new JoystickPane(new Vector2(JoystickPane.BoxPadding, 2 * JoystickPane.BoxPadding));
			rightPane = new JoystickPane(new Vector2(ScreenSize.X - JoystickPane.BoxSize - JoystickPane.BoxPadding, 2 * JoystickPane.BoxPadding));

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

			if (keyboardState.IsKeyDown(Keys.Escape) ||
				(gamePadStateP1.Buttons.Back == ButtonState.Pressed))
				this.Exit();

			if (keyboardState.IsKeyDown(Keys.Space) ||
				(gamePadStateP1.Buttons.Start == ButtonState.Pressed))
				ClearBoxes();

			if (usingXInput)
			{
				leftPane.Update(gamePadStateP1.ThumbSticks.Left);
				rightPane.Update(gamePadStateP1.ThumbSticks.Right);
			}
			else
			{
				leftPane.Update(new Vector2(diGamePadP1.ThumbSticks.Left.X, -diGamePadP1.ThumbSticks.Left.Y));
				rightPane.Update(new Vector2(diGamePadP1.ThumbSticks.Right.X, -diGamePadP1.ThumbSticks.Right.Y));
			}

			base.Update(gameTime);
		}

		public void ClearBoxes()
		{
			leftPane.ClearBox();
			rightPane.ClearBox();
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			leftPane.Draw(spriteBatch);
			rightPane.Draw(spriteBatch);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
