using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop0.Services
{
    /// <summary>
    /// Provides access to application services to simulate dependency injection and
    /// service management.
    /// </summary>
    public class ServiceContainer
    {
        public static ServiceContainer Instance { get;  } = new ServiceContainer();

        private ServiceContainer() { }

        public IUIService? UIService { get; set; } = null;
        public IScriptCurrencyService? ScriptCurrencyService { get; set; } = null;
    }
}
