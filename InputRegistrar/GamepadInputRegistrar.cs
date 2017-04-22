using UnityEngine;
using System;
using System.Collections.Generic;

namespace InputRegistrar
{
	public class GamepadInputRegistrar<TButton, TAxis> : InputRegistrar<TButton, TAxis>
	{
		protected Dictionary<ButtonGesture, string> m_buttonBindings;
		protected Dictionary<AxisGesture, string> m_axisBindings;

		public override void Initialise()
		{
			base.Initialise();
			m_buttonBindings = new Dictionary<ButtonGesture, string>();
			m_axisBindings = new Dictionary<AxisGesture, string>();
		}

		protected override void RegisterButton(ButtonGesture gesture, TButton button)
		{
			if (!typeof(TButton).IsEnum)
			{
				Debug.LogError("TButton is not an enum");
				return;
			}

			string input = string.Empty;
			if (m_controllerNumber == 0)
			{
				input = "joystick button " + (int)Enum.Parse(typeof(TButton), button.ToString());
			}
			else
			{
				input = "joystick " + m_controllerNumber.ToString() + " button " + (int)Enum.Parse(typeof(TButton), button.ToString());
			}
			m_buttonBindings.Add(gesture, input);
			if (!m_buttonEvents.ContainsKey(gesture))
			{
				m_buttonEvents.Add(gesture, delegate { });
			}
		}

		public override void Update()
		{
			foreach (KeyValuePair<ButtonGesture, string> binding in m_buttonBindings)
			{
				SwitchButtonAction(binding);
			}
			foreach (KeyValuePair<AxisGesture, string> binding in m_axisBindings)
			{
				SwitchAxisAction(binding);
			}
		}

		protected virtual void SwitchButtonAction(KeyValuePair<ButtonGesture, string> binding)
		{
			switch (binding.Key.ButtonAction)
			{
				case ButtonAction.OnPressDown:
					if (Input.GetKeyDown(binding.Value))
						m_buttonEvents[binding.Key]();
					break;

				case ButtonAction.OnPress:
					if (Input.GetKey(binding.Value))
						m_buttonEvents[binding.Key]();
					break;

				case ButtonAction.OnPressUp:
					if (Input.GetKeyUp(binding.Value))
						m_buttonEvents[binding.Key]();
					break;
			}
		}

		protected virtual void SwitchAxisAction(KeyValuePair<AxisGesture, string> binding)
		{
			switch (binding.Key.AxisAction)
			{
				case AxisAction.GetAxis:
					m_axisEvents[binding.Key](Input.GetAxis(binding.Value));
					break;

				case AxisAction.GetAxisRaw:
					m_axisEvents[binding.Key](Input.GetAxisRaw(binding.Value));
					break;
			}
		}
	}
}