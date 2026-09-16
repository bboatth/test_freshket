public class FoodStore
{
    private readonly MenuItem[] menu = new MenuItem[]
    {
        new MenuItem("Red set", 50m),
        new MenuItem("Green set", 40m, hasBundleDiscount: true),
        new MenuItem("Blue set", 30m),
        new MenuItem("Yellow set", 50m),
        new MenuItem("Pink set", 80m, hasBundleDiscount: true),
        new MenuItem("Purple set", 90m),
        new MenuItem("Orange set", 120m, hasBundleDiscount: true)
    };

    public void Run()
    {
        DisplayMenu();

        Order? order = ReadOrder();
        if (order == null)
        {
            return;
        }

        if (order.Items.Count == 0)
        {
            Console.WriteLine("No items ordered.");
            return;
        }

        bool? hasMemberCard = ReadMemberCard();
        if (hasMemberCard == null)
        {
            return;
        }

        order.HasMemberCard = hasMemberCard.Value;
        DisplayOrderSummary(order);
    }

    private void DisplayMenu()
    {
        Console.WriteLine("Food Store Menu");
        Console.WriteLine("---------------");
        foreach (var item in menu)
        {
            Console.WriteLine($"{item.Name,-12} {item.Price,3:0} THB/set");
        }

        Console.WriteLine($"Every same-color pair of Green, Pink or Orange sets gets {OrderItem.BundleDiscountRate * 100:0}% off that pair.");
        Console.WriteLine($"Members get {Order.MemberDiscountRate * 100:0}% off after bundle discounts.");
    }

    private Order? ReadOrder()
    {
        Console.WriteLine();
        Console.WriteLine("Enter the quantity for each set (0 to skip).");

        var order = new Order();
        foreach (var item in menu)
        {
            int? quantity = ReadQuantity(item.Name);
            if (quantity == null)
            {
                return null;
            }

            if (quantity.Value > 0)
            {
                order.AddItem(item, quantity.Value);
            }
        }

        return order;
    }

    private int? ReadQuantity(string itemName)
    {
        while (true)
        {
            Console.Write($"{itemName} quantity: ");
            string? input = Console.ReadLine();
            if (input == null)
            {
                return null;
            }

            if (int.TryParse(input, out int quantity) && quantity >= 0)
            {
                return quantity;
            }

            Console.WriteLine("Please enter a whole number of 0 or more.");
        }
    }

    private bool? ReadMemberCard()
    {
        while (true)
        {
            Console.Write("Does the customer have a member card? (y/n): ");
            string? input = Console.ReadLine();
            if (input == null)
            {
                return null;
            }

            string answer = input.Trim().ToLowerInvariant();
            if (answer == "y" || answer == "yes")
            {
                return true;
            }

            if (answer == "n" || answer == "no")
            {
                return false;
            }

            Console.WriteLine("Please enter y or n.");
        }
    }

    private void DisplayOrderSummary(Order order)
    {
        Console.WriteLine();
        Console.WriteLine("Order Summary");
        Console.WriteLine("-------------");
        foreach (var item in order.Items)
        {
            Console.WriteLine($"{item.MenuItem.Name} x {item.Quantity}: {item.CalculateTotal():F2} THB");
            if (item.CalculateBundleDiscount() > 0m)
            {
                Console.WriteLine($"  Bundle discount: -{item.CalculateBundleDiscount():F2} THB");
            }
        }

        Console.WriteLine($"Subtotal: {order.CalculateSubtotal():F2} THB");
        Console.WriteLine($"Bundle discount ({OrderItem.BundleDiscountRate * 100:0}% per pair): {order.CalculateBundleDiscount():F2} THB");
        Console.WriteLine($"Member discount ({Order.MemberDiscountRate * 100:0}%): {order.CalculateMemberDiscount():F2} THB");
        Console.WriteLine($"Total: {order.CalculateTotal():F2} THB");
    }
}
