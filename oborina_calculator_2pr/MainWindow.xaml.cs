using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace OborinaCalculator2PR
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string text = ((Button)sender).Content.ToString();

			if (text == "C")
			{
				tbCommand.Clear();
			}
			else if (text == "=")
			{
				try
				{
					string input = tbCommand.Text;

					double result = EvaluateExpression(input);

					tbCommand.Text = result.ToString(CultureInfo.InvariantCulture);
				}
				catch (DivideByZeroException ex)
				{
					tbCommand.Text = ex.Message;
				}
				catch (Exception ex)
				{
					tbCommand.Text = "Ошибка: " + ex.Message;
				}
			}
			else
			{
				tbCommand.Text += text;
			}
		}

		private void Button_Clear_Click(object sender, RoutedEventArgs e)
		{
			tbCommand.Clear();
		}

		private void Button_Equals_Click(object sender, RoutedEventArgs e)
		{
			Button_Click(sender, e);
		}
		private double EvaluateExpression(string input)
		{
			while (input.Contains("^"))
			{
				int index = input.IndexOf('^');

				int leftStart = index - 1;
				int bracketCount = 0;
				if (input[leftStart] == ')') 
				{
					bracketCount = 1;
					leftStart--;
					while (leftStart >= 0 && bracketCount > 0)
					{
						if (input[leftStart] == ')') bracketCount++;
						if (input[leftStart] == '(') bracketCount--;
						leftStart--;
					}
					leftStart++;
				}
				else
				{
					while (leftStart >= 0 && (char.IsDigit(input[leftStart]) || input[leftStart] == '.')) leftStart--;
					leftStart++;
				}
				int rightEnd = index + 1;
				if (input[rightEnd] == '(')
				{
					bracketCount = 1;
					rightEnd++;
					while (rightEnd < input.Length && bracketCount > 0)
					{
						if (input[rightEnd] == '(') bracketCount++;
						if (input[rightEnd] == ')') bracketCount--;
						rightEnd++;
					}
				}
				else
				{
					while (rightEnd < input.Length && (char.IsDigit(input[rightEnd]) || input[rightEnd] == '.')) rightEnd++;
				}

				string leftStr = input.Substring(leftStart, index - leftStart);
				string rightStr = input.Substring(index + 1, rightEnd - index - 1);

				double left = EvaluateExpression(leftStr);
				double right = EvaluateExpression(rightStr);

				double powResult = Math.Pow(left, right);

				input = input.Substring(0, leftStart) +
						powResult.ToString(CultureInfo.InvariantCulture) +
						input.Substring(rightEnd);
			}
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == '/')
				{
					int j = i + 1;
					while (j < input.Length && input[j] == ' ') j++; 

					if (j < input.Length)
					{
						if (input[j] != '(')
						{
							int k = j;
							while (k < input.Length && (char.IsDigit(input[k]) || input[k] == '.')) k++;
							string rightOperand = input.Substring(j, k - j);
							if (double.Parse(rightOperand, CultureInfo.InvariantCulture) == 0)
								throw new DivideByZeroException("Деление на ноль невозможно");
						}
					}
				}
			}
			var dt = new DataTable();
			dt.Locale = CultureInfo.InvariantCulture;
			return Convert.ToDouble(dt.Compute(input, ""));
		}
	}
}