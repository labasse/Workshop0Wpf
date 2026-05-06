using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using Workshop0.Models;
using Workshop0.Services;

namespace Workshop0.ViewModels
{
    public class ScriptVM : INotifyPropertyChanged
    {
        private readonly IScriptCurrencyService _curScriptService;
        public ScriptVM(IScriptCurrencyService curScriptService)
        {
            _curScriptService = curScriptService;
            _curScriptService.SelectedScriptChanged += (s, e) => 
                PropertyChanged?.Invoke(this, 
                    new PropertyChangedEventArgs(nameof(Script))
                );
        }

        public Script? Script => _curScriptService.SelectedScript;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
