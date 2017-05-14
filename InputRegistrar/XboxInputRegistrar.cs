using UnityEngine;
using System.Collections.Generic;

namespace InputRegistrar
{
	public class XboxInputRegistrar : GamepadInputRegistrar<ButtonInput.Xbox, AxisInput.Xbox>
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
			m_inputButtonBindings.Add(InputValue.Alpha, ButtonInput.Xbox.A);
			m_inputButtonBindings.Add(InputValue.Beta, ButtonInput.Xbox.B);
			m_inputButtonBindings.Add(InputValue.Gamma, ButtonInput.Xbox.X);
			m_inputButtonBindings.Add(InputValue.Delta, ButtonInput.Xbox.Y);

			RegisterButtons();

			//Axes
			m_inputAxisBindings.Add(InputValue.Alpha, AxisInput.Xbox.LeftX);
			m_inputAxisBindings.Add(InputValue.Beta, AxisInput.Xbox.LeftY);
			m_inputAxisBindings.Add(InputValue.Gamma, AxisInput.Xbox.RightX);
			m_inputAxisBindings.Add(InputValue.Delta, AxisInput.Xbox.RightY);
			m_inputAxisBindings.Add(InputValue.Epsilon, AxisInput.Xbox.DPadX);
			m_inputAxisBindings.Add(InputValue.Zeta, AxisInput.Xbox.DPadY);
			m_inputAxisBindings.Add(InputValue.Eta, AxisInput.Xbox.LeftTrigger);
			m_inputAxisBindings.Add(InputValue.Theta, AxisInput.Xbox.RightTrigger);

			RegisterAxes();
		}

		protected override void RegisterAxis(AxisGesture gesture, AxisInput.Xbox axis)
		{
			string input = string.Empty;
			switch (axis)
			{
				case AxisInput.Xbox.LeftX:
					input = "XAxis_";
					break;
				case AxisInput.Xbox.LeftY:
					input = "YAxis_";
					break;
				case AxisInput.Xbox.BothTriggers:
					input = "3rd_Axis_";
					break;
				case AxisInput.Xbox.RightX:
					input = "4th_Axis_";
					break;
				case AxisInput.Xbox.RightY:
					input = "5th_Axis_";
					break;
				case AxisInput.Xbox.DPadX:
					input = "6th_Axis_";
					break;
				case AxisInput.Xbox.DPadY:
					input = "7th_Axis_";
					break;
				case AxisInput.Xbox.LeftTrigger:
					input = "9th_Axis_";
					break;
				case AxisInput.Xbox.RightTrigger:
					input = "10th_Axis_";
					break;
			}
			input += m_controllerNumber;

			m_axisBindings.Add(gesture, input);
		}
	}
}