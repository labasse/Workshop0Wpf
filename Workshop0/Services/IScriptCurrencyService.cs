using System;
using System.Collections.Generic;
using System.Text;
using Workshop0.Models;

namespace Workshop0.Services
{
    public interface IScriptCurrencyService
    {
        event EventHandler? SelectedScriptChanged;

        Script? SelectedScript { get; set; }
    }
}
