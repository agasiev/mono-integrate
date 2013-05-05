using System;
using System.ComponentModel;

namespace trapezoidalmethod
{
	public class TrapezoidalMethod : Component
	{
		private double m_from;
		private double m_to;
		private double m_epsilon;
		private Func<double, double> m_calc;

		public TrapezoidalMethod ()
		{
			setValues(0, 0, 0, null);
		}

		public TrapezoidalMethod (double _from, double _to, double _epsilon, Func<double, double> _f)
		{
			setValues(_from, _to, _epsilon, _f);
		}

		public void setValues (double _from, double _to, double _epsilon, Func<double, double> _f)
		{
			m_from = _from;
			m_to = _to;
			m_epsilon = _epsilon;
			m_calc = _f;
		}

		public double integrate ()
		{
			checkValues();

			int n = getNumberOfSteps ();
			n = (n == 0 ? 1 : n);
			double h = (m_to - m_from) / n;
			double result = (m_calc(m_from) + m_calc(m_to))/2.0;

			for (int i = 1; i < n; i++) {
				result += m_calc(m_from + h * i);
			}

			return h * result;
		}

		private int getNumberOfSteps ()
		{
			return (int)(Math.Sqrt((Math.Pow(m_to - m_from, 3.0)*secondDerivative())/(12.0 * m_epsilon)));
		}

		private double secondDerivative ()
		{
			double h = (m_to - m_from) / 100.0;
			double res = 0;

			for (double i = m_from; i < m_to; i += h) {
				double v = (m_calc(i + 2*h) - 2*m_calc(i + h) + m_calc(i)) / (h*h);
				res = Math.Max(res, Math.Abs(v));
			}

			return res;
		}

		private void checkValues ()
		{
			if (m_to == m_from) {
				throw new System.ArgumentException("Параметры должны различаться.", "m_to, m_from");
			}

			if (m_to < m_from) {
				throw new System.ArgumentException("Параметр m_to < m_from.", "m_to, m_from");
			}

			if (m_epsilon == 0) {
				throw new System.ArgumentException("Параметр не может быть равен нулю.", "m_epsilon");
			}

			if (m_calc == null) {
				throw new System.ArgumentException("Функция интегрирования должна быть задана.", "m_calc");
			}
		}

		protected override void Dispose(bool dispAll) {
			Console.WriteLine("Dispose для компонента TrapezoidalMethod");
			base.Dispose(dispAll);
        }
	}
}

