using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Microsoft.VisualStudio.PlatformUI;

namespace VSClearType {
	/// <summary>
	/// ClearType-Konfigurationseinstellungen.
	/// </summary>
	internal class ConfigurationSettings {
		/// <summary>
		/// ClearType-Einstellungen.
		/// </summary>
		public ClearTypeOptions Options { get; private set; }

		/// <summary>
		/// Minimale Höhe oder Breite des Framework Elements.
		/// </summary>
		private int minSize = 10;

		/// <summary>
		/// Instanziierung der ClearType-Einstellungen.
		/// </summary>
		public ConfigurationSettings() {
			Options = new ClearTypeOptions();
		}

		/// <summary>
		/// Wende ClearType-Einstellungen auf Element an.
		/// </summary>
		/// <param name="element">Element, auf das die ClearType-Einstellungen angewendet werden sollen.</param>
		private void AdjustObject(FrameworkElement element) {
			if (element != null && element.ActualHeight >= minSize && element.ActualWidth >= minSize) {
				TextOptions.SetTextFormattingMode(element, Options.Formatting);
				TextOptions.SetTextRenderingMode(element, Options.Rendering);
				TextOptions.SetTextHintingMode(element, Options.Hinting);
			}
		}

		/// <summary>
		/// Wende ClearType-Einstellungen auf alle Elemente des visuellen Baumes an.
		/// </summary>
		/// <param name="element">
		/// Wurzel-Element des visuellen Baumes, auf das die ClearType-Einstellungen 
		/// (inkusive Wurzel-Element) angewendet werden sollen.
		/// </param>
		private void AdjustVisualTree(FrameworkElement element) {
			if (element != null) {
				AdjustObject(element);
				element.TraverseVisualTree<FrameworkElement>(frameworkElement => AdjustObject(frameworkElement));
			}
		}

		/// <summary>
		/// Wende ClearType-Einstellungen auf alle Framework Elemente 
		/// aller detektierten visuellen Bäume der Visual Studio Applikation an.
		/// </summary>
		private void AdjustVSFrameworkElements() {
			var frameworkElements = PresentationSource.CurrentSources.OfType<HwndSource>()
				.Select(handleSource => handleSource.RootVisual).OfType<FrameworkElement>()
				.Where(x => x != null);
			foreach (var frameworkElement in frameworkElements) {
				AdjustVisualTree(frameworkElement);
			}
		}

		/// <summary>
		/// Wende ClearType-Einstellungen auf Haupt-, Toolfenster und auf Framework 
		/// Elemente aller detektierten visuellen Bäume der Visual Studio Applikation an.
		/// </summary>
		public void UpdateClearType() {
			AdjustVSFrameworkElements();
		}
	}
}
