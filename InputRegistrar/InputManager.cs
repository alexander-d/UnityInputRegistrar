using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
//using XInputDotNetPure;

namespace InputRegistrar
{
	[System.Serializable]
	public enum InputMethod
	{
		Keyboard,
		Xbox,
		PS4,
		Vive,
		Oculus,
		SamsungGamepad
	}

	public class ButtonInput
	{
		public enum Xbox
		{
			A = 0,
			B = 1,
			X = 2,
			Y = 3,
			LB = 4,
			RB = 5,
			Back = 6,
			Start = 7,
			L3 = 8,
			R3 = 9
		}

		public enum PS4
		{
			Square = 0,
			X = 1,
			Circle = 2,
			Triangle = 3,
			L1 = 4,
			R1 = 5,
			L2 = 6,
			R2 = 7,
			Share = 8,
			Options = 9,
			L3 = 10,
			R3 = 11,
			PS = 12,
			PadPress = 13
		}

		public enum Samsung
		{
			One = 0,
			Two = 1,
			Three = 2,
			Four = 3,
			LB = 4,
			RB = 5
		}
	}

	public class AxisInput
	{
		public enum Xbox
		{
			LeftX = 0,
			LeftY = 1,
			BothTriggers = 3,
			RightX = 4,
			RightY = 5,
			DPadX = 6,
			DPadY = 7,
			LeftTrigger = 9,
			RightTrigger = 10
		}

		public enum PS4
		{
			LeftX = 0,
			LeftY = 1,
			RightX = 3,
			RightY = 4,
			L2 = 5,
			R2 = 6,
			DPadX = 7,
			DPadY = 8
		}

		public enum Samsung
		{
			LeftX = 0,
			LeftY = 1,
			RightX = 3,
			RightY = 4,
			DPadX = 5,
			DPadY = 6
		}
	}

	public enum InputValue
	{
		Alpha,
		Beta,
		Gamma,
		Delta,
		Epsilon,
		Zeta,
		Eta,
		Theta,
		Iota,
		Kappa,
		Lambda,
		Mu,
		Nu,
		Xi,
		Omikron,
		Pi,
		Rho,
		Sigma,
		Tau,
		Upsilon,
		Phi,
		Chi,
		Psi,
		Omega
	}

	public class ButtonGesture
	{
		public InputValue InputValue;
		public ButtonAction ButtonAction;

		public ButtonGesture(InputValue value, ButtonAction action)
		{
			InputValue = value;
			ButtonAction = action;
		}

		public static bool operator ==(ButtonGesture thisGesture, ButtonGesture otherGesture)
		{
			if (thisGesture.ButtonAction == otherGesture.ButtonAction &&
				thisGesture.InputValue == otherGesture.InputValue)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(ButtonGesture thisGesture, ButtonGesture otherGesture)
		{
			if (thisGesture.ButtonAction == otherGesture.ButtonAction &&
				thisGesture.InputValue == otherGesture.InputValue)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

	public class AxisGesture
	{
		public InputValue InputValue;
		public AxisAction AxisAction;

		public AxisGesture(InputValue value, AxisAction action)
		{
			InputValue = value;
			AxisAction = action;
		}

		public static bool operator ==(AxisGesture thisGesture, AxisGesture otherGesture)
		{
			if (thisGesture.AxisAction == otherGesture.AxisAction &&
				thisGesture.InputValue == otherGesture.InputValue)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(AxisGesture thisGesture, AxisGesture otherGesture)
		{
			if (thisGesture.AxisAction == otherGesture.AxisAction &&
				thisGesture.InputValue == otherGesture.InputValue)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

	public class InputManager<TInputRegistrar, TController> : InputManager where TInputRegistrar : InputRegistrar
	{
		public InputManager(TController controllerNumber)
		{
			if (m_inputRegistrar != null)
			{
				Debug.LogWarning("Attempting to initialise an already initialised input manager");
				return;
			}

			System.Reflection.ConstructorInfo constructor = typeof(TInputRegistrar).GetConstructor(new System.Type[] { typeof(TController) });
			if (constructor == null)
			{
				throw new System.InvalidOperationException("Type " + typeof(TInputRegistrar).Name + " does not contain an appropriate constructor");
			}
			m_inputRegistrar = constructor.Invoke(new object[] { controllerNumber }) as TInputRegistrar;
		}
	}

	public class InputManager<TInputRegistrar> : InputManager where TInputRegistrar : InputRegistrar
	{
		public InputManager()
		{
			if (m_inputRegistrar != null)
			{
				Debug.LogWarning("Attempting to initialise an already initialised input manager");
				return;
			}

			System.Reflection.ConstructorInfo constructor = typeof(TInputRegistrar).GetConstructor(System.Type.EmptyTypes);
			if (constructor == null)
			{
				throw new System.InvalidOperationException("Type " + typeof(TInputRegistrar).Name + " does not contain an appropriate constructor");
			}
			m_inputRegistrar = constructor.Invoke(new object[] { }) as TInputRegistrar;
		}
	}

	public class InputManager
	{
		protected InputRegistrar m_inputRegistrar;

		public void Update()
		{
			if (m_inputRegistrar != null)
				m_inputRegistrar.Update();
		}

		public void UnbindAllInputs()
		{
			//TODO: does not work right now
			m_inputRegistrar.UnbindAll();
		}

		public void UnregisterAllInputs()
		{
			if (m_inputRegistrar != null)
			{
				m_inputRegistrar.UnbindAll();
				m_inputRegistrar = null;
			}
		}

		public void RegisterButtonInput(ButtonGesture _gesture, Action _event)
		{
			foreach (ButtonGesture gesture in m_inputRegistrar.m_buttonEvents.Keys.ToArray())
			{
				if (gesture == _gesture)
				{
					m_inputRegistrar.m_buttonEvents[gesture] += _event;
					break;
				}
			}
		}

		public void RegisterAxisInput(AxisGesture _gesture, Action<float> _event)
		{
			foreach (AxisGesture gesture in m_inputRegistrar.m_axisEvents.Keys.ToArray())
			{
				if (gesture == _gesture)
				{
					m_inputRegistrar.m_axisEvents[gesture] += _event;
					break;
				}
			}
		}

		public void RegisterAxisInput(AxisGesture _gesture, Action<Vector2> _event)
		{
			foreach (AxisGesture gesture in m_inputRegistrar.m_doubleAxisEvents.Keys.ToArray())
			{
				if (gesture == _gesture)
				{
					m_inputRegistrar.m_doubleAxisEvents[gesture] += _event;
					break;
				}
			}
		}

		public void UnregisterButtonInput(ButtonGesture _gesture)
		{
			foreach (ButtonGesture gesture in m_inputRegistrar.m_buttonEvents.Keys.ToArray())
			{
				if (gesture == _gesture)
				{
					m_inputRegistrar.m_buttonEvents[gesture] = delegate { };
					break;
				}
			}
		}

		public void UnregisterButtonInput(ButtonGesture _gesture, Action _event)
		{
			foreach (ButtonGesture gesture in m_inputRegistrar.m_buttonEvents.Keys.ToArray())
			{
				if (gesture == _gesture)
				{
					m_inputRegistrar.m_buttonEvents[gesture] -= _event;
					break;
				}
			}
		}

		public void UnregisterAxisInput(AxisGesture _gesture)
		{
			foreach (AxisGesture gesture in m_inputRegistrar.m_axisEvents.Keys.ToArray())
			{
				if (gesture == _gesture)
				{
					m_inputRegistrar.m_axisEvents[gesture] = delegate { };
					break;
				}
			}
		}

		public void UnregisterAxisInput(AxisGesture _gesture, Action<float> _event)
		{
			foreach (AxisGesture gesture in m_inputRegistrar.m_axisEvents.Keys.ToArray())
			{
				if (gesture == _gesture)
				{
					m_inputRegistrar.m_axisEvents[gesture] -= _event;
					break;
				}
			}
		}

		public void UnregisterAxisInput(AxisGesture _gesture, Action<Vector2> _event)
		{
			foreach (AxisGesture gesture in m_inputRegistrar.m_doubleAxisEvents.Keys.ToArray())
			{
				if (gesture == _gesture)
				{
					m_inputRegistrar.m_doubleAxisEvents[gesture] -= _event;
					break;
				}
			}
		}

		public void VibrateController(float _intensity)
		{
			// getting most controllers to vibrate is more work
			// xbox controller can be done using XInputDotNetPure
			//if (controllerNumber > 0)
			//{
			//    switch (inputMethod)
			//    {
			//        case InputMethod.Xbox:
			//            //PlayerIndex pi = (PlayerIndex)(controllerNumber - 1);
			//            //GamePad.SetVibration(pi, _intensity, _intensity);
			//            break;
			//        case InputMethod.PS4:
			//            break;
			//        case InputMethod.Vive:
			//            _intensity *= 3999f;
			//            viveInputRegistrar.VibrateController((ushort)_intensity);
			//            break;
			//        case InputMethod.Oculus:
			//            oculusInputRegistrar.VibrateController(_intensity);
			//            break;
			//    }
			//}
		}
	}
}