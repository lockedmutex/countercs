using suyogtandel.countercs.ViewModels;

namespace suyogtandel.countercs.Views;

[GObject.Subclass<Adw.ApplicationWindow>(qualifiedName: nameof(CounterCsWindow))]
[Gtk.Template<Gtk.AssemblyResource>("CounterCsWindow.ui")]
public partial class CounterCsWindow
{
	[Gtk.Connect("CounterLabel")]
	private Gtk.Label _counterLabel;

	[Gtk.Connect("MinusButton")]
	private Gtk.Button _minusButton;

	[Gtk.Connect("PlusButton")]
	private Gtk.Button _plusButton;

	private readonly CounterViewModel _viewModel = new();

	public static CounterCsWindow New(Adw.Application app)
	{
		var window = CounterCsWindow.NewWithProperties([]);
		window.SetApplication(app);
		return window;
	}

	partial void Initialize()
	{
		_viewModel.PropertyChanged += (_, e) =>
		{
			if (e.PropertyName == nameof(CounterViewModel.Count))
				_counterLabel.SetText(_viewModel.Count.ToString());
		};

		_minusButton.OnClicked += (_, _) => _viewModel.Decrement();
		_plusButton.OnClicked += (_, _) => _viewModel.Increment();
	}
}
