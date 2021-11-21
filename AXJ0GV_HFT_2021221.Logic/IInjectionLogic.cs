using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Logic
{
    public interface IInjectionLogic
    {
        void Create(Injection injection);
        IQueryable<Injection> ReadAll();
        void Update(Injection injection);
        void Delete(int injectionId);
        List<Injection> OrderByPrice();
    }
}
