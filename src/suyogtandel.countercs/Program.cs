namespace suyogtandel.countercs;

public static class Program
{
	public static int Main(string[] args)
	{
		Adw.Module.Initialize();
		GirCore.Integration.Initialize();

		var app = CounterCsApplication.NewWithProperties([]);
		return app.Run(args);
	}
}