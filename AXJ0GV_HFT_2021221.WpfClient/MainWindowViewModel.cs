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
                //SetProperty(ref selectedDog, value);
                Dog oldog = value;
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
                    selectedDogsByOwner[selectedDogsByOwner.IndexOf(oldog)] = selectedDog;
                    OnPropertyChanged();
                    (RemoveDoggo as RelayCommand).NotifyCanExecuteChanged();
                    (ModifyDoggo as RelayCommand).NotifyCanExecuteChanged();
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
            get { return selectedOwner; }
            set 
            {
                selectedDog = null;
                selectedInjection = null;
                selectedDogsByOwner.Clear();
                dogsHelp = dogs.ToList();
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
                }
                if (SelectedOwner != null)
                {
                    for (int i = 0; i < dogsHelp.Count; i++)
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
                    });
                }
                );
                RemovePerson = new RelayCommand(() =>
                {
                   owners.Delete(SelectedOwner.Id);
                }, () => SelectedOwner != null
                );
                RemoveDoggo = new RelayCommand(() =>
                {
                   dogs.Delete(SelectedDog.Id);
                }, () => SelectedDog != null
                );
                RemoveInjection = new RelayCommand(() =>
                {
                   injections.Delete(SelectedInjection.Id);
                }, () => SelectedInjection != null
                );
                ModifyPerson = new RelayCommand(() =>
                {
                    owners.Update(SelectedOwner);
                }, () => SelectedOwner != null);

                ModifyDoggo = new RelayCommand(() =>
                {
                    dogs.Update(SelectedDog);
                }, () => SelectedDog != null);

                ModifyInjection = new RelayCommand(() =>
                {
                    injections.Update(SelectedInjection);
                }, () => SelectedInjection!= null);
            }
        }
    }
}
