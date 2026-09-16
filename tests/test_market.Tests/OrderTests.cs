namespace test_market.Tests;

public class OrderTests
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void EmptyOrder_AllAmountsAreZero(bool hasMemberCard)
    {
        var order = new Order { HasMemberCard = hasMemberCard };

        Assert.Equal(0m, order.CalculateSubtotal());
        Assert.Equal(0m, order.CalculateBundleDiscount());
        Assert.Equal(0m, order.CalculateMemberDiscount());
        Assert.Equal(0m, order.CalculateTotal());
    }

    [Fact]
    public void CalculateTotal_MultipleItemsWithoutMembership_ChargesFullSubtotal()
    {
        var order = new Order();
        order.AddItem(new MenuItem("Red set", 50m), 2);
        order.AddItem(new MenuItem("Green set", 40m, hasBundleDiscount: true), 1);

        Assert.Equal(140m, order.CalculateSubtotal());
        Assert.Equal(0m, order.CalculateBundleDiscount());
        Assert.Equal(0m, order.CalculateMemberDiscount());
        Assert.Equal(140m, order.CalculateTotal());
    }

    [Fact]
    public void CalculateTotal_MemberWithoutBundles_DiscountsEntireSubtotalByTenPercent()
    {
        var order = new Order { HasMemberCard = true };
        order.AddItem(new MenuItem("Red set", 50m), 2);
        order.AddItem(new MenuItem("Green set", 40m, hasBundleDiscount: true), 1);

        Assert.Equal(14m, order.CalculateMemberDiscount());
        Assert.Equal(126m, order.CalculateTotal());
    }

    [Fact]
    public void CalculateTotal_FiveOrangeSets_DiscountsFourSetsOnly()
    {
        // Arrange: five Orange sets at 120 THB each, without a member card.
        var order = new Order();
        order.AddItem(new MenuItem("Orange set", 120m, hasBundleDiscount: true), 5);

        // Act: calculate the bill.
        decimal total = order.CalculateTotal();

        // Assert: two pairs save 24 THB; the fifth set stays full price.
        Assert.Equal(600m, order.CalculateSubtotal());
        Assert.Equal(24m, order.CalculateBundleDiscount());
        Assert.Equal(576m, total);
    }

    [Fact]
    public void CalculateBundleDiscount_SingleSetsOfDifferentColors_DoNotFormPairs()
    {
        var order = new Order();
        order.AddItem(new MenuItem("Green set", 40m, hasBundleDiscount: true), 1);
        order.AddItem(new MenuItem("Pink set", 80m, hasBundleDiscount: true), 1);
        order.AddItem(new MenuItem("Orange set", 120m, hasBundleDiscount: true), 1);

        Assert.Equal(0m, order.CalculateBundleDiscount());
        Assert.Equal(240m, order.CalculateTotal());
    }

    [Fact]
    public void CalculateTotal_MixedOrder_DiscountsEligiblePairsOnly()
    {
        var order = new Order();
        order.AddItem(new MenuItem("Red set", 50m), 2);
        order.AddItem(new MenuItem("Green set", 40m, hasBundleDiscount: true), 3);
        order.AddItem(new MenuItem("Pink set", 80m, hasBundleDiscount: true), 2);
        order.AddItem(new MenuItem("Orange set", 120m, hasBundleDiscount: true), 5);

        Assert.Equal(980m, order.CalculateSubtotal());
        Assert.Equal(36m, order.CalculateBundleDiscount());
        Assert.Equal(0m, order.CalculateMemberDiscount());
        Assert.Equal(944m, order.CalculateTotal());
    }

    [Fact]
    public void CalculateTotal_MemberWithBundle_AppliesMembershipAfterBundleDiscount()
    {
        var order = new Order { HasMemberCard = true };
        order.AddItem(new MenuItem("Orange set", 120m, hasBundleDiscount: true), 5);

        Assert.Equal(24m, order.CalculateBundleDiscount());
        Assert.Equal(57.60m, order.CalculateMemberDiscount());
        Assert.Equal(518.40m, order.CalculateTotal());
    }
}
