using System;

namespace OborinaLibrary1
{
	public static class Calc
	{
		public static double Add(double a, double b) => a + b;

		public static double Sub(double a, double b) => a - b;

		public static double Mul(double a, double b) => a * b;

		public static double Div(double a, double b)
		{
			if (b == 0)
			throw new DivideByZeroException("Деление на ноль невозможно");
			return a / b;
		}

		public static double Pow(double a, double b) => Math.Pow(a, b);
	}
}