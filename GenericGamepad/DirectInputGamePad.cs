using System;
using System.Collections.Generic;
using Microsoft.DirectX.DirectInput;

namespace GenericGamePad
{
	public class DirectInputGamePad
	{
		private static List<DirectInputGamePad> gamePads;
		public static List<DirectInputGamePad> GamePads
		{
			get
			{
				if (gamePads == null)
					LoadGamePads();
				return gamePads;
			}
		}

		private static void LoadGamePads()
		{
			gamePads = new List<DirectInputGamePad>();

			DeviceList gamePadList = Manager.GetDevices(DeviceType.Gamepad, EnumDevicesFlags.AttachedOnly);
			DeviceList joystickList = Manager.GetDevices(DeviceType.Joystick, EnumDevicesFlags.AttachedOnly);

			foreach (DeviceInstance deviceInstance in gamePadList)
				gamePads.Add(new DirectInputGamePad(deviceInstance.InstanceGuid));

			foreach (DeviceInstance deviceInstance in joystickList)
				gamePads.Add(new DirectInputGamePad(deviceInstance.InstanceGuid));
		}

		public Device Device { get; private set; }

		public DirectInputGamePad(Guid gamePadGuid)
		{
			Device = new Device(gamePadGuid);
			Device.SetDataFormat(DeviceDataFormat.Joystick);
			Device.Acquire();
		}

		public DirectInputThumbSticks ThumbSticks { get { return new DirectInputThumbSticks(Device); } }

		public DirectInputDPad DPad { get { return new DirectInputDPad(Device); } }

		public DirectInputButtons Buttons { get { return new DirectInputButtons(Device); } }
	}
}
