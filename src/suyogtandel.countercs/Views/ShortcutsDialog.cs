using System.Reflection;

namespace suyogtandel.countercs.Views;

public static class ShortcutsDialog
{
	public static void Present(Gtk.Window parent)
	{
		using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("shortcuts-dialog.ui");
		using var reader = new StreamReader(stream!);
		using var builder = Gtk.Builder.NewFromString(reader.ReadToEnd(), -1);

		var dialog = (Adw.ShortcutsDialog)builder.GetObject("shortcuts_dialog")!;
		dialog.Present(parent);
	}
}
