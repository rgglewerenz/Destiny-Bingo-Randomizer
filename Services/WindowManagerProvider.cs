using ElectronNET.API;
using System.Diagnostics.Contracts;
using Testing_with_electron.Interfaces;

namespace Testing_with_electron.Services
{
    public class WindowManagerProvider : IWindowManagerProvider
	{
        private readonly BrowserWindow _window;

        public WindowManagerProvider(BrowserWindow window) {
            _window = window;
        }

        public async Task<bool> IsElectron()
        {
            return !(_window == null);
        }

        public async Task<bool> IsFullscreen()
        {
            return await _window.IsMaximizedAsync() || await _window.IsFullScreenAsync();
        }

        public async Task Close()
        {
			_window.Close();
		}

        public async Task Minimize()
        {
			if (await _window.IsFullScreenAsync())
				_window.SetFullScreen(false);
			_window.Minimize();
		}

        public async Task Restore()
        {
            if(await _window.IsFullScreenAsync())
                _window.SetFullScreen(false);
			_window.Restore();
		}

		public async Task Maximize()
		{
			_window.Maximize();
		}

        public void OnMaximize(Action action)
        {
            _window.OnMaximize += action;
        }

		public void OnRestore(Action action)
		{
			_window.OnRestore += action;
		}

		public void OnResize(Action action)
		{
			_window.OnResize += action;
		}



	}
}
