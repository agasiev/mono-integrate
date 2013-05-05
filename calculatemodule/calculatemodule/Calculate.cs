using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace calculatemodule
{
	public class Calculate : Component
	{
		string m_function;
		delegate double function_delegate(double param);
		IDictionary<string, function_delegate> dict;
		List<string> result;
		string integrateVariable;

		public Calculate ()
		{
			m_function = "";
			initFunctionTable();
		}

		public Calculate (string function, string var)
		{
			setFunction(function, var);
			initFunctionTable();
		}

		private void initFunctionTable ()
		{
			dict = new Dictionary<string, function_delegate>();
			dict["sin"] = Math.Sin;
			dict["cos"] = Math.Cos;
			dict["abs"] = Math.Abs;
			dict["tan"] = Math.Tan;
			dict["acos"] = Math.Acos;
			dict["asin"] = Math.Asin;
			dict["atan"] = Math.Atan;
			dict["ceiling"] = Math.Ceiling;
			dict["sinh"] = Math.Sinh;
			dict["cosh"] = Math.Cosh;
			dict["ln"] = Math.Log;
			dict["log10"] = Math.Log10;
			dict["sqrt"] = Math.Sqrt;
			dict["round"] = Math.Round;
			dict["floor"] = Math.Floor;
			dict["exp"] = Math.Exp;
			dict["neg"] = MathNeg;
		}

		private double MathNeg (double val)
		{
			return (-val);
		}

		public bool setFunction (string function, string var)
		{
			m_function = function.ToLower() + " ";
			integrateVariable = var;
			return makeReversePolishNotation();
		}

		public double calculateAtPoint (double point)
		{
			Stack<double> st = new Stack<double>();
			string operators = "+-*/^";

			foreach (string item in result) {
				if (operators.IndexOf(item) != -1) {
					if (st.Count < 2) {
						throw new ArgumentException("Неверное количество параметров для оператора.", "st.Count < 2");
					}

					double a = st.Peek();
					st.Pop();
					double b = st.Peek();
					st.Pop();

					if (item == "+") {
						st.Push(a + b);
					}
					if (item == "-") {
						st.Push(b - a);
					}
					if (item == "*") {
						st.Push(a * b);
					}
					if (item == "/") {
						if (a == 0) return Double.NaN;
						else st.Push(b / a);
					}
					if (item == "^") {
						st.Push(Math.Pow(b, a));
					}
				}
				else {
					double Num;
					bool isNum = double.TryParse (item, out Num);

					if (isNum) {
						st.Push(Num);
						continue;
					}
					else {
						if (item == integrateVariable) {
							st.Push(point);
						}
						else {
							double d = dict[item].Invoke(st.Peek());
							st.Pop();
							st.Push(d);
						}
					}
				}
			}

			return st.Peek();
		}

		private List<string> parseFunction ()
		{
			List<string> arr = new List<string> ();

			// tokenize
			string token = "";
			//string accepted = "abcdefghijklmnopqrstuvwxyz0123456789";
			string delimiter = "\t\n -+*/^()";
			string operators = "+-*/^";

			for (int i = 0; i < m_function.Length; i++) {
				if (delimiter.IndexOf(m_function[i]) != -1) {
					if (token.Length > 0) {
						arr.Add(token);
						token = "";
					}

					if (operators.IndexOf(m_function[i]) != -1 || "()".IndexOf(m_function[i]) != -1) {
						arr.Add(m_function[i].ToString());
					}
				}
				else {
					token += m_function[i];
				}
			}

			return arr;
		}

		private int getOperatorPriority (string op)
		{
			if (op == "^") return 4;
			if (op == "*" || op == "/") return 3;
			if (op == "+" || op == "-") return 2;
			return 0;
		}

		private bool makeReversePolishNotation ()
		{
			List<string> arr = parseFunction ();
			result = new List<string> ();
			Stack<string> st = new Stack<string> ();

			string operators = "+-*/^";

			foreach (string token in arr) {
				double Num;
				bool isNum = double.TryParse (token, out Num);

				if (isNum || token == integrateVariable) {
					result.Add (token);
					continue;
				}

				if (operators.IndexOf(token) == -1 && token != "(" && token != ")") {
					st.Push(token);
					continue;
				}

				if (token == "(") {
					st.Push (token);
					continue;
				}

				if (token == ")") {
					if (st.Count == 0) {
						throw new ArgumentException("Ошибка. Неверная скобочная последовательность.", "st.Count == 0");
					}

					while (st.Count > 0) {
						if (st.Peek () == "(") {
							st.Pop ();
							if (st.Count > 0 && st.Peek() != integrateVariable) {
								result.Add(st.Peek());
								st.Pop();
							}
							break;
						} else {
							result.Add (st.Peek ());
							st.Pop ();
						}
					}

					continue;
				}

				if (operators.IndexOf (token) != -1) {
					while (st.Count > 0 && getOperatorPriority(token) < getOperatorPriority(st.Peek())) {
						result.Add (st.Peek ());
						st.Pop ();
					}

					st.Push (token);
					continue;
				}
			}

			while (st.Count > 0) {
				result.Add (st.Peek ());
				if (operators.IndexOf (st.Peek ()) == -1) {
					throw new ArgumentException("Неверная скобочная последовательность.");
				}
				st.Pop ();
			}

			return true;
		}

		protected override void Dispose(bool dispAll) {
			Console.WriteLine("Dispose для компонента CalculateModule");
			base.Dispose(dispAll);
        }
	}
}

