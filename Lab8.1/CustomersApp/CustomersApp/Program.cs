﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace customersApp
{
    //Really good!
    public class Program
    {
        public static void Main(string[] args)
        {
            var customersList = new List<Customer>
            {
                new Customer(10, "Kamil", "Maghar"),
                new Customer(30, "Amir", "Maghar"),
                new Customer(60, "Yair", "Maghar"),
                new Customer(90, "kamil", "Tel-Aviv"),
                new Customer(120, "amir", "Tel-Aviv"),
                new Customer(150, "yair", "Tel-Aviv")
            };

            Console.WriteLine("All Customers:");
            Display(customersList);

            // Seperate Delegate method
            Console.WriteLine("Customers with name starts with A-K:");
            var p = new Program();
            var customersStartsWithAtoK = new CustomerFilter(p.CustomersStartWithAtoK);
            // CustomerFilter customersStartsWithAtoK = CustomersStartWithAtoK;
            Display(GetCustomers(customersList, customersStartsWithAtoK));

            // Anonymous Delegate
            Console.WriteLine("Customers with name starts with L-Z:");
            Display(GetCustomers(customersList, delegate(Customer customer)
                                                {
                                                    return Regex.IsMatch(customer.Name, "^[L-Z]");
                                                }
                                                ));

            // Lambda expression
            Console.WriteLine("Customers with ID < 100:");
            Display(GetCustomers(customersList, customer => customer.Id < 100));

        }

        public bool CustomersStartWithAtoK(Customer customer)
        {
            //Nice. Though you forgot small letters: "^[A-Ka-k]"
            return Regex.IsMatch(customer.Name, "^[A-K]");
        }

        //It is awesome that you knew to use IEnumerable, but why didn't you use yield?
        public static IEnumerable<Customer> GetCustomers(IEnumerable<Customer> customerCollection, CustomerFilter customerFilter)
        {
            var list = new List<Customer>();
            foreach (var customer in customerCollection)
            {
                if (customerFilter(customer))
                {
                    list.Add(customer);
                }
            }
            return list;

            // Alternatively, Using LINQ it's very simple:
            // return customerCollection.Where(customer => customerFilter(customer)).ToList();
        }

        //Great
        public static void Display(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID = {customer.Id} Name = {customer.Name} Address = {customer.Address}");
            }
            Console.WriteLine();
        }
    }
}
