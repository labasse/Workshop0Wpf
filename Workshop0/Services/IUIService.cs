using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop0.Services
{
    public interface IUIService
    {
        void ShowError(string message, string title);
        string? OpenFileDialog(string filter);

        // event EventArgs<ClosingEventArgs> Closing;
    }
}
