using AXJ0GV_HFT_2021221.Data;
using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Repository
{
    public class InjectionRepository
    {
        DogDbContext context;

        public InjectionRepository(DogDbContext context)
        {
            this.context = context;
        }
        public void Create(Injection injection)
        {
            context.Injections.Add(injection);
            context.SaveChanges();
        }
        public void Delete(int InjectionID)
        {
            context.Injections.Remove(ReadOne(InjectionID));
            context.SaveChanges();
        }

        public IQueryable<Injection> ReadAll()
        {
            return context.Injections;
        }

        public Injection ReadOne(int id)
        {
            return context
                .Injections
                .FirstOrDefault(c => c.Id == id);
        }

        public void Update(Injection injection)
        {
            Injection old = ReadOne(injection.Id);

            old.Name = injection.Name;
            old.Price = injection.Price;
            old.Commonness = injection.Commonness;
            old.DogID = injection.DogID;

            context.SaveChanges();
        }
    }
}
