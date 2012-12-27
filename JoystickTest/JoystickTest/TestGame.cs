using System;
using System.Collections.Generic;
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
		private DPadPane dPadPane;
		private List<ButtonPane> buttonPanes;

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

			dPadPane = new DPadPane(new Vector2(JoystickPane.BoxPadding, ScreenSize.Y - 2.5f * JoystickPane.BoxPadding));

			buttonPanes = new List<ButtonPane>();

			List<string> names = new List<string>() { "A", "B", "X", "Y", "LB", "LT", "LS", "RB", "RT", "RS", "Bk", "St" };

			for (int i = 0; i < names.Count; i++)
				buttonPanes.Add(new ButtonPane(names[i], new Vector2(ScreenSize.X - 2 * (i + 2) * ButtonPane.Padding, ScreenSize.Y - 4 * ButtonPane.Padding)));

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

				GamePadDPad dPad = gamePadStateP1.DPad;
				dPadPane.Update(dPad.Up, dPad.Down, dPad.Left, dPad.Right);

				buttonPanes[0].Update(gamePadStateP1.Buttons.A == ButtonState.Pressed);
				buttonPanes[1].Update(gamePadStateP1.Buttons.B == ButtonState.Pressed);
				buttonPanes[2].Update(gamePadStateP1.Buttons.X == ButtonState.Pressed);
				buttonPanes[3].Update(gamePadStateP1.Buttons.Y == ButtonState.Pressed);
				buttonPanes[4].Update(gamePadStateP1.Buttons.LeftShoulder == ButtonState.Pressed);
				buttonPanes[5].Update(gamePadStateP1.Triggers.Left > 0f);
				buttonPanes[6].Update(gamePadStateP1.Buttons.LeftStick == ButtonState.Pressed);
				buttonPanes[7].Update(gamePadStateP1.Buttons.RightShoulder == ButtonState.Pressed);
				buttonPanes[8].Update(gamePadStateP1.Triggers.Right > 0f);
				buttonPanes[9].Update(gamePadStateP1.Buttons.RightStick == ButtonState.Pressed);
				buttonPanes[10].Update(gamePadStateP1.Buttons.Back == ButtonState.Pressed);
				buttonPanes[11].Update(gamePadStateP1.Buttons.Start == ButtonState.Pressed);
			}
			else
			{
				leftPane.Update(new Vector2(diGamePadP1.ThumbSticks.Left.X, -diGamePadP1.ThumbSticks.Left.Y));
				rightPane.Update(new Vector2(diGamePadP1.ThumbSticks.Right.X, -diGamePadP1.ThumbSticks.Right.Y));

				DirectInputDPad dPad = diGamePadP1.DPad;
				dPadPane.Update(dPad.Up, dPad.Down, dPad.Left, dPad.Right);

				buttonPanes[0].Update(diGamePadP1.Buttons.A == ButtonState.Pressed);
				buttonPanes[1].Update(diGamePadP1.Buttons.B == ButtonState.Pressed);
				buttonPanes[2].Update(diGamePadP1.Buttons.X == ButtonState.Pressed);
				buttonPanes[3].Update(diGamePadP1.Buttons.Y == ButtonState.Pressed);
				buttonPanes[4].Update(diGamePadP1.Buttons.LeftBumper == ButtonState.Pressed);
				buttonPanes[5].Update(diGamePadP1.Buttons.LeftTrigger == ButtonState.Pressed);
				buttonPanes[6].Update(diGamePadP1.Buttons.LeftStick == ButtonState.Pressed);
				buttonPanes[7].Update(diGamePadP1.Buttons.RightBumper == ButtonState.Pressed);
				buttonPanes[8].Update(diGamePadP1.Buttons.RightTrigger == ButtonState.Pressed);
				buttonPanes[9].Update(diGamePadP1.Buttons.RightStick == ButtonState.Pressed);
				buttonPanes[10].Update(diGamePadP1.Buttons.Back == ButtonState.Pressed);
				buttonPanes[11].Update(diGamePadP1.Buttons.Start == ButtonState.Pressed);
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

			dPadPane.Draw(spriteBatch);

			foreach (ButtonPane buttonPane in buttonPanes)
				buttonPane.Draw(spriteBatch);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
