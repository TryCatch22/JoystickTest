using Microsoft.DirectX.DirectInput;
using Microsoft.Xna.Framework.Input;

namespace GenericGamePad
{
	public struct DirectInputButtons
	{
		public ButtonState A;
		public ButtonState B;
		public ButtonState X;
		public ButtonState Y;
		public ButtonState LeftBumper;
		public ButtonState LeftTrigger;
		public ButtonState LeftStick;
		public ButtonState RightBumper;
		public ButtonState RightTrigger;
		public ButtonState RightStick;
		public ButtonState Back;
		public ButtonState Start;

		public DirectInputButtons(Device device)
		{
			byte[] buttons = device.CurrentJoystickState.GetButtons();

			A = buttons[0] == 0 ? ButtonState.Released : ButtonState.Pressed;
			B = buttons[1] == 0 ? ButtonState.Released : ButtonState.Pressed;
			X = buttons[2] == 0 ? ButtonState.Released : ButtonState.Pressed;
			Y = buttons[3] == 0 ? ButtonState.Released : ButtonState.Pressed;
			LeftBumper = buttons[4] == 0 ? ButtonState.Released : ButtonState.Pressed;
			LeftTrigger = buttons[5] == 0 ? ButtonState.Released : ButtonState.Pressed;
			LeftStick = buttons[6] == 0 ? ButtonState.Released : ButtonState.Pressed;
			RightBumper = buttons[7] == 0 ? ButtonState.Released : ButtonState.Pressed;
			RightTrigger = buttons[8] == 0 ? ButtonState.Released : ButtonState.Pressed;
			RightStick = buttons[9] == 0 ? ButtonState.Released : ButtonState.Pressed;
			Back = buttons[10] == 0 ? ButtonState.Released : ButtonState.Pressed;
			Start = buttons[11] == 0 ? ButtonState.Released : ButtonState.Pressed;
		}
	}
}
