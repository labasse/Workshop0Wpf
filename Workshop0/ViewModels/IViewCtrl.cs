using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop0.ViewModels
{
    public interface IViewCtrl
    {
        void ShowError(string message, string title);
        string? OpenFileDialog(string filter);
        void Quit();
    }
}
