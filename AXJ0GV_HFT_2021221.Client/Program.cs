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
            Injection injectionHelp = new Injection();
            Owner ownerHelp = new Owner();
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
                        Console.WriteLine("Case1");
                        foreach (var item in GroupByAndCountByName)
                        {
                            Console.WriteLine("asd");
                            Console.WriteLine(item.Key + " " + item.Value);
                        }
                        break;
                    case 2:
                        var CountDogs = rest.Get<KeyValuePair<string, int>>("stat/CountDogs");
                        Console.WriteLine("Case2");
                        foreach (var cso in CountDogs)
                        {
                            Console.WriteLine(cso.Key + " " + cso.Value);
                        }
                        break;
                    case 3:
                        var ownerID = int.Parse(Console.ReadLine());
                        var CountByOwner = rest.GetSingle<int>("stat/CountByOwner/"+ ownerID);
                        var owners = rest.Get<Owner>("stat/GetAllOwner");
                        Owner getOwner = owners.Where(x => x.Id == ownerID).FirstOrDefault();
                        Console.WriteLine("{0} has {1} dog(s)", getOwner.Name, CountByOwner);
                        break;
                    case 4:
                        string injectionID = Console.ReadLine();
                        var CountByInjection = rest.GetSingle<int>("stat/CountByInjection/"+injectionID);
                        Console.WriteLine("The {0} injection has been used at {1} dog(s)", injectionID, CountByInjection);
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
                    //OwnersCrud
                    case 1:
                        Console.WriteLine("You are going to create an Owner.");
                        Console.Write("Name: ");
                        string ownerName = Console.ReadLine();
                        Console.Write("Identity Card Number(3LETTER,3NUMBER): ");
                        string ownerICN = Console.ReadLine();
                        Console.Write("Sex (Male - 0, Female - 1): ");
                        int ownerSex = int.Parse(Console.ReadLine());
                        Sex ownerSexEnum;
                        if (ownerSex == 1) 
                        {
                            ownerSexEnum = Sex.Female;
                        }
                        else
                        {
                            ownerSexEnum = Sex.Male;
                        }
                        ownerHelp = new Owner()
                        {
                            Name = ownerName,
                            IdentityCardNumber = ownerICN,
                            Sex = ownerSexEnum
                        };
                        rest.Post(ownerHelp, "Owner");
                        break;
                    case 2:
                        var owners = rest.Get<Owner>("Owner");
                        foreach (var item in owners)
                        {
                            Console.WriteLine(item.Name + " " + item.IdentityCardNumber);
                        }
                        break;

                    case 3:

                        break;
                    case 4:
                        int numberOwner = int.Parse(Console.ReadLine());
                        rest.Delete(numberOwner, "Owner" + numberOwner);
                        break;
                    //InjectionsCrud
                    case 5:
                        Console.WriteLine("You are going to create an Injection.");
                        Console.Write("0 - {0}\n1 - {1}\n2 - {2}\n3 - {3}\n4 - {4}\n5 - {5}\n6 - {6}\n7 - {7}: ", 
                            InjectionName.Bordetella_Bronchiseptica,
                            InjectionName.Canine_Distemper,
                            InjectionName.Canine_Hepatitis,
                            InjectionName.Canine_Parainfluenza,
                            InjectionName.Heartworm,
                            InjectionName.Leptospirosis,
                            InjectionName.Parvovirus,
                            InjectionName.Rabies);
                        int InjectionNameNumber = int.Parse(Console.ReadLine());
                        InjectionName injectionHelpName;
                        switch (InjectionNameNumber)
                        {
                            case 0: injectionHelpName = InjectionName.Bordetella_Bronchiseptica; break;
                            case 1: injectionHelpName = InjectionName.Canine_Distemper; break;
                            case 2: injectionHelpName = InjectionName.Canine_Hepatitis; break;
                            case 3: injectionHelpName = InjectionName.Canine_Parainfluenza; break;
                            case 4: injectionHelpName = InjectionName.Heartworm; break;
                            case 5: injectionHelpName = InjectionName.Leptospirosis; break;
                            case 6: injectionHelpName = InjectionName.Parvovirus; break;
                            case 7: injectionHelpName = InjectionName.Rabies; break;
                            default:
                                injectionHelpName = InjectionName.Bordetella_Bronchiseptica;
                                break;
                        }
                        Commonness injectionHelpCommonness;
                        Console.Write("0 - {0}\n1 - {1}\n2 - {2}\n3 - {3}: ", 
                            Commonness.Once, 
                            Commonness.Monthly, 
                            Commonness.Half_year, 
                            Commonness.Yearly);
                        int injectionHelpCommonnesNumber = int.Parse(Console.ReadLine());
                        switch (injectionHelpCommonnesNumber)
                        {
                            case 0: injectionHelpCommonness = Commonness.Once; break;
                            case 1: injectionHelpCommonness = Commonness.Monthly; break;
                            case 2: injectionHelpCommonness = Commonness.Half_year; break;
                            case 3: injectionHelpCommonness = Commonness.Yearly; break;
                            default:
                                injectionHelpCommonness = Commonness.Once;
                                break;
                        }
                        Console.Write("Price: ");
                        int injectionHelpPrice = int.Parse(Console.ReadLine());
                        injectionHelp = new Injection()
                        {
                            Name = injectionHelpName,
                            Commonness = injectionHelpCommonness,
                            Price = injectionHelpPrice
                        };
                        rest.Post(injectionHelp, "Injection");
                        break;
                    case 6:
                        var injections = rest.Get<Injection>("Injection");
                        foreach (var item in injections)
                        {
                            Console.WriteLine(item.Name + " " + item.Price);
                        }
                        break;
                    case 7:

                        break;
                    case 8:
                        int numberInjection = int.Parse(Console.ReadLine());
                        rest.Delete(numberInjection, "Injection" + numberInjection);
                        break;
                    //DogsCrud
                    case 9:
                        Console.WriteLine("You are going to create a Dog.");
                        Console.Write("Name: ");
                        string dogName = Console.ReadLine();
                        Sex dogSex;
                        Console.Write("0 - {0}\n1 - {1}: ", Sex.Male, Sex.Female);
                        int dogSexNumber = int.Parse(Console.ReadLine());
                        if (dogSexNumber == 0)
                        {
                            dogSex = Sex.Male;
                        }
                        else
                        {
                            dogSex = Sex.Female;
                        }
                        Console.Write("Species: ");
                        string dogSpecies = Console.ReadLine();
                        Dog newDog = new Dog()
                        {
                            Name = dogName,
                            Sex = dogSex,
                            Species = dogSpecies,
                            OwnerID = 1,
                            InjectionID = 2
                        };
                        rest.Post(newDog, "Dog");
                        break;
                    case 10:
                        var dogs = rest.Get<Dog>("Dog");
                        foreach (var item in dogs)
                        {
                            Console.WriteLine(item.Name + " " + item.Species);
                        }

                        break;
                    case 11: 
                        
                        break;
                    case 12:
                        int numberDog = int.Parse(Console.ReadLine());
                        rest.Delete(numberDog, "Dog" + numberDog);
                        break;
                    
                    
                    default:
                        Console.WriteLine("Error!");
                        break;
                }
                theEndThing();
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
            var CrudMenu = new ConsoleMenu(args, level: 1)
               .Add("Owner -> Create", () => Crud(1))
               .Add("Owner -> Read", () => Crud(2))
               .Add("Owner -> Update", () => Crud(3))
               .Add("Owner -> Delete", () => Crud(4))
               .Add("Injection -> Create", () => Crud(5))
               .Add("Injection -> Read", () => Crud(6))
               .Add("Injection -> Update", () => Crud(7))
               .Add("Injection -> Delete", () => Crud(8))
               .Add("Dog -> Create", () => Crud(9))
               .Add("Dog -> Read", () => Crud(10))
               .Add("Dog -> Update", () => Crud(11))
               .Add("Dog -> Delete", () => Crud(12))
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
             .Add("CRUD Methods", () => CrudMenu.Show())
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
