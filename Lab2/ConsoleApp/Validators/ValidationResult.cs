namespace Lab2.Validators;

public class ValidationResult
{
    public ValidationResult()
    { }

    public ValidationResult(bool isSucceeded)
    {
        IsSucceeded = isSucceeded;
    }
    
    public ValidationResult(bool isSucceeded, IEnumerable<string> message)
    {
        IsSucceeded = isSucceeded;
        Messages = message.ToList();
    }

    public bool IsSucceeded { get; set; }
    public List<string> Messages { get; set; } = new();
}