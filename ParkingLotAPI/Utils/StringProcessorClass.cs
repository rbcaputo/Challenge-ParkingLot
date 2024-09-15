using System.Globalization;

namespace ParkingLotAPI.Utils
{
	public static class StringProcessorClass
	{
		public static string ToTitleCase(string input)
		{
			if (string.IsNullOrEmpty(input) ||
					string.IsNullOrWhiteSpace(input))
				throw new ArgumentException($"{nameof(ToTitleCase)}: {nameof(input)} cannot be null, empty or white space.");

			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;

			return textInfo.ToTitleCase(input.ToLowerInvariant());
		}
	}
}
