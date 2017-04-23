using UnityEngine;
using System;
using System.Collections.Generic;

namespace InputRegistrar
{
	public class KeyboardInputRegistrar : InputRegistrar<KeyCode>
	{
		protected Dictionary<ButtonGesture, KeyCode> m_buttonBindings;

		public KeyboardInputRegistrar()
		{
			Initialise();
		}

		public override void Initialise()
		{
			m_buttonBindings = new Dictionary<ButtonGesture, KeyCode>();

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

		protected override void RegisterButton(ButtonGesture gesture, KeyCode button)
		{
			m_buttonBindings.Add(gesture, button);
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
	}
}