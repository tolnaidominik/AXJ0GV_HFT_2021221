using AXJ0GV_HFT_2021221.Models;
using AXJ0GV_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Logic
{
    class DogLogic : IDogLogic
    {
        DogRepository repo;

        public DogLogic(DogRepository repo)
        {
            this.repo = repo;
        }

        public void Create(Dog dog)
        {
            repo.Create(dog);
        }

        public void Delete(int dogId)
        {
            repo.Delete(dogId);
        }

        public void GroupByAndCountBySpecies()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Dog> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Dog dog)
        {
            repo.Update(dog);
        }
    }
}
