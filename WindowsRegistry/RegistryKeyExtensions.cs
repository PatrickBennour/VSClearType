using System;
using Microsoft.Win32;

namespace VSClearType {
	/// <summary>
	/// Typbasierte nullable Konvertierungen von Subkeys.
	/// </summary>
	internal static class RegistryKeyExtensions {
		/// <summary>
		/// Konvertierung des Subkeys in Enum-Wert
		/// </summary>
		/// <typeparam name="S">Typ des Enums.</typeparam>
		/// <param name="registry">RegistryKey.</param>
		/// <param name="key">Schlüssel des Subkeys.</param>
		/// <param name="defaultValue">Standardwert, falls Schlüssel nicht existiert.</param>
		/// <returns>Wert des Subkeys oder Standardwert, falls Schlüssel nicht existiert.</returns>
		public static S GetEnum<S>(this RegistryKey registry, string key, S defaultValue) where S : struct {
			var subKey = (registry.GetValue(key) ?? "") as string;
			return Enum.TryParse(subKey, out S enumValue) ? enumValue : defaultValue;
		}

		/// <summary>
		/// Konvertierung des Subkeys in booleschen Wert.
		/// </summary>
		/// <param name="registry">RegistryKey.</param>
		/// <param name="key">Schlüssel des Subkeys.</param>
		/// <param name="defaultValue">Standardwert, falls Schlüssel nicht existiert.</param>
		/// <returns>Wert des Subkeys oder Standardwert, falls Schlüssel nicht existiert.</returns>
		public static bool GetBool(this RegistryKey registry, string key, bool defaultValue) {
			var subKey = (registry.GetValue(key) ?? "") as string;
			return bool.TryParse(subKey, out bool boolValue) ? boolValue : defaultValue;
		}
	}
}
