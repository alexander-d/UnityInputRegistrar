using UnityEngine;
using System.Collections.Generic;

namespace InputRegistrar
{
	public class PS4InputRegistrar : GamepadInputRegistrar<ButtonInput.PS4, AxisInput.PS4>
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
			RegisterButton(InputValue.Alpha, ButtonInput.PS4.X);
			RegisterButton(InputValue.Beta, ButtonInput.PS4.Circle);
			RegisterButton(InputValue.Gamma, ButtonInput.PS4.Square);
			RegisterButton(InputValue.Delta, ButtonInput.PS4.Triangle);

			//Axes
			RegisterAxis(InputValue.Alpha, AxisInput.PS4.LeftX);
			RegisterAxis(InputValue.Beta, AxisInput.PS4.LeftY);
			RegisterAxis(InputValue.Gamma, AxisInput.PS4.RightX);
			RegisterAxis(InputValue.Delta, AxisInput.PS4.RightY);
			RegisterAxis(InputValue.Epsilon, AxisInput.PS4.DPadX);
			RegisterAxis(InputValue.Zeta, AxisInput.PS4.DPadY);
			RegisterAxis(InputValue.Eta, AxisInput.PS4.L2);
			RegisterAxis(InputValue.Theta, AxisInput.PS4.R2);
		}

		protected override void RegisterAxis(AxisGesture gesture, AxisInput.PS4 axis)
		{
			string input = string.Empty;
			switch (axis)
			{
				case AxisInput.PS4.LeftX:
					input = "XAxis_";
					break;
				case AxisInput.PS4.LeftY:
					input = "YAxis_";
					break;
				case AxisInput.PS4.RightX:
					input = "3rd_Axis_";
					break;
				case AxisInput.PS4.RightY:
					input = "6th_Axis_";
					break;
				case AxisInput.PS4.DPadX:
					input = "7th_Axis_";
					break;
				case AxisInput.PS4.DPadY:
					input = "8th_Axis_";
					break;
				case AxisInput.PS4.L2:
					input = "4th_Axis_";
					break;
				case AxisInput.PS4.R2:
					input = "5th_Axis_";
					break;
			}
			input += m_controllerNumber;

			m_axisBindings.Add(gesture, input);
		}
	}
}