using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Workshop0.Views.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(Brush))] // Attribut (Annotation, Décorateur)
    public class TimeSpanBrushConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets the time interval before an event when a warning should be issued.
        /// </summary>
        public required TimeSpan WarningTime { get; set; }
        /// <summary>
        /// Gets or sets the time interval before an event when a error should be issued.
        /// </summary>
        public required TimeSpan AlertTime { get; set; }
        /// <summary>
        /// Gets or sets the brush used to display warning indicators.
        /// </summary>
        public required Brush WarningColor { get; set; }
        /// <summary>
        /// Gets or sets the brush used to display error indicators.
        /// </summary>
        public required Brush AlertColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(targetType != typeof(Brush))
            {
                throw new InvalidOperationException("The target must be a Brush");
            }
            if(value is TimeSpan timeSpan)
            {
                if (timeSpan < WarningTime)
                {
                    return Brushes.Transparent;
                }
                else if (timeSpan < AlertTime)
                {
                    return WarningColor;
                }            
            }
            return AlertColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
