namespace test_market.Tests;

public class OrderItemTests
{
    [Fact]
    public void CalculateTotal_MultipleSets_ReturnsPriceTimesQuantity()
    {
        var item = new OrderItem(new MenuItem("Red set", 50m), 3);

        decimal total = item.CalculateTotal();

        Assert.Equal(150m, total);
    }

    [Theory]
    [InlineData("Green set", 40, 1, 0)]
    [InlineData("Green set", 40, 2, 4)]
    [InlineData("Green set", 40, 3, 4)]
    [InlineData("Green set", 40, 4, 8)]
    [InlineData("Green set", 40, 5, 8)]
    [InlineData("Pink set", 80, 1, 0)]
    [InlineData("Pink set", 80, 2, 8)]
    [InlineData("Pink set", 80, 3, 8)]
    [InlineData("Pink set", 80, 4, 16)]
    [InlineData("Pink set", 80, 5, 16)]
    [InlineData("Orange set", 120, 1, 0)]
    [InlineData("Orange set", 120, 2, 12)]
    [InlineData("Orange set", 120, 3, 12)]
    [InlineData("Orange set", 120, 4, 24)]
    [InlineData("Orange set", 120, 5, 24)]
    public void CalculateBundleDiscount_EligibleSet_DiscountsOnlyCompletePairs(
        string name, int price, int quantity, int expectedDiscount)
    {
        var menuItem = new MenuItem(name, price, hasBundleDiscount: true);
        var item = new OrderItem(menuItem, quantity);

        decimal discount = item.CalculateBundleDiscount();

        Assert.Equal((decimal)expectedDiscount, discount);
    }

    [Theory]
    [InlineData("Red set", 50)]
    [InlineData("Blue set", 30)]
    [InlineData("Yellow set", 50)]
    [InlineData("Purple set", 90)]
    public void CalculateBundleDiscount_IneligibleSet_ReturnsZero(string name, int price)
    {
        var item = new OrderItem(new MenuItem(name, price), 4);

        Assert.Equal(0m, item.CalculateBundleDiscount());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_NonPositiveQuantity_Throws(int quantity)
    {
        var menuItem = new MenuItem("Red set", 50m);

        Assert.Throws<ArgumentOutOfRangeException>(() => new OrderItem(menuItem, quantity));
    }
}
