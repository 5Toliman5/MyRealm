using System.ComponentModel.DataAnnotations;

namespace MyRealm.Common.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
	public class IncludeIfAnotherPropertyIsTrueAttribute : ValidationAttribute
	{
		public string DefiningPropertyName { get; }

		public IncludeIfAnotherPropertyIsTrueAttribute(string definingPropertyName)
		{
			DefiningPropertyName = definingPropertyName;
		}

		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
			var definingPropertyInfo = validationContext.ObjectType.GetProperty(DefiningPropertyName);
			if (definingPropertyInfo is null)
			{
				throw new ArgumentException($"Unknown property: {DefiningPropertyName}");
			}

			var definingPropertyValue = definingPropertyInfo.GetValue(validationContext.ObjectInstance);
			if (definingPropertyValue is null)
			{
				return ValidationResult.Success;
			}
			if (definingPropertyValue is bool isTrue)
			{
				if (isTrue)
				{
					var propertyInfo = validationContext.ObjectType.GetProperty(validationContext.MemberName);
					var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance);

					if (propertyValue is null)
					{
						return new ValidationResult($"The field {validationContext.DisplayName} is required when {DefiningPropertyName} is true.");
					}
				}
			}
			else
			{
				return new ValidationResult($"The field {DefiningPropertyName} is not recognized as Boolean.");
			}
			return ValidationResult.Success;
		}
	}
}
