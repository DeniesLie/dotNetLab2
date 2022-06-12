using Data;
using Data.Entities;
using Lab2;
using Lab2.Validators;
using LinqLab1;

var itemRepo = new ItemRepository("C:\\Users\\User\\RiderProjects\\dotNetLab2\\Lab2\\ConsoleApp\\items.xml");
var itemService = new ItemService(itemRepo);
var cr = new ConsoleReader(itemService);
var storage = new Storage();
storage.SeedData();
itemRepo.Create(storage.Items);

MenuPrinter.Print();

while (true)
{
    var optionNum = cr.ReadField<int>("Option number", validator => validator.SetMinValue(0).SetMaxValue(17));

    switch (optionNum)
    {
        case 0:
            var item = cr.ReadItem();
            itemService.Add(item);
            break;
        case 1:
            var itemToUpdateId = cr.ReadField<int>("Item id",
                v => v.SetMinValue(1));
            var dateTime = cr.ReadField<DateTime>("Supply date time");
            itemService.AddSupplyDateTimeToItem(itemToUpdateId, dateTime);
            break;
        case 2:
            // get all
            itemService.GetAll().Print();
            break;
        case 3:
            var manufacturerName = cr.ReadField<string>("Manufacturer name");
            itemService.GetAllItemsFromManufacturer(manufacturerName).Print();
            break;
        case 4:
            var itemId = cr.ReadField<int>("Item id", v => v.SetMinValue(1));
            itemService.GetCategoriesOfItemById(itemId).Print();
            break;
        case 5:
            itemService.GetAllManufacturers().Print();
            break;
        case 6:
            var categoryName = cr.ReadField<string>("Category");
            itemService.GetItemsThatContainsCategory(categoryName).Print();
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
            var fromPrice = cr
                .ReadField<decimal>("From price", 
                      v => v.SetMinValue(0.01m));
            var toPrice = cr
                .ReadField<decimal>("To price", v => v.SetMinValue(0.01m));
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
