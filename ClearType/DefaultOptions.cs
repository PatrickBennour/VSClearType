using System.Windows.Media;

namespace VSClearType {
	/// <summary>
	/// Werksseitige Standardeinstellungen.
	/// </summary>
	internal static class DefaultOptions {
		/// <summary>
		/// Werksseitige Einstellung ist TextFormattingMode.Ideal.
		/// </summary>
		public const TextFormattingMode Formatting = TextFormattingMode.Ideal;

		/// <summary>
		/// Werksseitige Einstellung ist TextHintingMode.Animated.
		/// </summary>
		public const TextHintingMode Hinting = TextHintingMode.Animated;

		/// <summary>
		/// Werksseitige Einstellung ist TextRenderingMode.ClearType.
		/// </summary>
		public const TextRenderingMode Rendering = TextRenderingMode.ClearType;
	}
}
