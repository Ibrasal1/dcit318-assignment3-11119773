using System;
using System.Collections.Generic;

namespace WarehouseInventory
{
    // Marker interface
    public interface IInventoryItem { }

    // Custom exception
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(string message) : base(message) { }
    }

    // Electronic item
    public class ElectronicItem : IInventoryItem
    {
        public string Name { get; }
        public int Quantity { get; private set; }

        public ElectronicItem(string name, int quantity)
        {
            if (quantity < 0) throw new InvalidQuantityException("Quantity cannot be negative.");
            Name = name;
            Quantity = quantity;
        }

        public override string ToString() => $"[Electronics] {Name} - Qty: {Quantity}";
    }

    // Grocery item
    public class GroceryItem : IInventoryItem
    {
        public string Name { get; }
        public int Quantity { get; private set; }

        public GroceryItem(string name, int quantity)
        {
            if (quantity < 0) throw new InvalidQuantityException("Quantity cannot be negative.");
            Name = name;
            Quantity = quantity;
        }

        public override string ToString() => $"[Grocery] {Name} - Qty: {Quantity}";
    }

    // Generic repository
    public class Repository<T> where T : IInventoryItem
    {
        private List<T> _items = new();

        public void Add(T item) => _items.Add(item);

        public List<T> GetAll() => new List<T>(_items);
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("WAREHOUSE INVENTORY SYSTEM (Q3)\n");

            var electronicsRepo = new Repository<ElectronicItem>();
            var groceryRepo = new Repository<GroceryItem>();

            try
            {
                electronicsRepo.Add(new ElectronicItem("Laptop", 10));
                electronicsRepo.Add(new ElectronicItem("Smartphone", 20));

                groceryRepo.Add(new GroceryItem("Apples", 50));
                groceryRepo.Add(new GroceryItem("Bananas", -5)); // Will throw exception
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
            }

            Console.WriteLine("\n--- Electronics ---");
            foreach (var item in electronicsRepo.GetAll())
                Console.WriteLine(item);

            Console.WriteLine("\n--- Groceries ---");
            foreach (var item in groceryRepo.GetAll())
                Console.WriteLine(item);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
