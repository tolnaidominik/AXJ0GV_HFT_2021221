using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AXJ0GV_HFT_2021221.WpfClient
{
    public class NonCrudWindowViewModel
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public List<Owner> OrderByIdentityCardNumber { get; }

        public int CountByOwnerKritya { get; }
        public int CountByOwnerDoma { get; }
        public int CountByOwnerTubi { get; }

        public int CountByInjection { get; }

        public List<Injection> GetUsedInjections { get; }
        public List<Injection> OrderByPrice { get; }

        public int SumPrice { get; }


        public NonCrudWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                OrderByIdentityCardNumber = new RestService("http://localhost:18683/stat/", "OrderByIdentityCardNumber").Get<Owner>("OrderByIdentityCardNumber");

                CountByOwnerKritya = new RestService("http://localhost:18683/stat/", "CountByOwner/1").GetSingle<int>("CountByOwner/1");
                CountByOwnerDoma = new RestService("http://localhost:18683/stat/", "CountByOwner/2").GetSingle<int>("CountByOwner/2");
                CountByOwnerTubi = new RestService("http://localhost:18683/stat/", "CountByOwner/3").GetSingle<int>("CountByOwner/3");

                CountByInjection = new RestService("http://localhost:18683/stat/", "CountByInjection/3").GetSingle<int>("CountByInjection/3");

                GetUsedInjections = new RestService("http://localhost:18683/stat/", "GetUsedInjections").Get<Injection>("GetUsedInjections");
                OrderByPrice = new RestService("http://localhost:18683/stat/", "OrderByPrice").Get<Injection>("OrderByPrice");

                SumPrice = new RestService("http://localhost:18683/stat/", "SumPrice").GetSingle<int>("SumPrice");
            }
        }
    }
}
