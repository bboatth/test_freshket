# Unit tests

Run all tests from the repository root:

```powershell
dotnet test test_market.sln
```

In Visual Studio, open `test_market.sln`, then choose **Test > Test Explorer > Run All Tests**.

The xUnit tests call `Order` and `OrderItem` directly, without console input.

- `OrderItemTests.cs`: quantity validation, line totals, and complete-pair discounts for quantities 1 through 5.
- `OrderTests.cs`: empty and mixed orders, member discounts, and the Orange five-set example (600 THB minus 24 THB = 576 THB).

`[Fact]` defines one test. `[Theory]` runs the same test once for each `[InlineData]` row.

Each test arranges an order, calls a calculation, and uses `Assert.Equal(expected, actual)` to check the result. Expected amounts are fixed examples of the pricing rules.

Membership is applied after bundle discounts. These tests cover calculation logic; they do not test console prompts or the menu configuration in `FoodStore`.
