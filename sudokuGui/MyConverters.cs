using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Utilities;

namespace sudokuGui;

public abstract class MyConverters
{
    /// <summary>
    /// Gets a Converter that takes a number as input and converts it into a text representation
    /// </summary>
    public static IValueConverter IndexConverter { get; } =
        new FunValueConverter<List<object?>?, object?>(
            (list, param) =>
            {
                var i = int.Parse((string)param! ?? "0");
                return list?[i];
            });


    /// <summary>
    /// A general purpose <see cref="IValueConverter"/> that uses a <see cref="Func{TResult}"/>
    /// to provide the converter logic.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    class FunValueConverter<TIn, TOut> : IValueConverter
    {
        private readonly Func<TIn?, object?, TOut> _convert;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncValueConverter{TIn, TOut}"/> class.
        /// </summary>
        /// <param name="convert">The convert function.</param>
        public FunValueConverter(Func<TIn?, object?, TOut> convert)
        {
            _convert = convert;
        }

        /// <inheritdoc/>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (TypeUtilities.CanCast<TIn>(value))
            {
                return _convert((TIn?)value, parameter);
            }
            else
            {
                return AvaloniaProperty.UnsetValue;
            }
        }

        /// <inheritdoc/>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}