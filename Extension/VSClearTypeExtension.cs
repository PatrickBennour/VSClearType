using System;
using System.Windows;

namespace VSClearType {
	/// <summary>
	/// Einstiegsklasse dieser Visual Studio Extension.
	/// </summary>
	internal class VSClearTypeExtension {
		/// <summary>
		/// Lese-/Schreib-Zugriff auf die Windows Registry.
		/// </summary>
		public ConfigurationRegistry ConfigurationRegistry { get; private set; }

		/// <summary>
		/// Statische Referenz auf diese Extension.
		/// </summary>
		public static VSClearTypeExtension Current { get; private set; }

		/// <summary>
		/// Initialisierung dieser Visual Studio Extension.
		/// </summary>
		public static void Initialize() {
			Current = new VSClearTypeExtension();
		}

		/// <summary>
		/// Initialisiere weitere Komponenten und Ereignisbehandler.
		/// </summary>
		public VSClearTypeExtension() {
			ConfigurationRegistry = new ConfigurationRegistry();
			ConfigurationRegistry.Settings.UpdateClearType();
			Application.Current.MainWindow.LayoutUpdated += AppWindowLayoutUpdated;
		}

		/// <summary>
		/// Wende ClearType-Einstellungen auf alle visuellen Elemente 
		/// der Visual Studio IDE an, wenn sich das Layout ändert.
		/// </summary>
		/// <param name="sender">Auslöser des Ereignisses.</param>
		/// <param name="e">Ereignisinformationen.</param>
		private void AppWindowLayoutUpdated(object sender, EventArgs e) {
			ConfigurationRegistry.Settings.UpdateClearType();
		}
	}
}
