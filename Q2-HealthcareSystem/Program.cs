using System;
using System.Collections.Generic;

namespace HealthcareSystem
{
    // ---------- Entities ----------
    public class Patient
    {
        public int Id { get; }
        public string Name { get; }
        public int Age { get; }

        public Patient(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public override string ToString() => $"Patient {Id}: {Name}, {Age} years old";
    }

    public class Prescription
    {
        public string Medicine { get; }
        public int Quantity { get; }
        public string Dosage { get; }

        public Prescription(string medicine, int quantity, string dosage)
        {
            Medicine = medicine;
            Quantity = quantity;
            Dosage = dosage;
        }

        public override string ToString() => $"{Medicine} - {Quantity} units ({Dosage})";
    }

    // ---------- Generic Repository ----------
    public class Repository<T>
    {
        private List<T> _items = new();

        public void Add(T item) => _items.Add(item);

        public List<T> GetAll() => new List<T>(_items);
    }

    // ---------- App ----------
    class Program
    {
        static void Main()
        {
            Console.WriteLine("HEALTHCARE SYSTEM DEMO (Q2)\n");

            var patientRepo = new Repository<Patient>();
            var prescriptionsMap = new Dictionary<int, List<Prescription>>();

            // Add patients
            var p1 = new Patient(1, "Ibrahim", 25);
            var p2 = new Patient(2, "Ama", 32);
            patientRepo.Add(p1);
            patientRepo.Add(p2);

            // Add prescriptions
            prescriptionsMap[p1.Id] = new List<Prescription>
            {
                new Prescription("Paracetamol", 10, "2 tablets daily"),
                new Prescription("Vitamin C", 20, "1 tablet daily")
            };

            prescriptionsMap[p2.Id] = new List<Prescription>
            {
                new Prescription("Amoxicillin", 15, "1 capsule every 8 hours")
            };

            // Print all patients
            Console.WriteLine("All Patients:");
            foreach (var patient in patientRepo.GetAll())
            {
                Console.WriteLine(patient);
            }

            // Print prescriptions for each patient
            Console.WriteLine("\nPrescriptions by Patient:");
            foreach (var patient in patientRepo.GetAll())
            {
                Console.WriteLine($"\n{patient.Name}'s Prescriptions:");
                if (prescriptionsMap.TryGetValue(patient.Id, out var presList))
                {
                    foreach (var pres in presList)
                        Console.WriteLine($"  - {pres}");
                }
                else
                {
                    Console.WriteLine("  No prescriptions found.");
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
