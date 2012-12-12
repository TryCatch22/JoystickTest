using Microsoft.DirectX.DirectInput;
using Microsoft.Xna.Framework;

namespace GenericGamePad
{
	public struct DirectInputThumbSticks
	{
		public Vector2 Left;
		public Vector2 Right;

		private const float Center = 32767.5f;

		public DirectInputThumbSticks(Device device)
		{
			Left = Vector2.Zero;
			Right = Vector2.Zero;

			JoystickState state = device.CurrentJoystickState;

			if (device.Caps.NumberAxes > 0)
			{
				Left = new Vector2((state.X - Center) / Center, (state.Y - Center) / Center);

				if (device.Caps.NumberAxes > 2)
					Right = new Vector2((state.Rz - Center) / Center, (state.Z - Center) / Center);
			}
		}
	}
}
