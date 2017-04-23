using UnityEngine;
using System;
using System.Collections.Generic;
using Valve.VR;

namespace InputRegistrar
{
	public class ViveInputRegistrar : InputRegistrar<ulong, Valve.VR.EVRButtonId, Valve.VR.EVRButtonId>
	{
		protected Dictionary<ButtonGesture, ulong> m_buttonBindings;
		protected Dictionary<AxisGesture, Valve.VR.EVRButtonId> m_axisBindings;
		protected Dictionary<AxisGesture, Valve.VR.EVRButtonId> m_doubleAxisBindings;

		public ViveInputRegistrar(int controllerNumber)
		{
			m_controllerNumber = controllerNumber;
			Initialise();
		}

		public override void Initialise()
		{
			base.Initialise();

			m_buttonBindings = new Dictionary<ButtonGesture, ulong>();
			m_axisBindings = new Dictionary<AxisGesture, EVRButtonId>();
			m_doubleAxisBindings = new Dictionary<AxisGesture, EVRButtonId>();

			RegisterButton(InputValue.Alpha, SteamVR_Controller.ButtonMask.Touchpad);
			RegisterTouchButton(InputValue.Alpha, SteamVR_Controller.ButtonMask.Touchpad);
			RegisterButton(InputValue.Beta, SteamVR_Controller.ButtonMask.Trigger);
			RegisterButton(InputValue.Gamma, SteamVR_Controller.ButtonMask.Grip);
			RegisterButton(InputValue.Delta, SteamVR_Controller.ButtonMask.ApplicationMenu);

			RegisterAxis(InputValue.Beta, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
			RegisterDoubleAxis(InputValue.Alpha, Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
		}

		protected override void RegisterButton(ButtonGesture gesture, ulong button)
		{
			m_buttonBindings.Add(gesture, button);
		}

		protected override void RegisterAxis(AxisGesture gesture, EVRButtonId axis)
		{
			m_axisBindings.Add(gesture, axis);
		}

		protected override void RegisterDoubleAxis(AxisGesture gesture, EVRButtonId axis)
		{
			m_doubleAxisBindings.Add(gesture, axis);
		}

		protected override void CheckForButtonInput(KeyValuePair<ButtonGesture, Action> binding)
		{
			SteamVR_Controller.Device device = SteamVR_Controller.Input(m_controllerNumber);
			switch (binding.Key.ButtonAction)
			{
				case ButtonAction.OnPressDown:
					if (device.GetPressDown(m_buttonBindings[binding.Key]))
						binding.Value();
					break;

				case ButtonAction.OnPress:
					if (device.GetPress(m_buttonBindings[binding.Key]))
						binding.Value();
					break;

				case ButtonAction.OnPressUp:
					if (device.GetPressUp(m_buttonBindings[binding.Key]))
						binding.Value();
					break;

				case ButtonAction.OnTouchDown:
					if (device.GetTouchDown(m_buttonBindings[binding.Key]))
						binding.Value();
					break;

				case ButtonAction.OnTouch:
					if (device.GetTouch(m_buttonBindings[binding.Key]))
						binding.Value();
					break;

				case ButtonAction.OnTouchUp:
					if (device.GetTouchUp(m_buttonBindings[binding.Key]))
						binding.Value();
					break;
			}
		}

		protected override void CheckForAxisInput(KeyValuePair<AxisGesture, Action<float>> binding)
		{
			SteamVR_Controller.Device device = SteamVR_Controller.Input(m_controllerNumber);

			switch (binding.Key.AxisAction)
			{
				case AxisAction.GetAxis:
					binding.Value(device.GetAxis(m_axisBindings[binding.Key]).x);
					break;
			}
		}

		protected override void CheckForDoubleAxisInput(KeyValuePair<AxisGesture, Action<Vector2>> binding)
		{
			SteamVR_Controller.Device device = SteamVR_Controller.Input(m_controllerNumber);

			switch (binding.Key.AxisAction)
			{
				case AxisAction.GetAxis:
					binding.Value(device.GetAxis(m_doubleAxisBindings[binding.Key]));
					break;
			}
		}

		public void VibrateController(ushort _intensity)
		{
			SteamVR_Controller.Device device = SteamVR_Controller.Input(m_controllerNumber);
			//rumble limit is 3999
			if (_intensity >= (ushort)4000)
				_intensity = (ushort)3999;

			device.TriggerHapticPulse(_intensity);
		}
	}
}