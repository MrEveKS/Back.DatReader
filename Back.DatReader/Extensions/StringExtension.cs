using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Back.DatReader.Extensions
{
	public static class StringExtension
	{
		/// <summary>
		/// Split the input string into parts based on uppercase letters
		/// </summary>
		/// <param name="input">input string</param>
		/// <returns>list of parts</returns>
		public static string[] SplitByCamelCase(this string input)
		{
			return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim().Split(' ');
		}

		public static string ToPascalCase(this string input)
		{
			var stringBuilder = new StringBuilder();
			var inputParts = input.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var word in inputParts)
			{
				stringBuilder.Append(char.ToUpper(word[0]));
				for (var charIndex = 1; charIndex < word.Length; charIndex++)
				{
					stringBuilder.Append(word[charIndex]);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
