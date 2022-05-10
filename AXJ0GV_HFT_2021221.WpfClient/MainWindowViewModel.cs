using AXJ0GV_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using AXJ0GV_HFT_2021221.WpfClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace AXJ0GV_HFT_2021221.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public ICommand AddPerson{ get; set; }
        public ICommand AddDoggo{ get; set; }
        public ICommand AddInjection{ get; set; }
        public ICommand RemovePerson{ get; set; }
        public ICommand RemoveDoggo{ get; set; }
        public ICommand RemoveInjection{ get; set; }
        public ICommand ModifyPerson{ get; set; }
        public ICommand ModifyDoggo { get; set; }
        public ICommand ModifyInjection { get; set; }

        public RestCollection<Dog> dogs { get; set; }
        public List<Dog> dogsHelp { get; set; }
        public List<Injection> injectionHelp { get; set; }
        public RestCollection<Owner> owners { get; set; }
        public RestCollection<Injection> injections { get; set; }

        private Dog selectedDog;

        public Dog SelectedDog
        {
            get 
            {
                return selectedDog; 
            }
            set 
            {
                selectedInjection = null;
                (AddDoggo as RelayCommand).NotifyCanExecuteChanged();
                (RemoveInjection as RelayCommand).NotifyCanExecuteChanged();
                (ModifyInjection as RelayCommand).NotifyCanExecuteChanged();
                //SetProperty(ref selectedDog, value);
                if (value != null)
                {
                    selectedDog = new Dog()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Species = value.Species,
                        Sex = value.Sex,
                        OwnerID = value.OwnerID,
                        InjectionID = value.InjectionID
                    };
                    selectedDogsByOwner[selectedDogsByOwner.IndexOf(value)] = selectedDog;
                    OnPropertyChanged();
                    (RemoveDoggo as RelayCommand).NotifyCanExecuteChanged();
                    (ModifyDoggo as RelayCommand).NotifyCanExecuteChanged();
                    (AddInjection as RelayCommand).NotifyCanExecuteChanged();

                }
                selectedInjectionsByDog.Clear();
                injectionHelp = injections.ToList();
                foreach (var item in selectedDogsByOwner)
                {
                    if(item.Id == SelectedDog.Id)
                    selectedInjectionsByDog.Add(injectionHelp.FirstOrDefault(x => x.Id == item.InjectionID));
                }
            }
        }

        private Owner selectedOwner;

        public Owner SelectedOwner
        {
            get 
            {
                return selectedOwner; 
            }
            set 
            {
                selectedDogsByOwner.Clear();
                selectedInjection = null;
                //SetProperty(ref selectedOwner, value);
                if(value != null)
                {
                    selectedOwner = new Owner()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        IdentityCardNumber = value.IdentityCardNumber,
                        Sex = value.Sex,
                        Dogs = value.Dogs
                    };
                    OnPropertyChanged();
                    (RemovePerson as RelayCommand).NotifyCanExecuteChanged();
                    (ModifyPerson as RelayCommand).NotifyCanExecuteChanged();
                    (AddDoggo as RelayCommand).NotifyCanExecuteChanged();
                }
                if (SelectedOwner != null)
                {
                    dogsHelp = dogs.ToList();
                    int counter = dogsHelp.Count;
                    for (int i = 0; i < counter; i++)
                    {
                        Dog selectedDoggo = dogsHelp.FirstOrDefault(x => x.OwnerID == SelectedOwner.Id);
                        if (selectedDoggo != null)
                        {
                            selectedDogsByOwner.Add(selectedDoggo);
                            dogsHelp.Remove(selectedDoggo);
                            selectedDoggo = null;
                        }
                    }
                }
            }
        }
        private Injection selectedInjection;
        private Injection InjectionBeforeDelete;

        public Injection SelectedInjection
        {
            get { return selectedInjection; }
            set 
            {
                //SetProperty(ref selectedInjection, value);

                if (value != null)
                {
                    selectedInjection = new Injection()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Price = value.Price,
                        Commonness = value.Commonness
                    };
                    OnPropertyChanged();
                    (RemoveInjection as RelayCommand).NotifyCanExecuteChanged();
                    (ModifyInjection as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        private ObservableCollection<Injection> selectedInjectionsByDog;
        int index_szamlalo = 4;

        public ObservableCollection<Injection> SelectedInjectionsByDog
        {
            get { return selectedInjectionsByDog; }
            set { SetProperty(ref selectedInjectionsByDog, value); }
        }

        private ObservableCollection<Dog> selectedDogsByOwner;
        public ObservableCollection<Dog> SelectedDogsByOwner
        {
            get
            {
                return selectedDogsByOwner;
            }
            set
            {
                SetProperty(ref selectedDogsByOwner, value);
            }
        }

        public ICommand LoadCommand { get; set; }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                dogs = new RestCollection<Dog>("http://localhost:18683/", "Dog");
                owners = new RestCollection<Owner>("http://localhost:18683/", "Owner");
                injections = new RestCollection<Injection>("http://localhost:18683/", "Injection");
                selectedDogsByOwner = new ObservableCollection<Dog>();
                selectedInjectionsByDog = new ObservableCollection<Injection>();
                AddPerson = new RelayCommand(() =>
                {
                    owners.Add(new Owner
                    {
                        Name = "Jani",
                        IdentityCardNumber = "LOL420"

                    });
                }
                );
                AddDoggo = new RelayCommand(() =>
                {
                    Dog newDoggo;
                    dogs.Add(newDoggo = new Dog
                    {
                        Name = "Lábhoz",
                        Species = "French Bulldog",
                        Sex = Sex.Male,
                        OwnerID = SelectedOwner.Id,
                        InjectionID = 1
                    });
                    newDoggo.Id = index_szamlalo++;
                    selectedDog = newDoggo;
                    selectedDogsByOwner.Add(newDoggo);
                }, () => SelectedOwner != null
                );
                AddInjection = new RelayCommand(() =>
                {
                    selectedInjectionsByDog.Clear();
                    Injection newInjection;
                    injections.Add(newInjection = new Injection
                    {
                        Name = InjectionName.Rabies,
                        Price = 69420,
                        Commonness = Commonness.Once,
                    });
                    newInjection.Id = injections.Count() + 1;
                    SelectedDog.InjectionID = newInjection.Id;
                    dogs.Update(SelectedDog);
                    selectedInjectionsByDog.Add(newInjection);
                }, () => SelectedDog != null && selectedInjectionsByDog.Count == 0
                );
                RemovePerson = new RelayCommand(() =>
                {
                    owners.Delete(SelectedOwner.Id);
                    selectedOwner = null;
                    (RemovePerson as RelayCommand).NotifyCanExecuteChanged();
                }, () => SelectedOwner != null
                );
                RemoveDoggo = new RelayCommand(() =>
                {
                    Owner needOwner = owners.FirstOrDefault(x => x.Id == SelectedDog.OwnerID);
                    selectedOwner = needOwner;
                    dogs.Delete(SelectedDog.Id);
                    selectedDogsByOwner.Remove(SelectedDog);
                    selectedDog = null;
                    (RemoveDoggo as RelayCommand).NotifyCanExecuteChanged();
                }, () => SelectedDog != null
                );
                RemoveInjection = new RelayCommand(() =>
                {
                    InjectionBeforeDelete = SelectedInjection;
                    selectedInjectionsByDog.Clear();
                    SelectedDog.InjectionID = 107;
                    dogs.Update(SelectedDog);
                    selectedInjectionsByDog.Add(SelectedInjection);
                    (RemoveInjection as RelayCommand).NotifyCanExecuteChanged();
                }, () => SelectedInjection != null && selectedInjectionsByDog.Count > 0
                );
                ModifyPerson = new RelayCommand(() =>
                {
                    owners.Update(SelectedOwner);
                }, () => SelectedOwner != null);

                ModifyDoggo = new RelayCommand(() =>
                {
                    selectedDogsByOwner.Remove(SelectedDog);
                    dogs.Update(SelectedDog);
                    selectedDogsByOwner.Add(SelectedDog);
                }, () => SelectedDog != null);

                ModifyInjection = new RelayCommand(() =>
                {
                    int torolnivaloID = SelectedInjection.Id;
                    Injection getInjection = SelectedInjection;
                    foreach (Injection item in injections)
                    {
                        getInjection = injections.FirstOrDefault(x => x.Name == SelectedInjection.Name);
                    }
                    SelectedInjection = getInjection;
                    selectedInjectionsByDog.Remove(selectedInjectionsByDog.FirstOrDefault(x => x.Id == torolnivaloID));
                    injections.Update(SelectedInjection);
                    selectedInjectionsByDog.Add(SelectedInjection);
                }, () => SelectedInjection!= null);
            }
        }
    }
}
