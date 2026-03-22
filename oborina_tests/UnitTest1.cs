using Microsoft.VisualStudio.TestTools.UnitTesting;
using OborinaLibrary1;
using System;

namespace OborinaTests
{
	[TestClass]
	public class CalcTests
	{
		[TestMethod]
		public void Add_ShouldReturnSum() => Assert.AreEqual(5, Calc.Add(2, 3));

		[TestMethod]
		public void Sub_ShouldReturnDifference() => Assert.AreEqual(2, Calc.Sub(5, 3));

		[TestMethod]
		public void Mul_ShouldReturnProduct() => Assert.AreEqual(20, Calc.Mul(4, 5));

		[TestMethod]
		public void Div_ByNonZero_ShouldReturnQuotient() => Assert.AreEqual(5, Calc.Div(10, 2));

		[TestMethod]
		public void Div_ByZero_ShouldThrowException()
		{
			try
			{
				Calc.Div(5, 0);
			}
			catch (DivideByZeroException ex)
			{
				StringAssert.Contains(ex.Message, "ноль");
				return;
			}
			Assert.Fail("Ожидаемое исключение не было вызвано");
		}

		[TestMethod]
		public void Pow_ShouldReturnPower() => Assert.AreEqual(8, Calc.Pow(2, 3));
	}
}