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
    public class OwnerWindowViewModel : ObservableRecipient
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public ICommand AddOwner { get; set; }
        public ICommand ClearOwner { get; set; }
        public ICommand RemoveOwner { get; set; }
        public ICommand ModifyOwner { get; set; }
        public RestCollection<Owner> owners { get; set; }

        private Owner selectedOwner;
        public Owner SelectedOwner
        {
            get
            {
                return selectedOwner;
            }
            set
            {
                if(value != null)
                {
                    selectedOwner = new Owner()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        IdentityCardNumber = value.IdentityCardNumber,
                        Sex = value.Sex
                    };
                }
                OnPropertyChanged();
                (RemoveOwner as RelayCommand).NotifyCanExecuteChanged();
                (ModifyOwner as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public OwnerWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                owners = new RestCollection<Owner>("http://localhost:18683/", "owner", "hub");

                AddOwner = new RelayCommand(() =>
                {
                    owners.Add(new Owner()
                    {
                        Name = SelectedOwner.Name,
                        IdentityCardNumber = SelectedOwner.IdentityCardNumber,
                        Sex = SelectedOwner.Sex
                    });
                });

                ClearOwner = new RelayCommand(() => SelectedOwner = new Owner());

                RemoveOwner = new RelayCommand(() =>
                {
                    owners.Delete(SelectedOwner.Id);
                    SelectedOwner = new Owner();

                },() => SelectedOwner != null && SelectedOwner.Name != null
                );
                ModifyOwner = new RelayCommand(() =>
                {
                    owners.Update(SelectedOwner);

                }, () => SelectedOwner != null && SelectedOwner.Name != null);

                SelectedOwner = new Owner();
            }
        }
    }
}
