using AXJ0GV_HFT_2021221.Data;
using AXJ0GV_HFT_2021221.Logic;
using AXJ0GV_HFT_2021221.Models;
using AXJ0GV_HFT_2021221.Repository;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AXJ0GV_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(5000);
            RestService rest = new RestService("http://localhost:18683");
            //var result1 = rest.Get<Dog>("/dog");

            //var result2 = rest.Get<Owner>("/owner");

            //var result3 = rest.Get<Injection>("/injection");

            //methods i need
            void theEndThing()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nPress enter to get back to the Menu");
                Console.ResetColor();
                Console.ReadLine();
            }
            void List(string listWhat)
            {
                switch (listWhat)
                {
                    case "Dog":
                        var dogs = rest.Get<Dog>("Dog");
                        foreach (var dog in dogs)
                        {
                            Console.WriteLine(dog.Name);
                        }
                        break;
                    case "Injection":
                        var injections = rest.Get<Injection>("Injection");
                        foreach (var injection in injections)
                        {
                            Console.WriteLine("{0} ({1}Ft)",
                                injection.Name, injection.Price);
                        }
                        break;
                    case "Owner":
                        var owners = rest.Get<Owner>("Owner");
                        foreach (var owner in owners)
                        {
                            Console.WriteLine("{0} {1}", owner.Name, owner.IdentityCardNumber);
                        }
                        break;
                    default:
                        Console.WriteLine("Error!");
                        break;
                }
                theEndThing();
            }
            void NonCrud(int num)
            {
                switch (num)
                {
                    case 1:
                        var GroupByAndCountByName = rest.Get<KeyValuePair<string, int>>("stat/GroupByAndCountByName");
                        foreach (var owners in GroupByAndCountByName)
                        {
                            Console.WriteLine("asd");
                            Console.WriteLine(owners.Key + " " + owners.Value);
                        }
                        break;
                    case 2:
                        var CountDogs = rest.Get<KeyValuePair<string, int>>("stat/CountDogs");
                        foreach (var dogs in CountDogs)
                        {
                            Console.WriteLine(dogs.Key + " " + dogs.Value);
                        }
                        break;
                    case 3:
                        string onwerName = Console.ReadLine();
                        var CountByOwner = rest.GetSingle<int>("stat/CountByOwner/"+onwerName);
                        Console.WriteLine("{0} {1}",onwerName, CountByOwner);
                        break;
                    case 4:
                        string InjectionName = Console.ReadLine();
                        var CountByInjection = rest.GetSingle<int>("stat/CountByInjection?injection=" + InjectionName);
                        Console.WriteLine("{0} {1}", InjectionName, CountByInjection);
                        break;
                    case 5:
                        var OrderByPrice = rest.Get<Injection>("stat/OrderByPrice");
                        foreach (var injection in OrderByPrice)
                        {
                            Console.WriteLine(injection.Name + " " + injection.Price);
                        }
                        break;
                    case 6:
                        var SumPrice = rest.GetSingle<int>("stat/SumPrice");
                        Console.WriteLine("Sum of injection's prices: {0}", SumPrice);
                        break;
                    default:
                        Console.WriteLine("Error!");
                        break;
                }
                theEndThing();
            }
            void Crud(int num) 
            {
                switch (num)
                {
                    case 1: 
                        
                        break;
                    default:
                        break;
                }
            }
            var listMenu = new ConsoleMenu(args, level: 1)
                      .Add("Dogs", () => List("Dog"))
                      .Add("Injections", () => List("Injection"))
                      .Add("Owners", () => List("Owner"))
                      .Add("Back", ConsoleMenu.Close)
                      .Configure(config =>
                      {
                          config.Selector = "--> ";
                          config.EnableFilter = false;
                          config.Title = "What to list?";
                          config.EnableBreadcrumb = true;
                          config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                      });

            var nonCrudMenu = new ConsoleMenu(args, level: 1)
               .Add("Owner -> GroupByAndCountByName", () => NonCrud(1))
               .Add("Owner -> CountDogs", () => NonCrud(2))
               .Add("Dog -> CountByOwner", () => NonCrud(3))
               .Add("Dog -> CountByInjection", () => NonCrud(4))
               .Add("Injection -> OrderByPrice", () => NonCrud(5))
               .Add("Injection -> SumPrice", () => NonCrud(6))
               .Add("Back", ConsoleMenu.Close)
               .Configure(config =>
               {
                   config.Selector = "--> ";
                   config.EnableFilter = false;
                   config.Title = "Non-CRUD queries";
                   config.EnableBreadcrumb = true;
                   config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
               });

            var mainMenu = new ConsoleMenu(args, level: 0)
             .Add("Lists", () => listMenu.Show())
             .Add("Non-CRUD Methods", () => nonCrudMenu.Show())
             .Add("CRUD Methods", () => Console.WriteLine("Not yet implemented"))
             .Add("Exit", () => Environment.Exit(0))
             .Configure(config =>
             {
                 config.Selector = "--> ";
                 config.EnableFilter = false;
                 config.Title = "Main menu";
                 config.EnableWriteTitle = false;
                 config.EnableBreadcrumb = true;
             });

            mainMenu.Show();
        }
    }
}
