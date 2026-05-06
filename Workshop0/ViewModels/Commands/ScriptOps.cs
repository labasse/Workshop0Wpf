using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Workshop0.Windows;

namespace Workshop0.ViewModels.Commands
{
    public class ScriptOps
    {
        public static RoutedCommand New  { get; } = new RoutedCommand(nameof(New), typeof(ScriptOps));
        public static RoutedCommand Open { get; } = new RoutedCommand(nameof(Open), typeof(ScriptOps));
        public static RoutedCommand Filter { get; } = new RoutedCommand(nameof(Filter), typeof(ScriptOps));
    }
}
