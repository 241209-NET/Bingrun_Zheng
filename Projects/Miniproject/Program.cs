namespace MiniProject;

class Program
{
    static Dictionary<string, int> inventory = new();
    static void Main(string[] args)
    {
        bool finish = false;
        while (!finish)
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. Add Player");
            Console.WriteLine("2. Add Gold");
            Console.WriteLine("3. Spend Gold");
            Console.WriteLine("4. View Player Gold");
            Console.WriteLine("5. Exit");

            int choices;
            if (int.TryParse(Console.ReadLine(), out choices))
            {
                switch (choices)
                {
                    case 1:
                        AddPlayer();
                        break;
                    case 2:
                        AddGold();
                        break;
                    case 3:
                        SpendGold();
                        break;
                    case 4:
                        ViewGold();
                        break;
                    case 5:
                        finish = true;
                        break;
                    default:
                        Console.WriteLine("Please Enter a number from 1 to 4.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please Enter a valid number.");
            }
        }
    }

    static void AddPlayer()
    {
        Console.WriteLine("Please enter the player name:");
        string? name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Player name cannot be blank");
        }
        else if (name.Any(char.IsDigit))
        {
            Console.WriteLine("Please do not enter number for name.");
        }
        else
        {
            Console.WriteLine("Please enter the initial gold the player have:");
            string? goldInput = Console.ReadLine();
            if (int.TryParse(goldInput, out int gold))
            {
                if (!inventory.TryAdd(name, gold))
                {
                    Console.WriteLine("Player already exist.");
                }
            }
            else
            {
                Console.WriteLine("Enter a valid gold amount.");
            }
        }
    }

    static void AddGold()
    {
        Console.WriteLine("Please enter the player name you want to add gold to: ");
        int count = 1;
        foreach (var player in inventory)
        {
            Console.WriteLine($"{count}. Player Name: {player.Key}, Player Gold: {player.Value}");
            count++;
        }

        string? pName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(pName))
        {
            Console.WriteLine("Please enter a valid player name.");
        }
        else if (inventory.ContainsKey(pName))
        {
            Console.WriteLine("Please enter the amount of gold you would like to add:");
            string? goldInput = Console.ReadLine();
            if (int.TryParse(goldInput, out int gold))
            {
                inventory[pName] += gold;
                Console.WriteLine($"Added {gold} gold to {pName}. New balance: {inventory[pName]}");
            }
            else
            {
                Console.WriteLine("Enter a valid gold amount.");
            }
        }
        else
        {
            Console.WriteLine("Player not found.");
        }
    }


    static void SpendGold()
    {
        Console.WriteLine("Please enter the player name you want to spend gold with: ");
        int count = 1;
        foreach (var player in inventory)
        {
            Console.WriteLine($"{count}. Player Name: {player.Key}, Player Gold: {player.Value}");
            count++;
        }

        string? pName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(pName))
        {
            Console.WriteLine("Please enter a valid player name.");
        }
        else if (inventory.ContainsKey(pName))
        {
            Console.WriteLine("Please enter the amount of gold you would like to spend:");
            string? goldInput = Console.ReadLine();
            if (int.TryParse(goldInput, out int gold))
            {
                inventory[pName] -= gold;
                Console.WriteLine($"Spend {gold} gold of {pName}. New balance: {inventory[pName]}");
            }
            else
            {
                Console.WriteLine("Enter a valid gold amount.");
            }
        }
        else
        {
            Console.WriteLine("Player not found.");
        }
    }

    static void ViewGold()
    {
        int count = 1;
        foreach (var player in inventory)
        {
            Console.WriteLine($"{count}. Player Name: {player.Key}, Player Gold: {player.Value}");
            count++;
        }
    }

}