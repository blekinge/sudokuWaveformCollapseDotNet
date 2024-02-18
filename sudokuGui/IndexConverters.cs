using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Utilities;

namespace sudokuGui;

public abstract class IndexConverters
{
    public static IValueConverter IndexConverter { get; } =
        new FunValueConverter<List<object?>?, object?>(
            (List<object?>? list, object? param) =>
            {
                if (param is null) return null;
                var i = int.Parse((string)param);
                return list?[i];
            });

    private class FunValueConverter<TIn, TOut>(Func<TIn?, object?, TOut> convert) : IValueConverter
    {
        /// <inheritdoc/>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return TypeUtilities.CanCast<TIn>(value)
                ? convert((TIn?)value, parameter) ?? AvaloniaProperty.UnsetValue
                : AvaloniaProperty.UnsetValue;
        }

        /// <inheritdoc/>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}