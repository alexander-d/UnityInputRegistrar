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