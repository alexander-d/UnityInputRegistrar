﻿using UnityEngine;
using System.Collections.Generic;

namespace InputRegistrar
{
	public class KeyboardInputRegistrar : InputRegistrar<KeyCode, KeyCode>
	{
		public KeyboardInputRegistrar()
		{
			Initialise();
		}

		public override void Initialise()
		{
			base.Initialise();

			RegisterButton(InputValue.Alpha, KeyCode.Space);
			RegisterButton(InputValue.Beta, KeyCode.LeftControl);
			RegisterButton(InputValue.Gamma, KeyCode.RightControl);
			RegisterButton(InputValue.Delta, KeyCode.LeftAlt);
			RegisterButton(InputValue.Epsilon, KeyCode.RightAlt);

			RegisterButton(InputValue.Upsilon, KeyCode.UpArrow);
			RegisterButton(InputValue.Phi, KeyCode.LeftArrow);
			RegisterButton(InputValue.Chi, KeyCode.RightArrow);
			RegisterButton(InputValue.Psi, KeyCode.DownArrow);
		}

		protected override void SwitchButtonAction(KeyValuePair<ButtonGesture, KeyCode> binding)
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

				case ButtonAction.OnTouchDown:
					if (Input.GetKeyDown(binding.Value))
						m_buttonEvents[binding.Key]();
					break;

				case ButtonAction.OnTouch:
					if (Input.GetKey(binding.Value))
						m_buttonEvents[binding.Key]();
					break;

				case ButtonAction.OnTouchUp:
					if (Input.GetKeyUp(binding.Value))
						m_buttonEvents[binding.Key]();
					break;
			}
		}
	}
}