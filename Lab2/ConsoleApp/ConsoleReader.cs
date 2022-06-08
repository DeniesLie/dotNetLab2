using Data;
using Data.Entities;
using Lab2.Validators;
using LinqLab1;

namespace Lab2;

public class ConsoleReader
{
    private readonly ItemService _itemService;
    
    public ConsoleReader(ItemService itemService)
    {
        _itemService = itemService;
    }
    
    public Item ReadItem()
    {
        var item = new Item();
        Console.WriteLine("Enter item info:");
        item.Name = ReadField<string>("Name", validator => validator.SetMinLength(2).SetMaxLength(30));
        item.PricePerUnit = ReadField<double>("Price per unit", validator => validator.SetMinValue(0.01));
        item.Amount = ReadField<uint>("Amount in storage");
        var firstSupplyDateTime = ReadField<DateTime>("Supply date time");
        item.SupplyDateTimes.Add(firstSupplyDateTime);
        Console.WriteLine("Select manufacturer:");
        var manufacturers = _itemService.GetAllManufacturers();
        manufacturers.Print();
        var manufacturerId = ReadField<int>("ManufacturerId:", v => v.SetMinValue(1));
        item.ManufacturerId = manufacturerId;
        item.Manufacturer = manufacturers.SingleOrDefault(m => m.Id == manufacturerId);
        var categoriesCount = ReadField<uint>("Number of item's categories:");
        for (int i = 0; i < categoriesCount; i++)
        {
            var categoryName = ReadField<string>($"Category #{i}", v => v.SetMinLength(2));
            item.ItemCategories.Add( new ItemCategory(){
                Name = categoryName
            });
        }
        return item;
    }


    public  T? ReadField<T>(string fieldName) => ReadField<T>(fieldName, null);
    public  T? ReadField<T>(string fieldName, Action<Validator<T>>? configureValidator)
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