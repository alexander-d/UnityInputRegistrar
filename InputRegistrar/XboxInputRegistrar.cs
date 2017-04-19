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