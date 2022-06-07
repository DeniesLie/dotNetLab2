using Data.Entities;
using Lab2.Validators;

namespace Lab2;

public static class ConsoleReader
{

    public static Item ReadItem()
    {
        var item = new Item();
        Console.WriteLine("Enter item info:");
        item.Name = ReadField<string>("Name", validator => validator.SetMinLength(2).SetMaxLength(30));
        item.PricePerUnit = ReadField<double>("Price per unit", validator => validator.SetMinValue(0.01));
        item.Amount = ReadField<uint>("Amount in storage");
        return item;
    }


    public static T? ReadField<T>(string fieldName) => ReadField<T>(fieldName, null);
    public static T? ReadField<T>(string fieldName, Action<Validator<T>>? configureValidator)
    {
        var validator = new Validator<T>();
        if (configureValidator is not null) configureValidator(validator);
        bool isValidationSucceeded = false;
        T? fieldValue = default(T);
        do
        {
            Console.Write($"{fieldName}: ");
            try
            {
                fieldValue = (T?) Convert.ChangeType(Console.ReadLine(), typeof(T));
                if (fieldValue is null) throw new FormatException("Unknown format");

                var result = validator.Validate(fieldValue);
                isValidationSucceeded = result.IsSucceeded;
                if (!isValidationSucceeded)
                    Console.WriteLine($"\t*{String.Join('\n', result.Messages)}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"\t*Invalid format: {ex.Message}");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"\t*Value is out of range: {ex.Message}");
            }
        } while (!isValidationSucceeded);
        return fieldValue;
    }
}