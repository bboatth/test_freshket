var store = new FoodStore();
store.Run();

if (!Console.IsInputRedirected)
{
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey(intercept: true);
}
