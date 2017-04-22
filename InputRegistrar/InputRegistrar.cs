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
		public override void ShowBindings()
		{
			//TODO
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

		public override void UnbindAll()
		{
			ButtonGesture[] buttonGestures = m_buttonEvents.Keys.ToArray();
			for (int i = 0; i < buttonGestures.Length; i++)
			{
				m_buttonEvents[buttonGestures[i]] = delegate { };
			}
		}
	}

	public class InputRegistrar<TButton, TAxis> : InputRegistrar<TButton>
	{
		public override void ShowBindings()
		{
			//TODO
		}

		protected void RegisterAxis(InputValue value, TAxis axis)
		{
			RegisterAxis(new AxisGesture(value, AxisAction.GetAxis), axis);
			RegisterAxis(new AxisGesture(value, AxisAction.GetAxisRaw), axis);
		}

		protected virtual void RegisterAxis(AxisGesture gesture, TAxis axis) { }

		public override void UnbindAll()
		{
			base.UnbindAll();
			AxisGesture[] axisGestures = m_axisEvents.Keys.ToArray();
			for (int i = 0; i < axisGestures.Length; i++)
			{
				m_axisEvents[axisGestures[i]] = delegate { };
			}
		}
	}

	public class InputRegistrar<TButton, TAxis, T2Axis> : InputRegistrar<TButton, TAxis>
	{

		public override void ShowBindings()
		{
			//TODO
		}

		protected void RegisterDoubleAxis(InputValue value, T2Axis axis)
		{
			RegisterDoubleAxis(new AxisGesture(value, AxisAction.GetAxis), axis);
		}

		protected virtual void RegisterDoubleAxis(AxisGesture gesture, T2Axis axis) { }

		public override void UnbindAll()
		{
			base.UnbindAll();
			AxisGesture[] doubleAxisGestures = m_doubleAxisEvents.Keys.ToArray();
			for (int i = 0; i < doubleAxisGestures.Length; i++)
			{
				m_doubleAxisEvents[doubleAxisGestures[i]] = delegate { };
			}
		}
	}
}