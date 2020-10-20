using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Motel.Core.ComponentModel
{
    public class GenericListTypeConverter<T>:TypeConverter  
    {
        
        /// <summary>
        /// Type converter
        /// </summary>
        protected readonly TypeConverter typeConverter;

        public GenericListTypeConverter()
        {
            typeConverter = TypeDescriptor.GetConverter(typeof(T));
            if (typeConverter == null)
                throw new InvalidOperationException("No type converter exists for type " + typeof(T).FullName);
        }

        /// <summary>
        /// Get string array from a comma-separate string
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Array</returns>
        protected virtual string[] GetStringArray(string input)
        {
            return string.IsNullOrEmpty(input) ? Array.Empty<string>() : input.Split(',').Select(x => x.Trim()).ToArray();
        }

        /// <summary>
        /// Gets a value indicating whether this converter can        
        /// convert an object in the given source type to the native type of the converter
        /// using the context.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="sourceType">Source type</param>
        /// <returns>Result</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType != typeof(string))
                return base.CanConvertFrom(context, sourceType);

            var items = GetStringArray(sourceType.ToString());
            return items.Any();
        }

        /// <summary>
        /// Converts the given object to the converter's native type.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="culture">Culture</param>
        /// <param name="value">Value</param>
        /// <returns>Result</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string) && value != null)
                return base.ConvertFrom(context, culture, value);

            var items = GetStringArray((string)value);
            var result = new List<T>();
            Array.ForEach(items, s =>
            {
                var item = typeConverter.ConvertFromInvariantString(s);
                if (item != null)
                {
                    result.Add((T)item);
                }
            });

            return result;
        }

    }
}
