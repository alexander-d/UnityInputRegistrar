using UnityEngine;
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
			if (!m_buttonEvents.ContainsKey(gesture))
			{
				m_buttonEvents.Add(gesture, delegate { });
			}
		}

		public override void Update()
		{
			foreach (KeyValuePair<ButtonGesture, KeyCode> binding in m_buttonBindings)
			{
				SwitchButtonAction(binding);
			}
		}

		protected virtual void SwitchButtonAction(KeyValuePair<ButtonGesture, KeyCode> binding)
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

		public override void UnbindAll()
		{
			base.UnbindAll();
			m_buttonBindings.Clear();
		}
	}
}
