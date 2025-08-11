using System.ComponentModel.DataAnnotations;

namespace TrueSecProject.Validation;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RequireOneOfAttribute : ValidationAttribute
{
    private readonly string[] _propertyNames;

    public RequireOneOfAttribute(params string[] propertyNames)
    {
        _propertyNames = propertyNames;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        var type = validationContext.ObjectType;

        var hasAtLeastOneValue = _propertyNames.Any(propertyName =>
        {
            var property = type.GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on type '{type.Name}'.");
            }

            var propertyValue = property.GetValue(value);

            return propertyValue is string strValue ? !string.IsNullOrWhiteSpace(strValue) : propertyValue != null;
        });

        if (hasAtLeastOneValue)
        {
            return ValidationResult.Success;
        }

        // If no properties have a value, return a validation error.
        ErrorMessage ??= $"At least one of the following properties must be provided: {string.Join(", ", _propertyNames)}.";
        return new ValidationResult(ErrorMessage, _propertyNames);
    }
}