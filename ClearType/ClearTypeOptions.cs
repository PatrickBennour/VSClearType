using System.Windows.Media;

namespace VSClearType {
	/// <summary>
	/// ClearType-Einstellungen inklusive Methoden.
	/// </summary>
	internal class ClearTypeOptions {
		/// <summary>
		/// Definiert die von der TextFormatter-Klasse unterstützten Formatierungsmethoden.
		/// </summary>
		public TextFormattingMode Formatting { get; set; }

		/// <summary>
		/// Definiert das Renderingverhalten von statischem oder animiertem Text.
		/// </summary>
		public TextHintingMode Hinting { get; set; }

		/// <summary>
		/// Definiert die unterstützten Rendermodi für Text.
		/// </summary>
		public TextRenderingMode Rendering { get; set; }

		/// <summary>
		/// Initialisiert neue ClearType-Einstellungen mit Standardeinstellungen,
		/// wenn nichts anderes angegeben wurde.
		/// </summary>
		/// <param name="formatting">Standard Formatting.</param>
		/// <param name="hinting">Standard Hinting.</param>
		/// <param name="rendering">Standard Rendering.</param>
		public ClearTypeOptions(TextFormattingMode? formatting = null, TextHintingMode? hinting = null, TextRenderingMode? rendering = null) {
			Formatting = formatting ?? DefaultOptions.Formatting;
			Hinting = hinting ?? DefaultOptions.Hinting;
			Rendering = rendering ?? DefaultOptions.Rendering;
		}
	}
}
