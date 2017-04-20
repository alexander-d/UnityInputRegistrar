using UnityEngine;
using System.Collections.Generic;

namespace InputRegistrar
{
	public class SamsungGamepadInputRegistrar : GamepadInputRegistrar<ButtonInput.Samsung, AxisInput.Samsung>
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
			RegisterButton(InputValue.Alpha, ButtonInput.Samsung.One);
			RegisterButton(InputValue.Beta, ButtonInput.Samsung.Two);
			RegisterButton(InputValue.Gamma, ButtonInput.Samsung.Three);
			RegisterButton(InputValue.Delta, ButtonInput.Samsung.Four);

			//Shoulder Buttons
			RegisterButton(InputValue.Epsilon, ButtonInput.Samsung.LB);
			RegisterButton(InputValue.Zeta, ButtonInput.Samsung.RB);

			//Axes
			RegisterAxis(InputValue.Alpha, AxisInput.Samsung.LeftX);
			RegisterAxis(InputValue.Beta, AxisInput.Samsung.LeftY);
			RegisterAxis(InputValue.Gamma, AxisInput.Samsung.RightX);
			RegisterAxis(InputValue.Delta, AxisInput.Samsung.RightY);
			RegisterAxis(InputValue.Epsilon, AxisInput.Samsung.DPadX);
			RegisterAxis(InputValue.Zeta, AxisInput.Samsung.DPadY);
		}

		protected override void RegisterAxis(AxisGesture gesture, AxisInput.Samsung axis)
		{
			string input = string.Empty;
			switch (axis)
			{
				case AxisInput.Samsung.LeftX:
					input = "XAxis_";
					break;
				case AxisInput.Samsung.LeftY:
					input = "YAxis_";
					break;
				case AxisInput.Samsung.RightX:
					input = "3rd_Axis_";
					break;
				case AxisInput.Samsung.RightY:
					input = "4th_Axis_";
					break;
				case AxisInput.Samsung.DPadX:
					input = "5th_Axis_";
					break;
				case AxisInput.Samsung.DPadY:
					input = "6th_Axis_";
					break;
			}
			input += m_controllerNumber;

			m_axisBindings.Add(gesture, input);
			if (!m_axisEvents.ContainsKey(gesture))
			{
				m_axisEvents.Add(gesture, delegate { });
			}
		}
	}
}