using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
//using XInputDotNetPure;

namespace InputRegistrar
{
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

	public struct ButtonGesture
	{
		public InputValue InputValue;
		public ButtonAction ButtonAction;

		public ButtonGesture(InputValue value, ButtonAction action)
		{
			InputValue = value;
			ButtonAction = action;
		}
	}

	public struct AxisGesture
	{
		public InputValue InputValue;
		public AxisAction AxisAction;

		public AxisGesture(InputValue value, AxisAction action)
		{
			InputValue = value;
			AxisAction = action;
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
		public InputRegistrar m_inputRegistrar;

		public void Update()
		{
			m_inputRegistrar.Update();
		}

		public void UnregisterAllInputs()
		{
			m_inputRegistrar.UnregisterAllInputs();
		}

		public void RegisterButtonInput(ButtonGesture gesture, Action action)
		{
			m_inputRegistrar.m_buttonEvents[gesture] = action;
		}

		public void RegisterAxisInput(AxisGesture gesture, Action<float> action)
		{
			m_inputRegistrar.m_axisEvents[gesture] = action;
		}

		public void RegisterDoubleAxisInput(AxisGesture gesture, Action<Vector2> action)
		{
			m_inputRegistrar.m_doubleAxisEvents[gesture] = action;
		}

		public void UnregisterButtonInput(ButtonGesture gesture)
		{
			m_inputRegistrar.m_buttonEvents.Remove(gesture);
		}

		public void UnregisterAxisInput(AxisGesture gesture)
		{
			m_inputRegistrar.m_axisEvents.Remove(gesture);
		}

		public void UnregisterDoubleAxisInput(AxisGesture gesture)
		{
			m_inputRegistrar.m_doubleAxisEvents.Remove(gesture);
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