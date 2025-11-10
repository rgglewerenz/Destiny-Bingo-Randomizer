namespace Testing_with_electron.Interfaces
{
	public interface IWindowManagerProvider
	{
		Task Close();
		Task<bool> IsElectron();
		Task<bool> IsFullscreen();
		Task Maximize();
		Task Minimize();
		void OnMaximize(Action action);
		void OnResize(Action action);
		void OnRestore(Action action);
		Task Restore();
	}
}
