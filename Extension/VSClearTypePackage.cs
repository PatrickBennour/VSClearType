using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace VSClearType {
	/// <summary>
	/// Dies ist die Klasse, die das von dieser Assembly verfügbar gemachte Paket implementiert.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Die Mindestanforderung dafür, dass eine Klasse als gültiges Paket für Visual Studio betrachtet wird, 
	/// ist die Implementierung der IVsPackage-Schnittstelle und die Registrierung bei der Shell.
	/// </para>
	/// <para>
	/// Dieses Paket verwendet die im Managed Package Framework (MPF) definierten Hilfsklassen.
	/// </para>
	/// <para>
	/// Vorgehensweise: 
	/// Es leitet sich von der Package-Klasse ab, welche die Implementierung der IVsPackage-Schnittstelle 
	/// bereitstellt und die im Framework definierten Registrierungsattribute verwendet, um sich selbst 
	/// und seine Komponenten bei der Shell zu registrieren.
	/// </para>
	/// <para>
	/// Diese Attribute geben Auskunft darüber, welche Daten beim Erstellen des pkgdef-Dienstprogramms 
	/// in die .pkgdef-Datei geschrieben werden sollen.
	/// </para>
	/// <para>
	/// Um in Visual Studio geladen zu werden, muss das Paket durch
	/// &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in der .vsixmanifest-Datei referenziert werden.
	/// </para>
	/// </remarks>
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[Guid(PackageGuidString)]
	[ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
	[ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
	[ProvideOptionPage(typeof(VSClearTypeDialogPage), "ClearType", "General", 0, 0, false)]
	public sealed class VSClearTypePackage : AsyncPackage {
		/// <summary>
		/// VSClearTypePackage GUID string.
		/// </summary>
		public const string PackageGuidString = "cb68b1d3-2bd2-4267-968f-9e3a9c0e5e08";

		#region Package Members

		/// <summary>
		/// Initialisierung des Pakets:
		/// Diese Methode wird direkt nach der Platzierung des Pakets aufgerufen. 
		/// Daher können Sie hier den gesamten Initialisierungscode ablegen, 
		/// der auf den von Visual Studio bereitgestellten Diensten basiert.
		/// </summary>
		/// <param name="cancellationToken">Ein Abbruchtoken zur Überwachung des Initialisierungsabbruchs, der beim Herunterfahren von VS auftreten kann.</param>
		/// <param name="progress">Ein Provider für Fortschrittsaktualisierungen.</param>
		/// <returns>
		/// Ein Task, der die asynchrone Arbeit der Paketinitialisierung darstellt, 
		/// oder eine bereits abgeschlossene Aufgabe, wenn keine vorhanden ist. 
		/// Geben Sie in dieser Methode nicht null zurück.
		/// </returns>
		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress) {
			// Bei asynchroner Initialisierung kann der aktuelle Thread zu diesem Zeitpunkt ein Hintergrundthread sein.
			// Führen Sie alle Initialisierungen durch, die den UI-Thread erfordern, nachdem Sie zum UI-Thread gewechselt sind.
			await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

			// Initialisierung der Extension.
			VSClearTypeExtension.Initialize();
		}

		#endregion
	}
}
