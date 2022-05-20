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
    public class InjectionWindowViewModel : ObservableRecipient
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public ICommand AddInjection { get; set; }
        public ICommand ClearInjection { get; set; }
        public ICommand RemoveInjection { get; set; }
        public ICommand ModifyInjection { get; set; }
        public RestCollection<Injection> Injections { get; set; }

        private Injection selectedInjection;
        public Injection SelectedInjection
        {
            get
            {
                return selectedInjection;
            }
            set
            {
                if (value != null)
                {
                    selectedInjection = new Injection()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Price = value.Price,
                        Commonness = value.Commonness
                    };
                }
                OnPropertyChanged();
                (RemoveInjection as RelayCommand).NotifyCanExecuteChanged();
                (ModifyInjection as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public InjectionWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Injections = new RestCollection<Injection>("http://localhost:18683/", "injection", "hub");

                AddInjection = new RelayCommand(() =>
                {
                    Injections.Add(new Injection()
                    {
                        Name = SelectedInjection.Name,
                        Price = SelectedInjection.Price,
                        Commonness = SelectedInjection.Commonness
                    });
                });

                ClearInjection = new RelayCommand(() => SelectedInjection = new Injection());

                RemoveInjection = new RelayCommand(() =>
                {
                    Injections.Delete(SelectedInjection.Id);
                    SelectedInjection = new Injection();
                }, () => SelectedInjection != null && SelectedInjection.Name != InjectionName.Null);
                ModifyInjection = new RelayCommand(() =>
                {
                    Injections.Update(SelectedInjection);

                }, () => SelectedInjection != null && SelectedInjection.Name != InjectionName.Null);

                SelectedInjection = new Injection();
            }
        }
    }
}
