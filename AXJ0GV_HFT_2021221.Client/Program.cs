using AXJ0GV_HFT_2021221.Models;
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
            Console.Write('A');
            System.Threading.Thread.Sleep(200);
            Console.Write('X');
            System.Threading.Thread.Sleep(200);
            Console.Write('J');
            System.Threading.Thread.Sleep(200);
            Console.Write('0');
            System.Threading.Thread.Sleep(200);
            Console.Write('G');
            System.Threading.Thread.Sleep(200);
            Console.Write('V');
            System.Threading.Thread.Sleep(200);
            Console.Write(" - ");
            System.Threading.Thread.Sleep(200);
            Console.Write(" F ");
            System.Threading.Thread.Sleep(200);
            Console.Write(" É ");
            System.Threading.Thread.Sleep(200);
            Console.Write(" L ");
            System.Threading.Thread.Sleep(200);
            Console.Write(" É ");
            System.Threading.Thread.Sleep(200);
            Console.Write(" V ");
            System.Threading.Thread.Sleep(200);
            Console.Write(" E ");
            System.Threading.Thread.Sleep(200);
            Console.Write(" S ");
            System.Threading.Thread.Sleep(200);

            RestService rest = new RestService("http://localhost:18683");
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
                            Console.WriteLine("{0,-3} {1,-10} {2,-10} {3,-6}", dog.Id, dog.Name, dog.Species, dog.Sex);
                        }
                        break;
                    case "Injection":
                        var injections = rest.Get<Injection>("Injection");
                        foreach (var injection in injections)
                        {
                            Console.WriteLine("{0,-3} {1,-30} {2,-10} {3,-10}",
                                injection.Id, injection.Name, injection.Price, injection.Commonness);
                        }
                        break;
                    case "Owner":
                        var owners = rest.Get<Owner>("Owner");
                        foreach (var owner in owners)
                        {
                            Console.WriteLine("{0,-3} {1,-10} {2,-7} {3,-6}", owner.Id, owner.Name, owner.IdentityCardNumber, owner.Sex);
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
                        var OrderByIdentityCardNumber = rest.Get<Owner>("stat/OrderByIdentityCardNumber");
                        foreach (var item in OrderByIdentityCardNumber)
                        {
                            Console.WriteLine(item.Name + " " + item.IdentityCardNumber);
                        }
                        break;
                    case 2:
                        var usedInjections = rest.Get<Injection>("stat/GetUsedInjections");
                        foreach (var item in usedInjections)
                        {
                            Console.WriteLine(item.Name + " " + item.Price);
                        }
                        break;
                    case 3:
                        Console.Write("Enter the owner's ID: (Kristof - 1, Dominik - 2, Eszter - 3): ");
                        var ownerID = int.Parse(Console.ReadLine());
                        var CountByOwner = rest.GetSingle<int>("stat/CountByOwner/"+ ownerID);
                        var owners = rest.Get<Owner>("Owner");
                        Owner getOwner = owners.Where(x => x.Id == ownerID).FirstOrDefault();
                        Console.WriteLine("{0} has {1} dog(s)", getOwner.Name, CountByOwner);
                        break;
                    case 4:
                        int index = 1;
                        foreach (InjectionName injectionName in (InjectionName[])Enum.GetValues(typeof(InjectionName)))
                        {
                            Console.WriteLine("{0} - {1}", index++, injectionName);
                        }
                        int injectionID = int.Parse(Console.ReadLine());
                        var CountByInjection = rest.GetSingle<int>("stat/CountByInjection/"+injectionID);
                        var injections = rest.Get<Injection>("Injection");
                        Injection getInjection = injections.Where(x => x.Id == injectionID).FirstOrDefault();
                        Console.WriteLine("The {0} injection has been used at {1} dog(s)", getInjection.Name, CountByInjection);
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
                        Console.Clear();
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
                        Owner ownerHelp = new Owner()
                        {
                            Name = ownerName,
                            IdentityCardNumber = ownerICN,
                            Sex = ownerSexEnum
                        };
                        rest.Post(ownerHelp, "Owner");
                        break;
                    case 2:
                        Console.Clear();
                        var owners = rest.Get<Owner>("Owner");
                        foreach (var item in owners)
                        {
                            Console.WriteLine(item.Id + " " + item.Name + " " + item.IdentityCardNumber + " " + item.Sex);
                        }
                        break;

                    case 3:
                        Console.Clear();
                        var ownerList = rest.Get<Owner>("Owner");
                        Console.WriteLine("Which owner do you want to update? - CHOOSE AN ID");
                        foreach (var item in ownerList)
                        {
                            Console.WriteLine(item.Id + " " + item.Name + " " + item.IdentityCardNumber + item.Sex);
                        }
                        int ownerIdForChange = int.Parse(Console.ReadLine());
                        Owner ownerforChange = ownerList.Where(x => x.Id == ownerIdForChange).FirstOrDefault();
                        string name = ownerforChange.Name;
                        string icn = ownerforChange.IdentityCardNumber;
                        Sex sexforchange = ownerforChange.Sex;
                        Console.WriteLine("Owner's information: ");
                        Console.Write("Name: {0}\nIdentity Card Number: {1}\nSex: {2}", name, icn, sexforchange);
                        Console.WriteLine("\nWhat do you want to change? 1 - Name, 2 - Identity Card Number, 3 - Sex");
                        int informationChange = int.Parse(Console.ReadLine());
                        if (informationChange == 1)
                        {
                            Console.Write("Enter a new name: ");
                            name = Console.ReadLine();
                        }
                        else if (informationChange == 2)
                        {
                            Console.Write("Enter a identity card number: ");
                            icn = Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("Enter a new Sex: 0 - Male, 1 - Female");
                            int help = int.Parse(Console.ReadLine());
                            if (help == 0)
                            {
                                sexforchange = Sex.Male;
                            }
                            else
                            {
                                sexforchange = Sex.Female;
                            }
                        }
                        Owner updatedOwner = new Owner()
                        {
                            Id = ownerforChange.Id,
                            Name = name,
                            IdentityCardNumber = icn,
                            Sex = sexforchange
                        };
                        rest.Put(updatedOwner, "Owner");
                        break;
                    case 4:
                        Console.Clear();
                        var ownerforDelete = rest.Get<Owner>("Owner");
                        foreach (var item in ownerforDelete)
                        {
                            Console.WriteLine(item.Id + " " + item.Name);
                        }
                        Console.Write("Enter the owner's ID you want to delete: ");
                        int numberOwner = int.Parse(Console.ReadLine());
                        rest.Delete(numberOwner, "Owner");
                        break;
                    //InjectionsCrud
                    case 5:
                        Console.Clear();
                        Console.WriteLine("You are going to create an Injection.");
                        int index = 1;
                        foreach (InjectionName injectionName in (InjectionName[])Enum.GetValues(typeof(InjectionName)))
                        {
                            Console.WriteLine("{0} - {1}", index++, injectionName);
                        }
                        int InjectionNameNumber = int.Parse(Console.ReadLine());
                        InjectionName injectionHelpName = (InjectionName)InjectionNameNumber;
                        index = 1;
                        foreach (Commonness commonness in (Commonness[])Enum.GetValues(typeof(Commonness)))
                        {
                            Console.WriteLine("{0} - {1}", index++, commonness);
                        }
                        int injectionHelpCommonnesNumber = int.Parse(Console.ReadLine());
                        Commonness injectionHelpCommonnes = (Commonness)injectionHelpCommonnesNumber;
                        Console.Write("Price: ");
                        int injectionHelpPrice = int.Parse(Console.ReadLine());
                        Injection injectionHelp = new Injection()
                        {
                            Name = injectionHelpName,
                            Commonness = injectionHelpCommonnes,
                            Price = injectionHelpPrice
                        };
                        rest.Post(injectionHelp, "Injection");
                        break;
                    case 6:
                        Console.Clear();
                        var injections = rest.Get<Injection>("Injection");
                        foreach (var item in injections)
                        {
                            Console.WriteLine("{0,-3} {1,-30} {2,-10} {3,-10}",item.Id, item.Name, item.Price, item.Commonness);
                        }
                        break;
                    case 7:
                        Console.Clear();
                        var injectionsList = rest.Get<Injection>("Injection");
                        foreach (var item in injectionsList)
                        {
                            Console.WriteLine(item.Id + " " + item.Name + " " + item.Commonness + " " + item.Price);
                        }
                        Console.WriteLine("Which Injection do you want to change? CHOOSE AN ID");
                        int chosenInjectionNumber = int.Parse(Console.ReadLine());
                        Injection chosenInjection = injectionsList.Where(x => x.Id == chosenInjectionNumber).FirstOrDefault();
                        InjectionName oldName = chosenInjection.Name;
                        int? oldPrice = chosenInjection.Price;
                        Commonness oldCommonness = chosenInjection.Commonness;
                        Console.WriteLine("What information do you want to change? 1 - Name, 2 - Commonness, 3 - Price");
                        int informationForChange = int.Parse(Console.ReadLine());
                        if (informationForChange == 1)
                        {
                            index = 0;
                            foreach (InjectionName injectionName in (InjectionName[])Enum.GetValues(typeof(InjectionName)))
                            {
                                Console.WriteLine("{0} - {1}", index++, injectionName);
                            }
                            Console.WriteLine("For what do you want to change your injection's Name? CHOOSE AN ID");
                            int newInjectionNameNumber = int.Parse(Console.ReadLine());
                            oldName = (InjectionName)newInjectionNameNumber;
                        }
                        else if (informationForChange == 2)
                        {
                            index = 0;
                            foreach (Commonness commonness in (Commonness[])Enum.GetValues(typeof(Commonness)))
                            {
                                Console.WriteLine("{0} - {1}", index++, commonness);
                            }
                            Console.Write("For what do you want to change your injection's commonness CHOOSE AN ID? ");
                            int newInjectionCommonnessNumber = int.Parse(Console.ReadLine());
                            oldCommonness = (Commonness)newInjectionCommonnessNumber;
                        }
                        else
                        {
                            Console.Write("Enter the new price: ");
                            oldPrice = int.Parse(Console.ReadLine());
                        }
                        Injection updatedInjection = new Injection
                        {
                            Id = chosenInjection.Id,
                            Name = oldName,
                            Commonness = oldCommonness,
                            Price = oldPrice
                        };
                        rest.Put(updatedInjection, "Injection");
                        break;
                    case 8:
                        Console.Clear();
                        var deleteInjectionList = rest.Get<Injection>("Injection");
                        foreach (var item in deleteInjectionList)
                        {
                            Console.WriteLine(item.Id + " " + item.Name);
                        }
                        Console.Write("Enter the injection's ID you want to delete: ");
                        int numberInjection = int.Parse(Console.ReadLine());
                        rest.Delete(numberInjection, "Injection");
                        break;
                    //DogsCrud
                    case 9:
                        Console.Clear();
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
                        List<Owner> ownersHelpList = rest.Get<Owner>("Owner");
                        int dogOwnerId = 1;
                        int dogInjectionId = 1;
                        List<Injection> injectionHelpList = rest.Get<Injection>("Injection");
                        Console.WriteLine("Choose an Owner: ");
                        foreach (var item in ownersHelpList)
                        {
                            Console.WriteLine(item.Name + " " + item.Id);
                        }
                        dogOwnerId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Choose an Injection: ");
                        foreach (var item in injectionHelpList)
                        {
                            Console.WriteLine(item.Name + " " + item.Id);
                        }
                        dogInjectionId = int.Parse(Console.ReadLine());
                        Dog newDog = new Dog()
                        {
                            Name = dogName,
                            Sex = dogSex,
                            Species = dogSpecies,
                            OwnerID = dogOwnerId,
                            InjectionID = dogInjectionId
                        };
                        rest.Post(newDog, "Dog");
                        break;
                    case 10:
                        Console.Clear();
                        var dogs = rest.Get<Dog>("Dog");
                        foreach (var item in dogs)
                        {
                            Console.WriteLine(item.Id + " " + item.Name + " " + item.Species + " " + item.Sex);
                        }
                        break;
                    case 11:
                        Console.Clear();
                        var dogsList = rest.Get<Dog>("Dog");
                        foreach (var item in dogsList)
                        {
                            Console.WriteLine(item.Id + " " + item.Name + " " + item.Sex + " " + item.Species);
                        }
                        Console.Write("Choose a dog (BY ID): ");
                        int choosenDogNumber = int.Parse(Console.ReadLine());
                        Dog chosenDog = dogsList.Where(x => x.Id == choosenDogNumber).FirstOrDefault();
                        string oldNameDog = chosenDog.Name;
                        Sex oldSex = chosenDog.Sex;
                        string oldSpecies = chosenDog.Species;
                        Console.WriteLine("Which information do you want to update? 1 - Name, 2 - Sex, 3 - Species");
                        int informationForChangeDog = int.Parse(Console.ReadLine());
                        if (informationForChangeDog == 1)
                        {
                            Console.Write("Enter a new name for the dog: ");
                            oldNameDog = Console.ReadLine();
                        }
                        else if (informationForChangeDog == 2)
                        {
                            Console.Write("Choose a new Sex for the dog: 0 - Male, 1 - Female");
                            oldSex = (Sex)int.Parse(Console.ReadLine());
                        }
                        else
                        {
                            Console.Write("Enter the new species for the dog: ");
                            oldSpecies = Console.ReadLine();
                        }
                        Dog updatedDog = new Dog()
                        {
                            Id = chosenDog.Id,
                            Name = oldNameDog,
                            Sex = oldSex,
                            Species = oldSpecies,
                            OwnerID = chosenDog.Id,
                            InjectionID = chosenDog.InjectionID
                        };
                        rest.Put(updatedDog, "Dog");
                        break;
                    case 12:
                        Console.Clear();
                        var deleteDogList = rest.Get<Dog>("Dog");
                        foreach (var item in deleteDogList)
                        {
                            Console.WriteLine(item.Id + " " + item.Name);
                        }
                        Console.Write("Enter the dog's ID you want to delete: ");
                        int numberDog = int.Parse(Console.ReadLine());
                        rest.Delete(numberDog, "Dog");
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
               .Add("Owner -> OrderByIdentityCardNumber", () => NonCrud(1))
               .Add("Dog -> GetUsedInjections", () => NonCrud(2))
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
                   config.Title = "CRUD queries";
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
