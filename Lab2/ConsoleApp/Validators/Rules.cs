namespace Lab2.Validators;

public static class Rules
{
    public static Validator<string> SetMinLength(this Validator<string> validator, uint minLength)
    {
        return validator.AddRule((string input, ValidationResult result) =>
        {
            if (input.Length < minLength)
            {
                result.IsSucceeded = false;
                result.Messages.Add($"must contain at least {minLength} characters");
            }
        });
    }
    
    public static Validator<string> SetMaxLength(this Validator<string> validator, uint maxLength)
    {
        return validator.AddRule((string input, ValidationResult result) =>
        {
            if (input.Length > maxLength)
            {
                result.IsSucceeded = false;
                result.Messages.Add($"must contain at least {maxLength} characters");
            }
        });
    }
    
    public static Validator<int> SetMinValue(this Validator<int> validator, uint minValue)
    {
        return validator.AddRule(((input, result) =>
        {
            if (input < minValue)
            {
                result.IsSucceeded = false;
                result.Messages.Add($"Value must be not less then {minValue}");
            }
        }));
    }
    
    public static Validator<int> SetMaxValue(this Validator<int> validator, uint maxValue)
    {
        return validator.AddRule(((input, result) =>
        {
            if (input > maxValue)
            {
                result.IsSucceeded = false;
                result.Messages.Add($"Value must be less then {maxValue}");
            }
        }));
    }
    
    public static Validator<decimal> SetMinValue(this Validator<decimal> validator, decimal minValue)
    {
        return validator.AddRule(((input, result) =>
        {
            if (input < minValue)
            {
                result.IsSucceeded = false;
                result.Messages.Add($"Value must be not less then {minValue}");
            }
        }));
        
    }
    
    public static Validator<decimal> SetMaxValue(this Validator<decimal> validator, decimal maxValue)
    {
        return validator.AddRule(((input, result) =>
        {
            if (input > maxValue)
            {
                result.IsSucceeded = false;
                result.Messages.Add($"Value must be less then {maxValue}");
            }
        }));
    }
}