using Workshop0.Models;

namespace Workshop0.Services
{
    public class ScriptCurrencyService : IScriptCurrencyService
    {
        public Script? SelectedScript {
            get;
            set
            {
                if (field != value)
                {
                    field = value;
                    SelectedScriptChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        } = null;

        public event EventHandler? SelectedScriptChanged;
    }
}
