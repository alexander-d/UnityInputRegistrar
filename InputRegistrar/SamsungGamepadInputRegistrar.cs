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
			m_inputButtonBindings.Add(InputValue.Alpha, ButtonInput.Samsung.One);
			m_inputButtonBindings.Add(InputValue.Beta, ButtonInput.Samsung.Two);
			m_inputButtonBindings.Add(InputValue.Gamma, ButtonInput.Samsung.Three);
			m_inputButtonBindings.Add(InputValue.Delta, ButtonInput.Samsung.Four);

			//Shoulder Buttons
			m_inputButtonBindings.Add(InputValue.Epsilon, ButtonInput.Samsung.LB);
			m_inputButtonBindings.Add(InputValue.Zeta, ButtonInput.Samsung.RB);

			RegisterButtons();

			//Axes
			m_inputAxisBindings.Add(InputValue.Alpha, AxisInput.Samsung.LeftX);
			m_inputAxisBindings.Add(InputValue.Beta, AxisInput.Samsung.LeftY);
			m_inputAxisBindings.Add(InputValue.Gamma, AxisInput.Samsung.RightX);
			m_inputAxisBindings.Add(InputValue.Delta, AxisInput.Samsung.RightY);
			m_inputAxisBindings.Add(InputValue.Epsilon, AxisInput.Samsung.DPadX);
			m_inputAxisBindings.Add(InputValue.Zeta, AxisInput.Samsung.DPadY);

			RegisterAxes();
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
		}
	}
}