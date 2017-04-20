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
			RegisterButton(InputValue.Alpha, ButtonInput.Xbox.A);
			RegisterButton(InputValue.Beta, ButtonInput.Xbox.B);
			RegisterButton(InputValue.Gamma, ButtonInput.Xbox.X);
			RegisterButton(InputValue.Delta, ButtonInput.Xbox.Y);

			//Axes
			RegisterAxis(InputValue.Alpha, AxisInput.Xbox.LeftX);
			RegisterAxis(InputValue.Beta, AxisInput.Xbox.LeftY);
			RegisterAxis(InputValue.Gamma, AxisInput.Xbox.RightX);
			RegisterAxis(InputValue.Delta, AxisInput.Xbox.RightY);
			RegisterAxis(InputValue.Epsilon, AxisInput.Xbox.DPadX);
			RegisterAxis(InputValue.Zeta, AxisInput.Xbox.DPadY);
			RegisterAxis(InputValue.Eta, AxisInput.Xbox.LeftTrigger);
			RegisterAxis(InputValue.Theta, AxisInput.Xbox.RightTrigger);
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
			if (!m_axisEvents.ContainsKey(gesture))
			{
				m_axisEvents.Add(gesture, delegate { });
			}
		}
	}
}