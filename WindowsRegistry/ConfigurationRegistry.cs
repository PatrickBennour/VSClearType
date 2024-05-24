using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace VSClearType {
	/// <summary>
	/// Schreiben und Lesen von Windows Registry-Einträgen.
	/// </summary>
	internal class ConfigurationRegistry {
		/// <summary>
		/// Ablageort für Einstellungen dieser Visual Studio-Extension.
		/// </summary>
		const string registryKey = "Software\\VSExtensions\\ClearType";

		/// <summary>
		/// Registry-Schlüssel für TextHintingMode.
		/// </summary>
		const string hintingKey = "TextHintingMode";

		/// <summary>
		/// Registry-Schlüssel für TextFormattingMode.
		/// </summary>
		const string formattingKey = "TextFormattingMode";

		/// <summary>
		/// Registry-Schlüssel für TextRenderingMode.
		/// </summary>
		const string renderingKey = "TextRenderingMode";

		/// <summary>
		/// Registry-Schlüssel für das Visual Studio Hauptfenster.
		/// </summary>
		const string adjustMainWindowKey = "IsEnabled_AdjustVSAppWindow";

		/// <summary>
		/// Registry-Schlüssel für restliche Visual Studio Toolfenster.
		/// </summary>
		const string adjustToolWindowsKey = "IsEnabled_AdjustVSToolWindows";

		/// <summary>
		/// Registry-Schlüssel für weitere Visual Studio Famework Elemente.
		/// </summary>
		const string adjustFrameworkElementsKey = "AdjustFrameworkElements";

		/// <summary>
		/// ClearType-Konfigurationseinstellungen.
		/// </summary>
		public ConfigurationSettings Settings { get; private set; }

		/// <summary>
		/// Initialisierung der ClearType-Konfigurationseinstellungen.
		/// </summary>
		public ConfigurationRegistry() {
			Settings = new ConfigurationSettings();
			Load();
		}

		/// <summary>
		/// Liest alle ClearType-Einstellungen aus der Windows Registry in diese Configuration Settings.
		/// </summary>
		public void Load() {
			var registry = Registry.CurrentUser.OpenSubKey(registryKey);
			if (registry != null) {
				Settings.Options.Hinting = registry.GetEnum(hintingKey, DefaultOptions.Hinting);
				Settings.Options.Formatting = registry.GetEnum(formattingKey, DefaultOptions.Formatting);
				Settings.Options.Rendering = registry.GetEnum(renderingKey, DefaultOptions.Rendering);
			} else {
				Settings.Options.Hinting = DefaultOptions.Hinting;
				Settings.Options.Formatting = DefaultOptions.Formatting;
				Settings.Options.Rendering = DefaultOptions.Rendering;
			}
		}

		/// <summary>
		/// Schreibt alle ClearType-Einstellungen aus diesen Configuration Settings in die Windows Registry.
		/// </summary>
		public void Save() {
			try {
				var subKey = Registry.CurrentUser.CreateSubKey(registryKey);
				subKey.SetValue(hintingKey, Settings.Options.Hinting.ToString());
				subKey.SetValue(formattingKey, Settings.Options.Formatting.ToString());
				subKey.SetValue(renderingKey, Settings.Options.Rendering.ToString());
			} catch (Exception exception) {
				Debug.WriteLine(exception.ToString());
			}
		}
	}
}
