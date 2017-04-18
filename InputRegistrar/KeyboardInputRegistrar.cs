using UnityEngine;
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

			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPressDown), KeyCode.Space);
			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPress), KeyCode.Space);
			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPressUp), KeyCode.Space);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPressDown), KeyCode.LeftControl);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPress), KeyCode.LeftControl);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPressUp), KeyCode.LeftControl);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPressDown), KeyCode.RightControl);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPress), KeyCode.RightControl);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPressUp), KeyCode.RightControl);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPressDown), KeyCode.LeftAlt);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPress), KeyCode.LeftAlt);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPressUp), KeyCode.LeftAlt);
			RegisterButton(new ButtonGesture(InputValue.Epsilon, ButtonAction.OnPressDown), KeyCode.RightAlt);
			RegisterButton(new ButtonGesture(InputValue.Epsilon, ButtonAction.OnPress), KeyCode.RightAlt);
			RegisterButton(new ButtonGesture(InputValue.Epsilon, ButtonAction.OnPressUp), KeyCode.RightAlt);

			RegisterButton(new ButtonGesture(InputValue.Upsilon, ButtonAction.OnPressDown), KeyCode.UpArrow);
			RegisterButton(new ButtonGesture(InputValue.Upsilon, ButtonAction.OnPress), KeyCode.UpArrow);
			RegisterButton(new ButtonGesture(InputValue.Upsilon, ButtonAction.OnPressUp), KeyCode.UpArrow);
			RegisterButton(new ButtonGesture(InputValue.Phi, ButtonAction.OnPressDown), KeyCode.LeftArrow);
			RegisterButton(new ButtonGesture(InputValue.Phi, ButtonAction.OnPress), KeyCode.LeftArrow);
			RegisterButton(new ButtonGesture(InputValue.Phi, ButtonAction.OnPressUp), KeyCode.LeftArrow);
			RegisterButton(new ButtonGesture(InputValue.Chi, ButtonAction.OnPressDown), KeyCode.RightArrow);
			RegisterButton(new ButtonGesture(InputValue.Chi, ButtonAction.OnPress), KeyCode.RightArrow);
			RegisterButton(new ButtonGesture(InputValue.Chi, ButtonAction.OnPressUp), KeyCode.RightArrow);
			RegisterButton(new ButtonGesture(InputValue.Psi, ButtonAction.OnPressDown), KeyCode.DownArrow);
			RegisterButton(new ButtonGesture(InputValue.Psi, ButtonAction.OnPress), KeyCode.DownArrow);
			RegisterButton(new ButtonGesture(InputValue.Psi, ButtonAction.OnPressUp), KeyCode.DownArrow);
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