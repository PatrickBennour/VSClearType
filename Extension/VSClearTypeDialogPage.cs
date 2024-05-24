using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Microsoft.VisualStudio.Shell;
using Forms = System.Windows.Forms;

namespace VSClearType {
	/// <summary>
	/// Dialogseite in den Visual Studio Einstellungen.
	/// </summary>
	internal class VSClearTypeDialogPage : DialogPage {
		/// <summary>
		/// ElementHost.
		/// </summary>
		private ElementHost ElementHost { get; set; }

		/// <summary>
		/// RadioButton für "Auto"-Rendering.
		/// </summary>
		private RadioButton RadioButtonRenderingAuto { get; set; }

		/// <summary>
		/// RadioButton für "Aliased"-Rendering.
		/// </summary>
		private RadioButton RadioButtonRenderingAliased { get; set; }

		/// <summary>
		/// RadioButton für "Grayscale"-Rendering.
		/// </summary>
		private RadioButton RadioButtonRenderingGrayscale { get; set; }

		/// <summary>
		/// RadioButton für "ClearType"-Rendering.
		/// </summary>
		private RadioButton RadioButtonRenderingClearType { get; set; }

		/// <summary>
		/// RadioButton für "Display"-Formatting.
		/// </summary>
		private RadioButton RadioButtonFormattingDisplay { get; set; }

		/// <summary>
		/// RadioButton für "Ideal"-Formatting.
		/// </summary>
		private RadioButton RadioButtonFormattingIdeal { get; set; }

		/// <summary>
		/// RadioButton für "Auto"-Hinting.
		/// </summary>
		private RadioButton RadioButtonHintingAuto { get; set; }

		/// <summary>
		/// RadioButton für "Animated"-Hinting.
		/// </summary>
		private RadioButton RadioButtonHintingAnimated { get; set; }

		/// <summary>
		/// RadioButton für "Fixed"-Hinting.
		/// </summary>
		private RadioButton RadioButtonHintingFixed { get; set; }

		/// <summary>
		/// Erzeugt einen neuen Rahmen "thickness: 2, 2, 0, 3".
		/// </summary>
		/// <returns>Neuer Rahmen "thickness: 2, 2, 0, 3".</returns>
		private Thickness CreateMargin() {
			return new Thickness(2.0, 2.0, 0.0, 3.0);
		}

		/// <summary>
		/// Erzeugt ein neues RadioButton mit angegebenem Content.
		/// </summary>
		/// <param name="content">Content des RadioButton.</param>
		/// <param name="toolTip">ToolTip des RadioButton.</param>
		/// <returns>Neues RadioButton mit angegebenem Content.</returns>
		private RadioButton CreateRadioButton(string content, string toolTip) {
			var radioButton = new RadioButton {
				Content = content,
				Margin = CreateMargin(),
				ToolTip = toolTip
			};
			radioButton.Checked += RadioButtonChecked;
			return radioButton;
		}

		/// <summary>
		/// Behandle das Checked-Ereignis vom RadioButton 
		/// und Übertrage die Options in die Configuration Setings.
		/// Die visuelle Aktualisierung der IDE geschieht automatisch
		/// (über VSClearTypeExtension.AppWindowLayoutUpdated).
		/// </summary>
		/// <param name="sender">Auslöser des Ereignisses.</param>
		/// <param name="e">Ereignisinformationen.</param>
		private void RadioButtonChecked(object sender, RoutedEventArgs e) {
			WriteOptionsIntoSettings();
		}

		/// <summary>
		/// Erzeugt ein neues StackPanel mit einem Rahmen "thickness: 1, 3, 0, 2".
		/// </summary>
		/// <returns>Neues StackPanel mit einem Rahmen "thickness: 1, 3, 0, 2".</returns>
		private StackPanel CreateStackPanel() {
			return new StackPanel {
				Margin = new Thickness(1.0, 3.0, 0.0, 2.0)
			};
		}

		/// <summary>
		/// Erzeugt ein neues StackPanel mit angegebenen ContentControls,
		/// die als Kinder an dieses StackPanel angehängt werden.
		/// </summary>
		/// <param name="contentControls">ContentControls, die als Kinder in das StackPanel angehängt werden.</param>
		/// <returns>StackPanel mit ContentControls.</returns>
		private StackPanel CreateStackPanel(params ContentControl[] contentControls) {
			var stackPanel = CreateStackPanel();
			Array.ForEach(contentControls, contentControl => stackPanel.Children.Add(contentControl));
			return stackPanel;
		}

		/// <summary>
		/// Erzeugt eine GroupBox mit angegebenem Header und Content.
		/// </summary>
		/// <param name="header">Header der GroupBox.</param>
		/// <param name="content">Content der GroupBox.</param>
		/// <returns>GroupBox mit Header und Content.</returns>
		public GroupBox CreateGroupBox(string header, object content, string toolTip) {
			return new GroupBox {
				Header = header,
				Content = content,
				ToolTip = toolTip
			};
		}

		/// <summary>
		/// Konstruiert die visuelle Dialogseite für Visual Studio Einstellungen.
		/// </summary>
		private void CreateDialogPage() {
			ElementHost = new ElementHost {
				Child = CreateStackPanel(
					CreateGroupBox("Text Rendering mode",
						CreateStackPanel(
							RadioButtonRenderingAuto = CreateRadioButton("Auto",
							"Text wird mit dem am besten passenden Renderingalgorithmus auf Grundlage\ndes Layoutmodus gerendert, der zum Formatieren des Texts verwendet wurde."),
							RadioButtonRenderingAliased = CreateRadioButton("Aliased", "Text wird mit Bilevel-Antialiasing gerendert."),
							RadioButtonRenderingGrayscale = CreateRadioButton("Grayscale", "Text wird mit Graustufen-Antialiasing gerendert."),
							RadioButtonRenderingClearType = CreateRadioButton("ClearType", "Text wird mit dem am besten passenden ClearType-Renderingalgorithmus auf Grund-\nlage des Layoutmodus gerendert, der zum Formatieren des Texts verwendet wurde.")
						),
						"Unterstützte Rendermodi für Text."
					),
					CreateGroupBox("Text Formatting mode",
						CreateStackPanel(
							RadioButtonFormattingDisplay = CreateRadioButton("Display", "Verwendet GDI-kompatible Schriftarteigenschaften für das Layout von Text."),
							RadioButtonFormattingIdeal = CreateRadioButton("Ideal", "Verwendet ideale Schriftarteigenschaften für das Layout von Text.")
						),
						"Unterstützte Formatierungsmethoden."
					),
					CreateGroupBox("Text Hinting mode",
						CreateStackPanel(
							RadioButtonHintingAuto = CreateRadioButton("Auto", "Rendering-Engine bestimmt automatisch, ob zum Zeichnen von Text Qualitäts-\neinstellungen für animierten oder statischen Text verwendet werden sollen."),
							RadioButtonHintingAnimated = CreateRadioButton("Animated", "Rendering-Engine rendert Text mit der höchsten animierten Qualität."),
							RadioButtonHintingFixed = CreateRadioButton("Fixed", "Rendering-Engine rendert Text mit der höchsten statischen Qualität.")
						),
						"Renderingverhalten von statischem oder animiertem Text."
					)
				)
			};
		}

		/// <summary>
		/// Gesicherte ClearType-Einstellungen.
		/// </summary>
		private ClearTypeOptions Secured { get; set; }

		/// <summary>
		/// Übertrage Configuration Settings Einstellungen in gesicherte Einstellungen.
		/// </summary>
		private void SetSecuredOptions() {
			var options = VSClearTypeExtension.Current.ConfigurationRegistry.Settings.Options;
			Secured = new ClearTypeOptions {
				Formatting = options.Formatting,
				Hinting = options.Hinting,
				Rendering = options.Rendering
			};
		}

		/// <summary>
		/// Übertrage gesicherte Einstellungen in Configuration Settings Einstellungen.
		/// </summary>
		private void GetSecuredOptions() {
			var options = VSClearTypeExtension.Current.ConfigurationRegistry.Settings.Options;
			options.Formatting = Secured.Formatting;
			options.Hinting = Secured.Hinting;
			options.Rendering = Secured.Rendering;
		}

		/// <summary>
		/// Initialisiert die Dialogseite für die Einstellungen in Visual Studio.
		/// </summary>
		public VSClearTypeDialogPage() {
			CreateDialogPage();
			ReadSettingsFromRegistry();
			ReadOptionsFromSettings();
			SetSecuredOptions();
		}

		/// <summary>
		/// Window ist der ElementHost.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override Forms.IWin32Window Window {
			get {
				return ElementHost;
			}
		}

		/// <summary>
		/// Übertrage alle ClearType-Einstellungen in die Windows Registry
		/// und wende diese direkt an.
		/// </summary>
		/// <param name="e">Ereignisinformationen.</param>
		protected override void OnApply(PageApplyEventArgs e) {
			WriteOptionsIntoSettings();
			WriteSettingsIntoRegistry();
			base.OnApply(e);
		}

		/// <summary>
		/// Verwerfe alle ClearType-Einstellungen und wende 
		/// die Einstellungen aus der Windows Registry an.
		/// </summary>
		/// <param name="e">Ereignisinformationen.</param>
		protected override void OnClosed(EventArgs e) {
			GetSecuredOptions();
			WriteOptionsIntoSettings();
			base.OnClosed(e);
		}

		/// <summary>
		/// Lese Configuration Settings aus der Windows Registry.
		/// </summary>
		private void ReadSettingsFromRegistry() {
			VSClearTypeExtension.Current.ConfigurationRegistry.Load();
		}

		/// <summary>
		/// Schreibe Configuration Settings in die Windows Registry zurück.
		/// </summary>
		private void WriteSettingsIntoRegistry() {
			VSClearTypeExtension.Current.ConfigurationRegistry.Save();
		}

		/// <summary>
		/// Schreibt alle ClearType-Einstellungen aus der Dialogseite der 
		/// Einstellungen von Visual Studio in die Configuration Settings.
		/// </summary>
		private void WriteOptionsIntoSettings() {
			var settings = VSClearTypeExtension.Current.ConfigurationRegistry.Settings;
			var options = settings.Options;

			// Übertrage alle Einstellungen aus dieser Dialogseite in die ClearType-Einstellungen der Configuration Settings.
			if (RadioButtonRenderingAuto.IsChecked ?? false) {
				options.Rendering = TextRenderingMode.Auto;
			}
			if (RadioButtonRenderingAliased.IsChecked ?? false) {
				options.Rendering = TextRenderingMode.Aliased;
			}
			if (RadioButtonRenderingGrayscale.IsChecked ?? false) {
				options.Rendering = TextRenderingMode.Grayscale;
			}
			if (RadioButtonRenderingClearType.IsChecked ?? false) {
				options.Rendering = TextRenderingMode.ClearType;
			}
			if (RadioButtonFormattingDisplay.IsChecked ?? false) {
				options.Formatting = TextFormattingMode.Display;
			}
			if (RadioButtonFormattingIdeal.IsChecked ?? false) {
				options.Formatting = TextFormattingMode.Ideal;
			}
			if (RadioButtonHintingAuto.IsChecked ?? false) {
				options.Hinting = TextHintingMode.Auto;
			}
			if (RadioButtonHintingAnimated.IsChecked ?? false) {
				options.Hinting = TextHintingMode.Animated;
			}
			if (RadioButtonHintingFixed.IsChecked ?? false) {
				options.Hinting = TextHintingMode.Fixed;
			}
		}


		/// <summary>
		/// Liest alle ClearType-Einstellungen aus den Configuration Settings 
		/// in die Dialogseite der Einstellungen von Visual Studio.
		/// </summary>
		private void ReadOptionsFromSettings() {
			var settings = VSClearTypeExtension.Current.ConfigurationRegistry.Settings;
			var options = settings.Options;

			// Übertrage alle ClearType-Einstellungen der Configuration Settings in die Einstellungen dieser Dialogseite.
			switch (options.Rendering) {
				case TextRenderingMode.Auto:
					RadioButtonRenderingAuto.IsChecked = true;
					break;
				case TextRenderingMode.Aliased:
					RadioButtonRenderingAliased.IsChecked = true;
					break;
				case TextRenderingMode.Grayscale:
					RadioButtonRenderingGrayscale.IsChecked = true;
					break;
				case TextRenderingMode.ClearType:
					RadioButtonRenderingClearType.IsChecked = true;
					break;
			}
			switch (options.Formatting) {
				case TextFormattingMode.Ideal:
					RadioButtonFormattingIdeal.IsChecked = true;
					break;
				case TextFormattingMode.Display:
					RadioButtonFormattingDisplay.IsChecked = true;
					break;
			}
			switch (options.Hinting) {
				case TextHintingMode.Auto:
					RadioButtonHintingAuto.IsChecked = true;
					break;
				case TextHintingMode.Fixed:
					RadioButtonHintingFixed.IsChecked = true;
					break;
				case TextHintingMode.Animated:
					RadioButtonHintingAnimated.IsChecked = true;
					break;
			}
		}
	}
}
