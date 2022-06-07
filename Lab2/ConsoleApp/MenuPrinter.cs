using System.Text;

namespace Lab2;

public static class MenuPrinter
{
    public static void Print()
    {
        var sb = new StringBuilder();
        sb.Append("Options: \n");
        sb.Append("0 - Add New Item \n");
        sb.Append("1 - Add Supply DateTime To Item \n");
        sb.Append("2 - Get All items In Storage \n");
        sb.Append("3 - Get All Items From Manufacturer\n");
        sb.Append("4 - Get Categories Of Item \n");
        sb.Append("5 - GetAllManufacturers \n");
        sb.Append("6 - Get Items That Contain Category \n");
        sb.Append("7 - Get Items Sorted By Price \n");
        sb.Append("8 - Get Items Sorted By Last Supply Date \n");
        sb.Append("9 - Get Items Amount Per Manufacturer \n");
        sb.Append("10 - Get Top 3 Cheapest Items \n");
        sb.Append("11 - Get Item Categories Sold By Each Manufacturer \n");
        sb.Append("12 - Find Laptops By Price Range \n");
        sb.Append("13 - Get Wired Chargers \n");
        sb.Append("14 - Get Manufacturers Who Sales At Least One Laptop \n");
        sb.Append("15 - Get Average Price Of Smartphones \n");
        sb.Append("16 - Get Supplies Info Per Each Date \n");
        sb.Append("17 - Get The Most Expensive Items Per Manufacturer\n");
        Console.WriteLine(sb.ToString());
    }
}