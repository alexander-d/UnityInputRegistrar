using UnityEngine;
using System.Collections.Generic;

namespace InputRegistrar
{
	public class XboxInputRegistrar : InputRegistrar<ButtonInput.Xbox, AxisInput.Xbox>
	{
		public XboxInputRegistrar(int controllerNumber)
		{
			m_controllerNumber = controllerNumber;
			Initialise();
		}

		public override void Initialise()
		{
			base.Initialise();

			//Face Buttons
			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPressDown), ButtonInput.Xbox.A);
			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPress), ButtonInput.Xbox.A);
			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPressUp), ButtonInput.Xbox.A);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPressDown), ButtonInput.Xbox.B);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPress), ButtonInput.Xbox.B);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPressUp), ButtonInput.Xbox.B);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPressDown), ButtonInput.Xbox.X);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPress), ButtonInput.Xbox.X);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPressUp), ButtonInput.Xbox.X);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPressDown), ButtonInput.Xbox.Y);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPress), ButtonInput.Xbox.Y);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPressUp), ButtonInput.Xbox.Y);

			//Axes
			RegisterAxis(new AxisGesture(InputValue.Alpha, AxisAction.GetAxis), AxisInput.Xbox.LeftX);
			RegisterAxis(new AxisGesture(InputValue.Beta, AxisAction.GetAxis), AxisInput.Xbox.LeftY);
			RegisterAxis(new AxisGesture(InputValue.Gamma, AxisAction.GetAxis), AxisInput.Xbox.RightX);
			RegisterAxis(new AxisGesture(InputValue.Delta, AxisAction.GetAxis), AxisInput.Xbox.RightY);
			RegisterAxis(new AxisGesture(InputValue.Epsilon, AxisAction.GetAxis), AxisInput.Xbox.DPadX);
			RegisterAxis(new AxisGesture(InputValue.Zeta, AxisAction.GetAxis), AxisInput.Xbox.DPadY);
			RegisterAxis(new AxisGesture(InputValue.Eta, AxisAction.GetAxis), AxisInput.Xbox.LeftTrigger);
			RegisterAxis(new AxisGesture(InputValue.Theta, AxisAction.GetAxis), AxisInput.Xbox.RightTrigger);
		}

		protected override void SwitchButtonAction(KeyValuePair<ButtonGesture, ButtonInput.Xbox> binding)
		{
			string buttonAxis = ((int)binding.Value).ToString();
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

		protected override void SwitchAxisAction(KeyValuePair<AxisGesture, AxisInput.Xbox> binding)
		{
			string axisname = "";
			switch (binding.Value)
			{
				case AxisInput.Xbox.LeftX:
					axisname = "XAxis_";
					break;
				case AxisInput.Xbox.LeftY:
					axisname = "YAxis_";
					break;
				case AxisInput.Xbox.BothTriggers:
					axisname = "3rd_Axis_";
					break;
				case AxisInput.Xbox.RightX:
					axisname = "4th_Axis_";
					break;
				case AxisInput.Xbox.RightY:
					axisname = "5th_Axis_";
					break;
				case AxisInput.Xbox.DPadX:
					axisname = "6th_Axis_";
					break;
				case AxisInput.Xbox.DPadY:
					axisname = "7th_Axis_";
					break;
				case AxisInput.Xbox.LeftTrigger:
					axisname = "9th_Axis_";
					break;
				case AxisInput.Xbox.RightTrigger:
					axisname = "10th_Axis_";
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