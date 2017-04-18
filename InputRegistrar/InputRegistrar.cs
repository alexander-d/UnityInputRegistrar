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
		protected Dictionary<ButtonGesture, TButton> m_buttonBindings;

		public override void Initialise()
		{
			base.Initialise();
			m_buttonBindings = new Dictionary<ButtonGesture, TButton>();
		}

		public override void ShowBindings()
		{
			//TODO
		}

		public override void Update()
		{
			foreach (KeyValuePair<ButtonGesture, TButton> binding in m_buttonBindings)
			{
				SwitchButtonAction(binding);
			}
		}

		protected virtual void SwitchButtonAction(KeyValuePair<ButtonGesture, TButton> _binding) { }

		protected void RegisterButton(ButtonGesture _gesture, TButton _button)
		{
			m_buttonBindings.Add(_gesture, _button);
			if (!m_buttonEvents.ContainsKey(_gesture))
			{
				m_buttonEvents.Add(_gesture, delegate { });
			}
		}

		public override void UnbindAll()
		{
			m_buttonBindings.Clear();
			m_buttonEvents.Clear();
		}
	}

	public class InputRegistrar<TButton, TAxis> : InputRegistrar
	{
		protected Dictionary<ButtonGesture, TButton> m_buttonBindings;
		protected Dictionary<AxisGesture, TAxis> m_axisBindings;
		protected Dictionary<AxisGesture, TAxis> m_doubleAxisBindings;

		public override void Initialise()
		{
			base.Initialise();
			m_buttonBindings = new Dictionary<ButtonGesture, TButton>();
			m_axisBindings = new Dictionary<AxisGesture, TAxis>();
			m_doubleAxisBindings = new Dictionary<AxisGesture, TAxis>();
		}

		public override void ShowBindings()
		{
			//TODO
		}

		public override void Update()
		{
			foreach (KeyValuePair<ButtonGesture, TButton> binding in m_buttonBindings)
			{
				SwitchButtonAction(binding);
			}
			foreach (KeyValuePair<AxisGesture, TAxis> binding in m_axisBindings)
			{
				SwitchAxisAction(binding);
			}
			foreach (KeyValuePair<AxisGesture, TAxis> binding in m_doubleAxisBindings)
			{
				SwitchDoubleAxisAction(binding);
			}
		}

		protected virtual void SwitchButtonAction(KeyValuePair<ButtonGesture, TButton> _binding) { }

		protected virtual void SwitchAxisAction(KeyValuePair<AxisGesture, TAxis> _binding) { }

		protected virtual void SwitchDoubleAxisAction(KeyValuePair<AxisGesture, TAxis> _binding) { }

		protected void RegisterButton(ButtonGesture _gesture, TButton _button)
		{
			m_buttonBindings.Add(_gesture, _button);
			if (!m_buttonEvents.ContainsKey(_gesture))
			{
				m_buttonEvents.Add(_gesture, delegate { });
			}
		}

		protected void RegisterAxis(AxisGesture _gesture, TAxis _axis)
		{
			m_axisBindings.Add(_gesture, _axis);
			if (!m_axisEvents.ContainsKey(_gesture))
			{
				m_axisEvents.Add(_gesture, delegate { });
			}
		}

		protected void RegisterDoubleAxis(AxisGesture _gesture, TAxis _axis)
		{
			m_doubleAxisBindings.Add(_gesture, _axis);
			if (!m_doubleAxisEvents.ContainsKey(_gesture))
			{
				m_doubleAxisEvents.Add(_gesture, delegate { });
			}
		}

		public override void UnbindAll()
		{
			m_buttonBindings.Clear();
			m_buttonEvents.Clear();
			m_axisBindings.Clear();
			m_axisEvents.Clear();
		}
	}

	public class InputRegistrar<TButton, TAxis, T2Axis> : InputRegistrar
	{
		protected Dictionary<ButtonGesture, TButton> m_buttonBindings;
		protected Dictionary<AxisGesture, TAxis> m_axisBindings;
		protected Dictionary<AxisGesture, T2Axis> m_doubleAxisBindings;

		public override void Initialise()
		{
			base.Initialise();
			m_buttonBindings = new Dictionary<ButtonGesture, TButton>();
			m_axisBindings = new Dictionary<AxisGesture, TAxis>();
			m_doubleAxisBindings = new Dictionary<AxisGesture, T2Axis>();
		}

		public override void ShowBindings()
		{
			//TODO
		}

		public override void Update()
		{
			foreach (KeyValuePair<ButtonGesture, TButton> binding in m_buttonBindings)
			{
				SwitchButtonAction(binding);
			}
			foreach (KeyValuePair<AxisGesture, TAxis> binding in m_axisBindings)
			{
				SwitchAxisAction(binding);
			}
			foreach (KeyValuePair<AxisGesture, T2Axis> binding in m_doubleAxisBindings)
			{
				SwitchDoubleAxisAction(binding);
			}
		}

		protected virtual void SwitchButtonAction(KeyValuePair<ButtonGesture, TButton> binding) { }

		protected virtual void SwitchAxisAction(KeyValuePair<AxisGesture, TAxis> binding) { }

		protected virtual void SwitchDoubleAxisAction(KeyValuePair<AxisGesture, T2Axis> binding) { }

		protected void RegisterButton(ButtonGesture gesture, TButton button)
		{
			m_buttonBindings.Add(gesture, button);
			if (!m_buttonEvents.ContainsKey(gesture))
			{
				m_buttonEvents.Add(gesture, delegate { });
			}
		}

		protected void RegisterAxis(AxisGesture gesture, TAxis axis)
		{
			m_axisBindings.Add(gesture, axis);
			if (!m_axisEvents.ContainsKey(gesture))
			{
				m_axisEvents.Add(gesture, delegate { });
			}
		}

		protected void RegisterDoubleAxis(AxisGesture gesture, T2Axis axis)
		{
			m_doubleAxisBindings.Add(gesture, axis);
			if (!m_doubleAxisEvents.ContainsKey(gesture))
			{
				m_doubleAxisEvents.Add(gesture, delegate { });
			}
		}

		public override void UnbindAll()
		{
			m_buttonBindings.Clear();
			m_buttonEvents.Clear();
			m_axisBindings.Clear();
			m_axisEvents.Clear();
		}
	}
}