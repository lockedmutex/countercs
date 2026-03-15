using System.Reflection;

namespace suyogtandel.countercs;

public class CounterCsWindow
{
    public Adw.ApplicationWindow Window { get; }

    private readonly Gtk.Label _counterLabel;
    private readonly Gtk.Button _minusButton;
    private readonly Gtk.Button _plusButton;

    private int _count;

    public CounterCsWindow(Adw.Application app)
    {

        using var windowBuilder = GtkUiUtil.LoadFromAssembly("CounterCsWindow.ui");

        Window = (Adw.ApplicationWindow)windowBuilder.GetObject("CounterCsWindow")!;
        Window.Application = app;
        
        _counterLabel = (Gtk.Label)windowBuilder.GetObject("CounterLabel")!;
        _minusButton = (Gtk.Button)windowBuilder.GetObject("MinusButton")!;
        _plusButton = (Gtk.Button)windowBuilder.GetObject("PlusButton")!;

        _minusButton.OnClicked += (_, _) => UpdateCount(-1);
        _plusButton.OnClicked += (_, _) => UpdateCount(1);
    }

    private void UpdateCount(int delta)
    {
        _count += delta;
        _counterLabel.SetText(_count.ToString());
    }
}

internal static class GtkUiUtil
{
    public static Gtk.Builder LoadFromAssembly(string uiString)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(uiString);
        using var reader = new StreamReader(stream!);
        return Gtk.Builder.NewFromString(reader.ReadToEnd(), -1);
    }
}