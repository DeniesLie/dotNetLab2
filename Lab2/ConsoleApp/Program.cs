using Data;
using Data.Entities;
using Lab2;
using Lab2.Validators;
using LinqLab1;

var itemRepo = new ItemRepository("/mnt/8A8633A486339025/Study/DotNet/lab2/Lab2/ConsoleApp/items.xml");
var itemService = new ItemService(itemRepo);
var storage = new Storage();
storage.SeedData();
itemRepo.Create(storage.Items);

MenuPrinter.Print();

while (true)
{
    var optionNum = ConsoleReader.ReadField<int>("Option number", validator => validator.SetMinValue(0).SetMaxValue(16));

    switch (optionNum)
    {
        case 0:
            var item = ConsoleReader.ReadItem();
            itemService.Add(item);
            break;
        case 1:
            var itemToUpdateId = ConsoleReader.ReadField<int>("Item id",
                v => v.SetMinValue(1));
            var dateTime = ConsoleReader.ReadField<DateTime>("Supply date time");
            itemService.AddSupplyDateTimeToItem(itemToUpdateId, dateTime);
            break;
        case 2:
            // get all
            itemService.GetAll().Print();
            break;
        case 3:
            var manufacturerName = ConsoleReader.ReadField<string>("Manufacturer name");
            itemService.GetAllItemsFromManufacturer(manufacturerName).Print();
            break;
        case 4:
            var itemId = ConsoleReader.ReadField<int>("Item id", v => v.SetMinValue(1));
            itemService.GetCategoriesOfItemById(itemId).Print();
            break;
        case 5:
            itemService.GetAllManufacturers().Print();
            break;
        case 6:
            var categoryName = ConsoleReader.ReadField<string>("Category");
            itemService.GetItemsThatContainsCategory(categoryName);
            break;
        case 7:
            itemService.GetItemsSortedByPrice().Print();
            break;
        case 8:
            itemService.GetItemsSortedByLastSupplyDate().Print();
            break;
        case 9:
            itemService.GetItemsAmountPerManufacturer().Print();
            break;
        case 10:
            itemService.GetTop3CheapestItems().Print();
            break;
        case 11:
            itemService.GetItemCategoriesSoldByManufacturer().Print();
            break;
        case 12:
            var fromPrice = ConsoleReader
                .ReadField<double>("From price", v => v.SetMinValue(0.01));
            var toPrice = ConsoleReader
                .ReadField<double>("To price", v => v.SetMinValue(0.01));
            itemService.FindLaptopsByPriceRange(fromPrice, toPrice).Print();
            break;
        case 13:
            itemService.GetWiredChargers().Print();
            break;
        case 14:
            itemService.GetManufacturersWithAtLeastOneLaptop().Print();
            break;
        case 15:
            Console.WriteLine($"Average price of phones: {itemService.GetAveragePriceOfPhones()}");
            break;
        case 16:
            itemService.GetSuppliesInfo().Print();
            break;
        case 17:
            itemService.GetTheMostExpensiveItemsPerManufacturer().Print();
            break;
        default:
            Console.WriteLine("Unknown option.");
            break;
    }
}
