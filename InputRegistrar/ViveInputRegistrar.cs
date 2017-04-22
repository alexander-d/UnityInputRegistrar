using UnityEngine;
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

            RegisterButton(InputValue.Alpha, SteamVR_Controller.ButtonMask.Touchpad);
			RegisterTouchButton(InputValue.Alpha,  SteamVR_Controller.ButtonMask.Touchpad);
            RegisterButton(InputValue.Beta, SteamVR_Controller.ButtonMask.Trigger);
            RegisterButton(InputValue.Gamma, SteamVR_Controller.ButtonMask.Grip);
            RegisterButton(InputValue.Delta, SteamVR_Controller.ButtonMask.ApplicationMenu);

            RegisterAxis(InputValue.Beta, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);

			RegisterDoubleAxis(InputValue.Alpha, Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
		}

		protected override void RegisterButton(ButtonGesture gesture, ulong button)
		{
			m_buttonBindings.Add(gesture, button);
			if (!m_buttonEvents.ContainsKey(gesture))
			{
				m_buttonEvents.Add(gesture, delegate { });
			}
		}

		protected override void RegisterAxis(AxisGesture gesture, EVRButtonId axis)
		{
			m_axisBindings.Add(gesture, axis);
			if (!m_axisEvents.ContainsKey(gesture))
			{
				m_axisEvents.Add(gesture, delegate { });
			}
		}

		protected override void RegisterDoubleAxis(AxisGesture gesture, EVRButtonId axis)
		{
			m_doubleAxisBindings.Add(gesture, axis);
			if (!m_doubleAxisEvents.ContainsKey(gesture))
			{
				m_doubleAxisEvents.Add(gesture, delegate { });
			}
		}

		public override void Update()
		{
			foreach (KeyValuePair<ButtonGesture, ulong> binding in m_buttonBindings)
			{
				SwitchButtonAction(binding);
			}
			foreach (KeyValuePair<AxisGesture, Valve.VR.EVRButtonId> binding in m_axisBindings)
			{
				SwitchAxisAction(binding);
			}
			foreach (KeyValuePair<AxisGesture, Valve.VR.EVRButtonId> binding in m_doubleAxisBindings)
			{
				SwitchDoubleAxisAction(binding);
			}
		}

		protected virtual void SwitchButtonAction(KeyValuePair<ButtonGesture, ulong> binding)
        {
            SteamVR_Controller.Device device = SteamVR_Controller.Input(m_controllerNumber);
            switch (binding.Key.ButtonAction)
            {
                case ButtonAction.OnPressDown:
                    if (device.GetPressDown(binding.Value))
						m_buttonEvents[binding.Key]();
                    break;

                case ButtonAction.OnPress:
                    if (device.GetPress(binding.Value))
						m_buttonEvents[binding.Key]();
                    break;

                case ButtonAction.OnPressUp:
                    if (device.GetPressUp(binding.Value))
						m_buttonEvents[binding.Key]();
                    break;

                case ButtonAction.OnTouchDown:
                    if (device.GetTouchDown(binding.Value))
						m_buttonEvents[binding.Key]();
                    break;

                case ButtonAction.OnTouch:
                    if (device.GetTouch(binding.Value))
						m_buttonEvents[binding.Key]();
                    break;

                case ButtonAction.OnTouchUp:
                    if (device.GetTouchUp(binding.Value))
						m_buttonEvents[binding.Key]();
                    break;
            }
        }

        protected virtual void SwitchAxisAction(KeyValuePair<AxisGesture, Valve.VR.EVRButtonId> binding)
        {
            SteamVR_Controller.Device device = SteamVR_Controller.Input(m_controllerNumber);

            Vector2 value = device.GetAxis(binding.Value);
            switch (binding.Key.AxisAction)
            {
                case AxisAction.GetAxis:
                    if (value != Vector2.zero)
                        m_axisEvents[binding.Key](value.x);
                    break;
            }
        }

		protected virtual void SwitchDoubleAxisAction(KeyValuePair<AxisGesture, Valve.VR.EVRButtonId> binding)
		{
			SteamVR_Controller.Device device = SteamVR_Controller.Input(m_controllerNumber);

			Vector2 value = device.GetAxis();
			switch (binding.Key.AxisAction)
			{
				case AxisAction.GetAxis:
					if (value != Vector2.zero)
						m_doubleAxisEvents[binding.Key](value);
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