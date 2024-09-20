using System.Globalization;

namespace ParkingLotAPI.Utils
{
	public static class FormaterClass
	{
		public static string ToUpperCase(string input)
		{
			if (string.IsNullOrEmpty(input) ||
					string.IsNullOrWhiteSpace(input))
				throw new ArgumentException($"{nameof(FormaterClass)}: {nameof(ToUpperCase)}: {nameof(input)} cannot be null, empty or white space.");

			return input.ToUpper();
		}

		public static string ToTitleCase(string input)
		{
			if (string.IsNullOrEmpty(input) ||
					string.IsNullOrWhiteSpace(input))
				throw new ArgumentException($"{nameof(FormaterClass)}: {nameof(ToTitleCase)}: {nameof(input)} cannot be null, empty or white space.");

			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
		}
	}
}
