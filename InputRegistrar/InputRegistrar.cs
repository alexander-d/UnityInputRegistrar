using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace InputRegistrar
{
	public enum ButtonAction
	{
		OnPressDown,
		OnPress,
		OnPressUp,
		OnTouchDown,
		OnTouch,
		OnTouchUp
	}

	public enum AxisAction
	{
		GetAxis,
		GetAxisRaw
	}

	public class InputRegistrar
	{
		protected int m_controllerNumber;

		public Dictionary<ButtonGesture, Action> m_buttonEvents;
		public Dictionary<AxisGesture, Action<float>> m_axisEvents;
		public Dictionary<AxisGesture, Action<Vector2>> m_doubleAxisEvents;

		public virtual void Initialise()
		{
			m_buttonEvents = new Dictionary<ButtonGesture, Action>();
			m_axisEvents = new Dictionary<AxisGesture, Action<float>>();
			m_doubleAxisEvents = new Dictionary<AxisGesture, Action<Vector2>>();
		}

		public virtual void ShowBindings() { }
		public virtual void Update() { }
		public virtual void UnbindAll() { }
	}

	public class InputRegistrar<TButton> : InputRegistrar
	{
		protected Dictionary<ButtonGesture, string> m_buttonBindings;

		public override void Initialise()
		{
			base.Initialise();
			m_buttonBindings = new Dictionary<ButtonGesture, string>();
		}

		public override void ShowBindings()
		{
			//TODO
		}

		public override void Update()
		{
			foreach (KeyValuePair<ButtonGesture, string> binding in m_buttonBindings)
			{
				SwitchButtonAction(binding);
			}
		}

		protected virtual void SwitchButtonAction(KeyValuePair<ButtonGesture, string> binding)
		{
			switch (binding.Key.ButtonAction)
			{
				case ButtonAction.OnPressDown:
					if (Input.GetKeyDown(binding.Value))
						m_buttonEvents[binding.Key]();
					break;

				case ButtonAction.OnPress:
					if (Input.GetKey(binding.Value))
						m_buttonEvents[binding.Key]();
					break;

				case ButtonAction.OnPressUp:
					if (Input.GetKeyUp(binding.Value))
						m_buttonEvents[binding.Key]();
					break;
			}
		}

		protected void RegisterButton(InputValue value, TButton button)
		{
			RegisterButton(new ButtonGesture(value, ButtonAction.OnPressDown), button);
			RegisterButton(new ButtonGesture(value, ButtonAction.OnPress), button);
			RegisterButton(new ButtonGesture(value, ButtonAction.OnPressUp), button);
		}

		protected virtual void RegisterButton(ButtonGesture gesture, TButton button)
		{
			if (!typeof(TButton).IsEnum)
			{
				Debug.LogError("TButton is not an enum");
				return;
			}

			string input = string.Empty;
			if (m_controllerNumber == 0)
			{
				input = "joystick button " + (int)Enum.Parse(typeof(TButton), button.ToString());
			}
			else
			{
				input = "joystick " + m_controllerNumber.ToString() + " button " + (int)Enum.Parse(typeof(TButton), button.ToString());
			}
			m_buttonBindings.Add(gesture, input);
			if (!m_buttonEvents.ContainsKey(gesture))
			{
				m_buttonEvents.Add(gesture, delegate { });
			}
		}

		public override void UnbindAll()
		{
			m_buttonBindings.Clear();
			m_buttonEvents.Clear();
		}
	}

	public class InputRegistrar<TButton, TAxis> : InputRegistrar<TButton>
	{
		protected Dictionary<AxisGesture, TAxis> m_axisBindings;

		public override void Initialise()
		{
			base.Initialise();
			m_axisBindings = new Dictionary<AxisGesture, TAxis>();
		}

		public override void ShowBindings()
		{
			//TODO
		}

		public override void Update()
		{
			base.Update();
			foreach (KeyValuePair<AxisGesture, TAxis> binding in m_axisBindings)
			{
				SwitchAxisAction(binding);
			}
		}

		protected virtual void SwitchAxisAction(KeyValuePair<AxisGesture, TAxis> binding) { }

		protected void RegisterAxis(InputValue value, TAxis axis)
		{
			RegisterAxis(new AxisGesture(value, AxisAction.GetAxis), axis);
			RegisterAxis(new AxisGesture(value, AxisAction.GetAxisRaw), axis);
		}

		protected void RegisterAxis(AxisGesture gesture, TAxis axis)
		{
			m_axisBindings.Add(gesture, axis);
			if (!m_axisEvents.ContainsKey(gesture))
			{
				m_axisEvents.Add(gesture, delegate { });
			}
		}

		public override void UnbindAll()
		{
			base.UnbindAll();
			m_axisBindings.Clear();
			m_axisEvents.Clear();
		}
	}

	public class InputRegistrar<TButton, TAxis, T2Axis> : InputRegistrar
	{
		protected Dictionary<AxisGesture, T2Axis> m_doubleAxisBindings;

		public override void Initialise()
		{
			base.Initialise();
			m_doubleAxisBindings = new Dictionary<AxisGesture, T2Axis>();
		}

		public override void ShowBindings()
		{
			//TODO
		}

		public override void Update()
		{
			base.Update();
			foreach (KeyValuePair<AxisGesture, T2Axis> binding in m_doubleAxisBindings)
			{
				SwitchDoubleAxisAction(binding);
			}
		}

		protected virtual void SwitchDoubleAxisAction(KeyValuePair<AxisGesture, T2Axis> binding) { }

		protected void RegisterDoubleAxis(AxisGesture gesture, T2Axis axis)
		{
			m_doubleAxisBindings.Add(gesture, axis);
			if (!m_doubleAxisEvents.ContainsKey(gesture))
			{
				m_doubleAxisEvents.Add(gesture, delegate { });
			}
		}
	}
}