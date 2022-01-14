namespace Tanner.Template.Base.Common.Validators;

/// <summary>
/// Se utiliza para validar que el valor de un enum tomado desde un json se encuentre en el objeto Enum indicado
/// </summary>
/// <typeparam name="TEnum"></typeparam>
public class StringConverterValidator<TEnum> : JsonConverter<TEnum>
{
    private readonly bool _isNullable;
    private readonly JsonConverter<TEnum> _converter;
    private readonly Type _enumType;

    public StringConverterValidator() : this(null) { }

    public StringConverterValidator(JsonSerializerOptions options)
    {
        _isNullable = Nullable.GetUnderlyingType(typeof(TEnum)) != null;

        // for performance, use the existing converter if available
        if (options != null)
        {
            _converter = (JsonConverter<TEnum>)options.GetConverter(typeof(TEnum));
        }

        _enumType = _isNullable ?
            Nullable.GetUnderlyingType(typeof(TEnum)) :
            typeof(TEnum);
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(TEnum).IsAssignableFrom(typeToConvert);
    }

    public override TEnum Read(ref Utf8JsonReader reader,
        Type typeToConvert, JsonSerializerOptions options)
    {
        if (_converter != null)
        {
            return _converter.Read(ref reader, _enumType, options);
        }

        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException(CONSTANTS.INVALID_INPUT_FORMAT);


        string value = reader.GetString();


        if (!Enum.TryParse(_enumType, value, ignoreCase: false, out object result)
            && !Enum.TryParse(_enumType, value, ignoreCase: true, out result))
        {
            throw new JsonException(
                $"Unable to convert \"{value}\" to Enum \"{_enumType}\".");
        }

        return (TEnum)result;
    }

    public override void Write(Utf8JsonWriter writer,
        TEnum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}