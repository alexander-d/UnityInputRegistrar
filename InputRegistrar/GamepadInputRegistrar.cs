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
		}

		protected override void CheckForButtonInput(KeyValuePair<ButtonGesture, Action> binding)
		{
			switch (binding.Key.ButtonAction)
			{
				case ButtonAction.OnPressDown:
					if (Input.GetKeyDown(m_buttonBindings[binding.Key]))
						binding.Value();
					break;

				case ButtonAction.OnPress:
					if (Input.GetKey(m_buttonBindings[binding.Key]))
						binding.Value();
					break;

				case ButtonAction.OnPressUp:
					if (Input.GetKeyUp(m_buttonBindings[binding.Key]))
						binding.Value();
					break;
			}
		}

		protected override void CheckForAxisInput(KeyValuePair<AxisGesture, Action<float>> binding)
		{
			switch (binding.Key.AxisAction)
			{
				case AxisAction.GetAxis:
					binding.Value(Input.GetAxis(m_axisBindings[binding.Key]));
					break;

				case AxisAction.GetAxisRaw:
					binding.Value(Input.GetAxisRaw(m_axisBindings[binding.Key]));
					break;
			}
		}
	}
}