using UnityEngine;
using System.Collections.Generic;

namespace InputRegistrar
{
	public class SamsungGamepadInputRegistrar : InputRegistrar<ButtonInput.Samsung, AxisInput.Samsung>
	{
		public SamsungGamepadInputRegistrar(int controllerNumber)
		{
			m_controllerNumber = controllerNumber;
			Initialise();
		}

		public override void Initialise()
		{
			base.Initialise();

			//Face Buttons
			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPressDown), ButtonInput.Samsung.One);
			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPress), ButtonInput.Samsung.One);
			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPressUp), ButtonInput.Samsung.One);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPressDown), ButtonInput.Samsung.Two);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPress), ButtonInput.Samsung.Two);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPressUp), ButtonInput.Samsung.Two);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPressDown), ButtonInput.Samsung.Three);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPress), ButtonInput.Samsung.Three);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPressUp), ButtonInput.Samsung.Three);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPressDown), ButtonInput.Samsung.Four);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPress), ButtonInput.Samsung.Four);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPressUp), ButtonInput.Samsung.Four);

			//Shoulder Buttons
			RegisterButton(new ButtonGesture(InputValue.Epsilon, ButtonAction.OnPressDown), ButtonInput.Samsung.LB);
			RegisterButton(new ButtonGesture(InputValue.Epsilon, ButtonAction.OnPress), ButtonInput.Samsung.LB);
			RegisterButton(new ButtonGesture(InputValue.Epsilon, ButtonAction.OnPressUp), ButtonInput.Samsung.LB);
			RegisterButton(new ButtonGesture(InputValue.Zeta, ButtonAction.OnPressDown), ButtonInput.Samsung.RB);
			RegisterButton(new ButtonGesture(InputValue.Zeta, ButtonAction.OnPress), ButtonInput.Samsung.RB);
			RegisterButton(new ButtonGesture(InputValue.Zeta, ButtonAction.OnPressUp), ButtonInput.Samsung.RB);

			//Axes
			RegisterAxis(new AxisGesture(InputValue.Alpha, AxisAction.GetAxis), AxisInput.Samsung.LeftX);
			RegisterAxis(new AxisGesture(InputValue.Beta, AxisAction.GetAxis), AxisInput.Samsung.LeftY);
			RegisterAxis(new AxisGesture(InputValue.Gamma, AxisAction.GetAxis), AxisInput.Samsung.RightX);
			RegisterAxis(new AxisGesture(InputValue.Delta, AxisAction.GetAxis), AxisInput.Samsung.RightY);
			RegisterAxis(new AxisGesture(InputValue.Epsilon, AxisAction.GetAxis), AxisInput.Samsung.DPadX);
			RegisterAxis(new AxisGesture(InputValue.Zeta, AxisAction.GetAxis), AxisInput.Samsung.DPadY);
		}

		protected override void SwitchButtonAction(KeyValuePair<ButtonGesture, ButtonInput.Samsung> binding)
		{
			string buttonAxis = ((int)binding.Value).ToString();
			if (m_controllerNumber == 0)
			{
				switch (binding.Key.ButtonAction)
				{
					case ButtonAction.OnPressDown:
						if (Input.GetKeyDown("joystick button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;

					case ButtonAction.OnPress:
						if (Input.GetKey("joystick button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;

					case ButtonAction.OnPressUp:
						if (Input.GetKeyUp("joystick button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;

					case ButtonAction.OnTouchDown:
						if (Input.GetKeyDown("joystick button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;

					case ButtonAction.OnTouch:
						if (Input.GetKey("joystick button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;

					case ButtonAction.OnTouchUp:
						if (Input.GetKeyUp("joystick button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;
				}
			}
			else
			{
				switch (binding.Key.ButtonAction)
				{
					case ButtonAction.OnPressDown:
						if (Input.GetKeyDown("joystick " + m_controllerNumber.ToString() + " button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;

					case ButtonAction.OnPress:
						if (Input.GetKey("joystick " + m_controllerNumber.ToString() + " button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;

					case ButtonAction.OnPressUp:
						if (Input.GetKeyUp("joystick " + m_controllerNumber.ToString() + " button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;

					case ButtonAction.OnTouchDown:
						if (Input.GetKeyDown("joystick " + m_controllerNumber.ToString() + " button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;

					case ButtonAction.OnTouch:
						if (Input.GetKey("joystick " + m_controllerNumber.ToString() + " button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;

					case ButtonAction.OnTouchUp:
						if (Input.GetKeyUp("joystick " + m_controllerNumber.ToString() + " button " + buttonAxis))
							m_buttonEvents[binding.Key]();
						break;
				}
			}
		}

		protected override void SwitchAxisAction(KeyValuePair<AxisGesture, AxisInput.Samsung> binding)
		{
			string axisname = "";
			switch (binding.Value)
			{
				case AxisInput.Samsung.LeftX:
					axisname = "XAxis_";
					break;
				case AxisInput.Samsung.LeftY:
					axisname = "YAxis_";
					break;
				case AxisInput.Samsung.RightX:
					axisname = "3rd_Axis_";
					break;
				case AxisInput.Samsung.RightY:
					axisname = "4th_Axis_";
					break;
				case AxisInput.Samsung.DPadX:
					axisname = "5th_Axis_";
					break;
				case AxisInput.Samsung.DPadY:
					axisname = "6th_Axis_";
					break;
			}
			if (axisname != "")
			{
				axisname += m_controllerNumber;
				float value = Input.GetAxis(axisname);
				switch (binding.Key.AxisAction)
				{
					case AxisAction.GetAxis:
						if (value != 0)
							m_axisEvents[binding.Key](value);
						break;
					case AxisAction.GetAxisRaw:
						if (value > 0)
							m_axisEvents[binding.Key](1f);
						else if (value < 0)
							m_axisEvents[binding.Key](-1f);
						break;
				}
			}
		}
	}
}