using System.ComponentModel.DataAnnotations;

namespace MyRealm.Common.Validation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class OneTrueOfAttribute : ValidationAttribute
    {
        private readonly string[] PropertyNames;

        public OneTrueOfAttribute(params string[] propertyNames)
        {
            PropertyNames = propertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = validationContext.ObjectType
                .GetProperties()
                .Where(x => PropertyNames.Contains(x.Name))
                .ToArray();

            if (properties.Length != PropertyNames.Length)
            {
				throw new ArgumentException($"Some of the specified properties are not found");
			}

			var values = properties.Select(x => x.GetValue(validationContext.ObjectInstance));
            if (values.Any(x => x is not null and not bool))
            {
				throw new ArgumentException($"Some of the specified properties are not recognized as Boolean");
			}
			if (values.Count(x => x is true) > 1)
			{
				return new ValidationResult($"Only one of the following properties can be true: {string.Join(", ", PropertyNames)}");
			}

            return ValidationResult.Success;
        }
    }
}
