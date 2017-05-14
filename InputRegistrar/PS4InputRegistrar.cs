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
			m_inputButtonBindings.Add(InputValue.Alpha, ButtonInput.PS4.X);
			m_inputButtonBindings.Add(InputValue.Beta, ButtonInput.PS4.Circle);
			m_inputButtonBindings.Add(InputValue.Gamma, ButtonInput.PS4.Square);
			m_inputButtonBindings.Add(InputValue.Delta, ButtonInput.PS4.Triangle);

			RegisterButtons();

			//Axes
			m_inputAxisBindings.Add(InputValue.Alpha, AxisInput.PS4.LeftX);
			m_inputAxisBindings.Add(InputValue.Beta, AxisInput.PS4.LeftY);
			m_inputAxisBindings.Add(InputValue.Gamma, AxisInput.PS4.RightX);
			m_inputAxisBindings.Add(InputValue.Delta, AxisInput.PS4.RightY);
			m_inputAxisBindings.Add(InputValue.Epsilon, AxisInput.PS4.DPadX);
			m_inputAxisBindings.Add(InputValue.Zeta, AxisInput.PS4.DPadY);
			m_inputAxisBindings.Add(InputValue.Eta, AxisInput.PS4.L2);
			m_inputAxisBindings.Add(InputValue.Theta, AxisInput.PS4.R2);

			RegisterAxes();
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