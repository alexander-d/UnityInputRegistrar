using UnityEngine;
using System.Collections.Generic;

namespace InputRegistrar
{
	public class SamsungGamepadInputRegistrar : InputRegistrar<ButtonInput.Samsung, AxisInput.Samsung>
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

		protected override void SwitchAxisAction(KeyValuePair<AxisGesture, AxisInput.Samsung> binding)
		{
			string axisname = "";
			switch (binding.Value)
			{
				case AxisInput.Samsung.LeftX:
					axisname = "XAxis_";
					break;
				case AxisInput.Samsung.LeftY:
					axisname = "YAxis_";
					break;
				case AxisInput.Samsung.RightX:
					axisname = "3rd_Axis_";
					break;
				case AxisInput.Samsung.RightY:
					axisname = "4th_Axis_";
					break;
				case AxisInput.Samsung.DPadX:
					axisname = "5th_Axis_";
					break;
				case AxisInput.Samsung.DPadY:
					axisname = "6th_Axis_";
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