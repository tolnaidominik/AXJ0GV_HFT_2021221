using AXJ0GV_HFT_2021221.Data;
using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Repository
{
    public class DogRepository
    {
        DogDbContext context;

        public DogRepository(DogDbContext context)
        {
            this.context = context;
        }
        public void Create(Dog dog)
        {
            context.Dogs.Add(dog);
            context.SaveChanges();
        }
        public void Delete(int dogID)
        {
            context.Dogs.Remove(ReadOne(dogID));
            context.SaveChanges();
        }

        public IQueryable<Dog> ReadAll()
        {
            return context.Dogs;
        }

        public Dog ReadOne(int id)
        {
            return context
                .Dogs
                .FirstOrDefault(c => c.Id == id);
        }

        public void Update(Dog dog)
        {
            Dog old = ReadOne(dog.Id);

            old.Injections = dog.Injections;
            old.Name = dog.Name;
            old.OwnerID = dog.OwnerID;
            old.Sex = dog.Sex;
            old.Species = dog.Species;

            context.SaveChanges();
        }
    }
}
