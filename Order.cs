public class Order
{
    public const decimal MemberDiscountRate = 0.10m;

    public List<OrderItem> Items { get; } = new List<OrderItem>();
    public bool HasMemberCard { get; set; }

    public void AddItem(MenuItem menuItem, int quantity)
    {
        Items.Add(new OrderItem(menuItem, quantity));
    }

    public decimal CalculateSubtotal()
    {
        decimal subtotal = 0m;
        foreach (var item in Items)
        {
            subtotal += item.CalculateTotal();
        }

        return subtotal;
    }

    public decimal CalculateBundleDiscount()
    {
        decimal discount = 0m;
        foreach (var item in Items)
        {
            discount += item.CalculateBundleDiscount();
        }

        return discount;
    }

    public decimal CalculateMemberDiscount()
    {
        decimal amountAfterBundleDiscount = CalculateSubtotal() - CalculateBundleDiscount();
        return HasMemberCard ? amountAfterBundleDiscount * MemberDiscountRate : 0m;
    }

    public decimal CalculateTotal()
    {
        return CalculateSubtotal() - CalculateBundleDiscount() - CalculateMemberDiscount();
    }
}
