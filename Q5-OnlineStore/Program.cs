using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore
{
    // Product class
    public class Product
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; private set; }

        public Product(string name, decimal price, int quantity)
        {
            if (quantity < 0) throw new ArgumentException("Quantity cannot be negative.");
            if (price < 0) throw new ArgumentException("Price cannot be negative.");
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString() => $"{Name} - {Quantity} @ {Price:C} each";
    }

    // Generic shopping cart
    public class ShoppingCart<T> where T : Product
    {
        private List<T> _items = new();

        public void AddItem(T item) => _items.Add(item);

        public void RemoveItem(T item) => _items.Remove(item);

        public decimal GetTotalCost() => _items.Sum(i => i.Price * i.Quantity);

        public void DisplayCart()
        {
            if (!_items.Any())
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            Console.WriteLine("Cart Contents:");
            foreach (var item in _items)
                Console.WriteLine($" - {item}");
            Console.WriteLine($"Total: {GetTotalCost():C}");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("ONLINE STORE SYSTEM (Q5)\n");

            var electronicsCart = new ShoppingCart<Product>();
            var groceryCart = new ShoppingCart<Product>();

            // Add electronics
            electronicsCart.AddItem(new Product("Laptop", 4500m, 1));
            electronicsCart.AddItem(new Product("Headphones", 300m, 2));

            // Add groceries
            groceryCart.AddItem(new Product("Rice", 50m, 5));
            groceryCart.AddItem(new Product("Milk", 15m, 10));

            Console.WriteLine("=== Electronics Cart ===");
            electronicsCart.DisplayCart();

            Console.WriteLine("\n=== Grocery Cart ===");
            groceryCart.DisplayCart();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
