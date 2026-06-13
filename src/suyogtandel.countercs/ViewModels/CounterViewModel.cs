using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace suyogtandel.countercs.ViewModels;

public class CounterViewModel : INotifyPropertyChanged
{
	private int _count;

	public int Count
	{
		get => _count;
		private set
		{
			if (_count == value) return;
			_count = value;
			OnPropertyChanged();
		}
	}

	public void Increment() => Count++;
	public void Decrement() => Count--;

	public event PropertyChangedEventHandler? PropertyChanged;

	private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
