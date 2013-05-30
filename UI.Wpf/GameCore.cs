using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SemanticDataEnrichment.UI.Wpf
{
	class GameCore
	{
		private int numCount;
		private int checkCount;
		private char[] num;

		public int CheckCount
		{
			get { return this.checkCount; }
		}

		public GameCore(int numCount = 5)
		{
			if (numCount > 10)
				throw new Exception("Число не может быть больше 10 цифр");
			this.numCount = numCount;
			this.num = new char[0];
			this.checkCount = 0;
		}

		public string NewNum()
		{
			this.num = new char[numCount];
			for (int i = 0; i < numCount; i++)
			{
				char newChar = ' ';
				do
				{
					newChar = ((new Random()).Next(9) + 1).ToString()[0];
				}
				while (num.Contains(newChar));
				this.num[i] = newChar;
			}
			this.checkCount = 0;
			return "".PadLeft(numCount, '*');
		}

		public bool IsCorrectNum(string num)
		{
			bool output = true;
			char[] input = GetMass(num);
			for (int i = 0; i < numCount; i++)
			{
				output = output && (input[i] == this.num[i]);
			}
			this.checkCount++;
			return output;
		}

		public string CheckNum(string num)
		{
			char[] input = GetMass(num);
			int countRight = 0;
			int countGood = 0;
			int countBad = 0;
			for (int i = 0; i < numCount; i++)
			{
				if (input[i] == this.num[i])
					countRight++;
				else if (this.num.Contains(input[i]))
					countGood++;
			}
			countBad = this.numCount - (countGood + countRight);
			//Console.ForegroundColor = ConsoleColor.Green;
			//Console.Write("{0} ", countRight);
			//Console.ForegroundColor = ConsoleColor.Yellow;
			//Console.Write("{0} ", countGood);
			//Console.ForegroundColor = ConsoleColor.Red;
			//Console.WriteLine("{0}", countBad);
			//Console.ForegroundColor = ConsoleColor.Gray;
			return string.Format("{0} | {1} | {2}", countRight, countGood, countBad);
		}

		private char[] GetMass(string num)
		{
			if (num.Length != numCount || !Regex.IsMatch(num, "[\\d]{" + numCount.ToString() + "}"))
				throw new Exception("Неверный ввод!");

			return num.ToCharArray();
		}
	}
}
