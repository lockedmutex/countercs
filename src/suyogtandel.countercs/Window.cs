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

        Window = GtkUiUtil.GetRequiredWidget<Adw.ApplicationWindow>(windowBuilder, "CounterCsWindow");
        Window.Application = app;

        _counterLabel = GtkUiUtil.GetRequiredWidget<Gtk.Label>(windowBuilder, "CounterLabel");
        _minusButton = GtkUiUtil.GetRequiredWidget<Gtk.Button>(windowBuilder, "MinusButton");
        _plusButton = GtkUiUtil.GetRequiredWidget<Gtk.Button>(windowBuilder, "PlusButton");

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

    public static T GetRequiredWidget<T>(Gtk.Builder builder, string id) where T : GObject.Object
    {
        var obj = builder.GetObject(id);
        if (obj == null)
        {
            throw new InvalidOperationException(
                $"\n[CRITICAL UI ERROR]\n" +
                $"Failed to load the UI layout completely.\n" +
                $"Reason: A required widget with the ID '{id}' was not found in the XML definition.\n" +
                $"Action: Please check your .ui XML file and ensure that an element exists with the exact id=\"{id}\", and check for any typos.\n"
            );
        }

        if (obj is not T typedWidget)
        {
            throw new InvalidCastException(
                $"\n[CRITICAL UI ERROR]\n" +
                $"Failed to bind the UI layout to the code.\n" +
                $"Reason: The widget '{id}' was found, but there is a type mismatch.\n" +
                $"Expected Type (in C#): {typeof(T).Name}\n" +
                $"Actual Type (in XML): {obj.GetType().Name}\n" +
                $"Action: Ensure the XML tag matches the expected C# class type.\n"
            );
        }

        return typedWidget;
    }
}
