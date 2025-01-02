using System;
using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NotEqualAttribute : ValidationAttribute
    {
        private readonly object _disallowedValue;

        public NotEqualAttribute(object disallowedValue)
        {
            // Try to parse and store as the actual type (handles Guid or other value types)
            _disallowedValue = disallowedValue is string && Guid.TryParse(disallowedValue.ToString(), out var guidValue)
                ? guidValue // Store as Guid
                : disallowedValue; // Store as original value
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Handle null values and skip comparison
            if (value == null)
            {
                return ValidationResult.Success;
            }

            // Handle Guid comparison explicitly
            if (_disallowedValue is Guid disallowedGuid && value is Guid inputGuid)
            {
                if (inputGuid == disallowedGuid)
                {
                    return new ValidationResult($"The value '{_disallowedValue}' is not allowed for {validationContext.DisplayName}.");
                }
            }
            else
            {
                // Fallback for other comparable types
                if (_disallowedValue.Equals(value))
                {
                    return new ValidationResult($"The value '{_disallowedValue}' is not allowed for {validationContext.DisplayName}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}