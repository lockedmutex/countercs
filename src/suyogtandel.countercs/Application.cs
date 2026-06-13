using suyogtandel.countercs.Views;

namespace suyogtandel.countercs;

[GObject.Subclass<Adw.Application>]
public partial class CounterCsApplication
{
	partial void Initialize()
	{
		ApplicationId = "in.suyogtandel.countercs";
		Flags = Gio.ApplicationFlags.DefaultFlags;

		var preferencesAction = Gio.SimpleAction.New("preferences", null);
		preferencesAction.OnActivate += OnPreferencesAction;
		AddAction(preferencesAction);

		var aboutAction = Gio.SimpleAction.New("about", null);
		aboutAction.OnActivate += OnAboutAction;
		AddAction(aboutAction);

		var shortcutsAction = Gio.SimpleAction.New("shortcuts", null);
		shortcutsAction.OnActivate += OnShortcutsAction;
		AddAction(shortcutsAction);

		var quitAction = Gio.SimpleAction.New("quit", null);
		quitAction.OnActivate += (_, _) => Quit();
		AddAction(quitAction);

		SetAccelsForAction("app.shortcuts", ["<Control>question"]);
		SetAccelsForAction("app.quit", ["<Control>q"]);

		OnActivate += Activate;
	}

	private void Activate(GObject.Object sender, EventArgs args)
	{
		var window = ActiveWindow;
		if (window == null)
		{
			window = CounterCsWindow.New(this);
		}
		window.Present();
	}

	private static void OnPreferencesAction(Gio.SimpleAction sender, EventArgs args)
	{
		Console.WriteLine("app.preferences action activated");
	}

	private void OnShortcutsAction(Gio.SimpleAction sender, EventArgs args)
	{
		if (ActiveWindow != null)
		{
			ShortcutsDialog.Present(ActiveWindow);
		}
	}

	private void OnAboutAction(Gio.SimpleAction sender, EventArgs args)
	{
		var about = Adw.AboutDialog.New();
		about.ApplicationIcon = "CounterCS";
		about.DeveloperName = "Suyog Tandel";
		about.Version = "0.1.0";
		about.Copyright = "© 2026 Suyog Tandel";
		about.SetDevelopers(["Suyog Tandel"]);

		if (ActiveWindow != null)
		{
			about.Present(ActiveWindow);
		}
	}
}