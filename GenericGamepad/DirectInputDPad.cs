using Microsoft.DirectX.DirectInput;
using Microsoft.Xna.Framework.Input;

namespace GenericGamePad
{
	public struct DirectInputDPad
	{
		public ButtonState Up;
		public ButtonState Down;
		public ButtonState Left;
		public ButtonState Right;

		public DirectInputDPad(Device device)
		{
			Up = ButtonState.Released;
			Down = ButtonState.Released;
			Left = ButtonState.Released;
			Right = ButtonState.Released;

			int direction = device.CurrentJoystickState.GetPointOfView()[0];

			switch (direction)
			{
				case (-1):
					return;

				case (0):
					Up = ButtonState.Pressed;
					return;

				case (4500):
					Up = ButtonState.Pressed;
					Right = ButtonState.Pressed;
					return;

				case (9000):
					Right = ButtonState.Pressed;
					return;

				case (13500):
					Right = ButtonState.Pressed;
					Down = ButtonState.Pressed;
					return;

				case (18000):
					Down = ButtonState.Pressed;
					return;

				case (22500):
					Down = ButtonState.Pressed;
					Left = ButtonState.Pressed;
					return;

				case (27000):
					Left = ButtonState.Pressed;
					return;

				case (31500):
					Left = ButtonState.Pressed;
					Up = ButtonState.Pressed;
					return;

				default:
					return;
			}
		}
	}
}
