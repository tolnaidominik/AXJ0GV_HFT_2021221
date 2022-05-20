using AXJ0GV_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AXJ0GV_HFT_2021221.WpfClient
{
    public class DogWindowViewModel : ObservableRecipient
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public RestCollection<Dog> dogs { get; set; }
        public RestCollection<Owner> owners { get; set; }
        public RestCollection<Injection> injections { get; set; }
        public ICommand AddDoggo { get; set; }
        public ICommand ClearDoggo { get; set; }
        public ICommand RemoveDoggo { get; set; }
        public ICommand ModifyDoggo { get; set; }
        private Injection selectedInjection;
        public Injection SelectedInjection
        {
            get 
            { 
                return selectedInjection; 
            }
            set 
            {
                SetProperty(ref selectedInjection, value); 
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
                SetProperty(ref selectedOwner, value);
            }
        }
        private Dog selectedDoggo;

        public Dog SelectedDoggo
        {
            get 
            {
                return selectedDoggo; 
            }
            set 
            {
                if (value != null)
                {
                    selectedDoggo = new Dog()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Species = value.Species,
                        Sex = value.Sex,
                        OwnerID = value.Id,
                        InjectionID = value.Id
                    };
                }
                OnPropertyChanged();
                (RemoveDoggo as RelayCommand).NotifyCanExecuteChanged();
                (ModifyDoggo as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public DogWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                owners = new RestCollection<Owner>("http://localhost:18683/", "Owner", "hub");
                dogs = new RestCollection<Dog>("http://localhost:18683/", "Dog", "hub");
                injections = new RestCollection<Injection>("http://localhost:18683/", "Injection", "hub");
                AddDoggo = new RelayCommand(() =>
                {
                    dogs.Add(new Dog()
                    {
                        Name = SelectedDoggo.Name,
                        Species = SelectedDoggo.Species,
                        Sex = SelectedDoggo.Sex,
                        OwnerID = SelectedOwner.Id,
                        InjectionID = SelectedInjection.Id
                    });
                });
                ClearDoggo = new RelayCommand(() =>
                {
                    SelectedDoggo = new Dog();
                    SelectedOwner = new Owner();
                    SelectedInjection = new Injection();
                });
                RemoveDoggo = new RelayCommand(() =>
                {
                    dogs.Delete(SelectedDoggo.Id);
                    SelectedDoggo = new Dog();
                }, () => SelectedDoggo != null && SelectedDoggo.Name != null
                );
                ModifyDoggo = new RelayCommand(() =>
                {
                    SelectedDoggo.OwnerID = SelectedOwner.Id;
                    SelectedDoggo.InjectionID = SelectedInjection.Id;
                    dogs.Update(SelectedDoggo);
                }, () => SelectedDoggo != null && SelectedDoggo.Name != null && SelectedOwner != null && SelectedInjection != null);

                SelectedDoggo = new Dog();
                SelectedInjection = new Injection();
                SelectedOwner = new Owner();
            }
        }
    }
}
