using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Logic
{
    public interface IDogLogic
    {
        void Create(Dog dog);
        IQueryable<Dog> ReadAll();
        Dog Read(int id);
        void Update(Dog dog);
        void Delete(int dogId);
        int CountByOwner(int ownerID);
        int CountByInjection(int injectionID);
        public List<Injection> GetUsedInjections();
    }
}
