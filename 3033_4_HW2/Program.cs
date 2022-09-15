// Jack Herdejurgen 113436899
// Homework 2
// Sept 14, 2022

// Task1();
// Task2();
// Task3();

Console.Write("Enter the task # you would like performed: ");
string task = Console.ReadLine();
if (task == "1")
{
    Task1();
}
else if (task == "2")
{
    Task2();
}
else if (task == "3")
{
    Task3();
}
else { Console.WriteLine("Input not recognized."); }

void Task1()
{
    string[] Fruit = { "apples", "bananas", "oranges", "grapes", "blueberries" };
    double[] Price = { 0.99, 0.50, 0.50, 2.99, 1.99 };
    Console.WriteLine("Items List:");
    foreach (string fruit in Fruit)
    {
        Console.Write($"{fruit} ");
    }
    Console.WriteLine("\nPlease input the item name you want the price for:");
    string input = Console.ReadLine();
    int index = 5;
    for (int i = 0; i<Fruit.Length-1;i++)
    {
        if (Fruit[i] == input)
        {
            index = i;
        }
    }
    if (index == 5)
    {
        Console.WriteLine("Item name error, try again.");
        Task1();
    }
    else
    {
        Console.WriteLine($"The price for {Fruit[index]} is {Price[index]:C2}.");
    }
}
void Task2()
{
    bool success = false;
    Dictionary<string, double> dictionary = new Dictionary<string, double> { };
    dictionary.Add("apples", 0.99);
    dictionary.Add("bananas", 0.50);
    dictionary.Add("oranges", 0.50);
    dictionary.Add("grapes", 2.99);
    dictionary.Add("blueberries", 1.99);
    Dictionary<string,double>.KeyCollection keys = dictionary.Keys; 

    Console.WriteLine("Items List:");
    foreach (KeyValuePair<string, double> kvp in dictionary)
    {
        Console.Write($"{kvp.Key} ");
    }
    Console.WriteLine("\nPlease input the item name you want the price for:");
    string input = Console.ReadLine();
    foreach ( KeyValuePair<string,double> kvp in dictionary)
    {
        if (input == kvp.Key)
        {
            Console.WriteLine($"The price for {kvp.Key} is {kvp.Value:C2}.");
            success = true;
        }
    }
    if (success == false)
    {
        Console.WriteLine("Item name error, try again.");
        Task2();
    }
}
void Task3()
{
    Sale();
}
void Sale()
{
    Console.WriteLine("Do you want to enter a new receipt? (y/n)");
    Constant.input = Console.ReadLine();
    if(Constant.input == "y")
    {
        Prompt();
        PrintReceipt(Receipt.receipts.Last());
        Sale();
    }
    else if(Constant.input == "n")
    {
        Prompt2();
    }
    else
    {
        Console.WriteLine("Input not recognized.");
        Sale();
    }
}
void Prompt()
{
    int cognum;
    int gearnum;
    int id;
    double markup;
    double net;
    double tax;
    double total;
    Console.Write("Please input the number of cogs:");
    cognum = Convert.ToInt16(Console.ReadLine());
    Console.Write("Please input the number of gears:");
    gearnum = Convert.ToInt16(Console.ReadLine());
    Console.Write("Please input ID number:");
    id = Convert.ToInt16(Console.ReadLine());
    if (cognum > 10 || gearnum > 10 || cognum + gearnum >= 16)
    {
        markup = Constant.discountmarkup;
    }
    else { markup = Constant.fullmarkup; }
    net = ((cognum * Constant.cogprice) + (gearnum * Constant.gearprice)) * (1 + markup);
    tax = net * Constant.salextax;
    total = net + tax;
    Receipt receipt = new Receipt(cognum, gearnum, id, net, tax, total);
    Receipt.receipts.Add(receipt);
}
void PrintReceipt(Receipt receipt)
{
    Console.WriteLine("=============================");
    Console.WriteLine("RECEIPT");
    Console.WriteLine($"Customer ID: {receipt.id}");
    Console.WriteLine($"# of Cogs: {receipt.cognum}");
    Console.WriteLine($"# of Gears: {receipt.gearnum}");
    Console.WriteLine($"Net Amount: {receipt.netamt:C2}");
    Console.WriteLine($"Sales Tax: {receipt.tax:C2}");
    Console.WriteLine($"Grand Total: {receipt.total:C2}");
    Console.WriteLine($"Time: {receipt.dateTime}");
    Console.WriteLine("=============================");
}
void Prompt2()
{
    Console.Write("\nPlease choose from the options:\n1: Print all receipt of one customer\n2: Print all receipt for today\n3: Print the hightes total receipt\nPress other keys to end\n");
    string option = Console.ReadLine();
    if (option == "1")
    {
        Console.WriteLine("Please enter customer id:");
        int searchid = Convert.ToInt16(Console.ReadLine());
        foreach (Receipt receipt in Receipt.receipts)
        {
            if (searchid == receipt.id)
            {
                PrintReceipt(receipt);
            }
        }
        Prompt2();
    }
    else if (option == "2")
    {
        foreach(Receipt receipt in Receipt.receipts)
        {
            if(receipt.dateTime.Date == DateTime.Now.Date)
            {
                PrintReceipt(receipt);
            }            
        }
        Prompt2();
    }
    else if (option == "3")
    {
        Receipt maxreceipt = Receipt.receipts[0];
        double max = 0;
        foreach(Receipt receipt in Receipt.receipts)
        {
            if (receipt.total > max)
            {
                maxreceipt = receipt;
                max = receipt.total;
            }
        }
        PrintReceipt(maxreceipt);
        Prompt2();
    }
}
public class Constant
{
    public static double cogprice = 79.99;
    public static double gearprice = 250;
    public static double fullmarkup = 0.15;
    public static double discountmarkup = .125;
    public static double salextax = 0.089;
    public static string input;
}
public class Receipt
{
    public static List<Receipt> receipts = new List<Receipt>();
    public DateTime dateTime;
    public int cognum { get; set; }
    public int gearnum { get; set; }
    public int id { get; set; }
    public double netamt { get; set; }
    public double tax { get; set; }
    public double total { get; set; }
    public Receipt(int cognum, int gearnum, int id, double netamt, double tax, double total)
    {
        this.cognum = cognum;
        this.gearnum = gearnum;
        this.id = id;
        this.netamt = netamt;
        this.tax = tax;
        this.total = total;
        this.dateTime = DateTime.Now;
    }
}