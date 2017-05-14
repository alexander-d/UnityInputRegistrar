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

		public virtual void Update() { }
		public virtual void UnregisterAllInputs() { }
	}

	public class InputRegistrar<TButton> : InputRegistrar
	{
		public Dictionary<InputValue, TButton> m_inputButtonBindings;

		public override void Initialise()
		{
			base.Initialise();
			m_inputButtonBindings = new Dictionary<InputValue, TButton>();
		}

		public override void Update()
		{
			foreach (KeyValuePair<ButtonGesture, Action> binding in m_buttonEvents)
			{
				CheckForButtonInput(binding);
			}
		}

		protected virtual void CheckForButtonInput(KeyValuePair<ButtonGesture, Action> binding) { }

		protected void RegisterButtons()
		{
			foreach (KeyValuePair<InputValue, TButton> buttonBinding in m_inputButtonBindings)
			{
				RegisterButton(buttonBinding.Key, buttonBinding.Value);
			}
		}

		protected void RegisterButton(InputValue value, TButton button)
		{
			RegisterButton(new ButtonGesture(value, ButtonAction.OnPressDown), button);
			RegisterButton(new ButtonGesture(value, ButtonAction.OnPress), button);
			RegisterButton(new ButtonGesture(value, ButtonAction.OnPressUp), button);
		}

		protected void RegisterTouchButton(InputValue value, TButton button)
		{
			RegisterButton(new ButtonGesture(value, ButtonAction.OnTouchDown), button);
			RegisterButton(new ButtonGesture(value, ButtonAction.OnTouch), button);
			RegisterButton(new ButtonGesture(value, ButtonAction.OnTouchUp), button);
		}

		protected virtual void RegisterButton(ButtonGesture gesture, TButton button) { }

		public override void UnregisterAllInputs()
		{
			ButtonGesture[] buttonGestures = m_buttonEvents.Keys.ToArray();
			for (int i = 0; i < buttonGestures.Length; i++)
			{
				m_buttonEvents.Remove(buttonGestures[i]);
			}
		}
	}

	public class InputRegistrar<TButton, TAxis> : InputRegistrar<TButton>
	{
		public Dictionary<InputValue, TAxis> m_inputAxisBindings;

		public override void Initialise()
		{
			base.Initialise();
			m_inputAxisBindings = new Dictionary<InputValue, TAxis>();
		}

		public override void Update()
		{
			base.Update();
			foreach (KeyValuePair<AxisGesture, Action<float>> binding in m_axisEvents)
			{
				CheckForAxisInput(binding);
			}
		}
		
		protected virtual void CheckForAxisInput(KeyValuePair<AxisGesture, Action<float>> binding) { }

		protected void RegisterAxes()
		{
			foreach (KeyValuePair<InputValue, TAxis> axisBinding in m_inputAxisBindings)
			{
				RegisterAxis(axisBinding.Key, axisBinding.Value);
			}
		}

		protected void RegisterAxis(InputValue value, TAxis axis)
		{
			RegisterAxis(new AxisGesture(value, AxisAction.GetAxis), axis);
			RegisterAxis(new AxisGesture(value, AxisAction.GetAxisRaw), axis);
		}

		protected virtual void RegisterAxis(AxisGesture gesture, TAxis axis) { }

		public override void UnregisterAllInputs()
		{
			base.UnregisterAllInputs();
			AxisGesture[] axisGestures = m_axisEvents.Keys.ToArray();
			for (int i = 0; i < axisGestures.Length; i++)
			{
				m_axisEvents.Remove(axisGestures[i]);
			}
		}
	}

	public class InputRegistrar<TButton, TAxis, T2Axis> : InputRegistrar<TButton, TAxis>
	{
		public override void Update()
		{
			base.Update();
			foreach (KeyValuePair<AxisGesture, Action<Vector2>> binding in m_doubleAxisEvents)
			{
				CheckForDoubleAxisInput(binding);
			}
		}

		protected virtual void CheckForDoubleAxisInput(KeyValuePair<AxisGesture, Action<Vector2>> binding) { }

		protected void RegisterDoubleAxis(InputValue value, T2Axis axis)
		{
			RegisterDoubleAxis(new AxisGesture(value, AxisAction.GetAxis), axis);
		}

		protected virtual void RegisterDoubleAxis(AxisGesture gesture, T2Axis axis) { }

		public override void UnregisterAllInputs()
		{
			base.UnregisterAllInputs();
			AxisGesture[] doubleAxisGestures = m_doubleAxisEvents.Keys.ToArray();
			for (int i = 0; i < doubleAxisGestures.Length; i++)
			{
				m_doubleAxisEvents.Remove(doubleAxisGestures[i]);
			}
		}
	}
}