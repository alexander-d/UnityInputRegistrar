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
		
		public override void UnbindAll()
		{
			m_buttonEvents.Clear();
		}
	}

	public class InputRegistrar<TButton, TAxis> : InputRegistrar<TButton>
	{
		public override void ShowBindings()
		{
			//TODO
		}

		public override void Update()
		{
			base.Update();
		}		

		public override void UnbindAll()
		{
			base.UnbindAll();
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