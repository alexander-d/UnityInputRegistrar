using UnityEngine;
using System.Collections.Generic;

namespace InputRegistrar
{
	public class PS4InputRegistrar : InputRegistrar<ButtonInput.PS4, AxisInput.PS4>
	{
		public PS4InputRegistrar(int controllerNumber)
		{
			m_controllerNumber = controllerNumber;
			Initialise();
		}

		public override void Initialise()
		{
			base.Initialise();

			//Face Buttons
			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPressDown), ButtonInput.PS4.X);
			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPress), ButtonInput.PS4.X);
			RegisterButton(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPressUp), ButtonInput.PS4.X);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPressDown), ButtonInput.PS4.Circle);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPress), ButtonInput.PS4.Circle);
			RegisterButton(new ButtonGesture(InputValue.Beta, ButtonAction.OnPressUp), ButtonInput.PS4.Circle);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPressDown), ButtonInput.PS4.Square);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPress), ButtonInput.PS4.Square);
			RegisterButton(new ButtonGesture(InputValue.Gamma, ButtonAction.OnPressUp), ButtonInput.PS4.Square);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPressDown), ButtonInput.PS4.Triangle);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPress), ButtonInput.PS4.Triangle);
			RegisterButton(new ButtonGesture(InputValue.Delta, ButtonAction.OnPressUp), ButtonInput.PS4.Triangle);

			//Axes
			RegisterAxis(new AxisGesture(InputValue.Alpha, AxisAction.GetAxis), AxisInput.PS4.LeftX);
			RegisterAxis(new AxisGesture(InputValue.Beta, AxisAction.GetAxis), AxisInput.PS4.LeftY);
			RegisterAxis(new AxisGesture(InputValue.Gamma, AxisAction.GetAxis), AxisInput.PS4.RightX);
			RegisterAxis(new AxisGesture(InputValue.Delta, AxisAction.GetAxis), AxisInput.PS4.RightY);
			RegisterAxis(new AxisGesture(InputValue.Epsilon, AxisAction.GetAxis), AxisInput.PS4.DPadX);
			RegisterAxis(new AxisGesture(InputValue.Zeta, AxisAction.GetAxis), AxisInput.PS4.DPadY);
			RegisterAxis(new AxisGesture(InputValue.Eta, AxisAction.GetAxis), AxisInput.PS4.L2);
			RegisterAxis(new AxisGesture(InputValue.Theta, AxisAction.GetAxis), AxisInput.PS4.R2);
		}

		protected override void SwitchButtonAction(KeyValuePair<ButtonGesture, ButtonInput.PS4> binding)
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

		protected override void SwitchAxisAction(KeyValuePair<AxisGesture, AxisInput.PS4> binding)
		{
			string axisname = "";
			switch (binding.Value)
			{
				case AxisInput.PS4.LeftX:
					axisname = "XAxis_";
					break;
				case AxisInput.PS4.LeftY:
					axisname = "YAxis_";
					break;
				case AxisInput.PS4.RightX:
					axisname = "3rd_Axis_";
					break;
				case AxisInput.PS4.RightY:
					axisname = "6th_Axis_";
					break;
				case AxisInput.PS4.DPadX:
					axisname = "7th_Axis_";
					break;
				case AxisInput.PS4.DPadY:
					axisname = "8th_Axis_";
					break;
				case AxisInput.PS4.L2:
					axisname = "4th_Axis_";
					break;
				case AxisInput.PS4.R2:
					axisname = "5th_Axis_";
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