using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Repository
{
    interface IInjectionRepository
    {
        void Create(Injection injection);
        Injection ReadOne(int id); // GetById
        IQueryable<Injection> ReadAll(); // Query
        void Update(Injection injection);
        void Delete(int injectionId);
    }
}
