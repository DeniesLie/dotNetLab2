namespace Lab2.Validators;

public class Validator<T>
{
    public delegate void RuleDelegate(T input, ValidationResult validationResult);

    // chain of rules (delegates)
    private RuleDelegate? _rules;

    public Validator<T> AddRule(RuleDelegate rule)
    {
        _rules += rule;
        return this;
    }
    
    public ValidationResult Validate(T input)
    {
        var result = new ValidationResult(isSucceeded: true);
        if (_rules is not null)
            _rules.Invoke(input, result);
        return result;
    }
}   