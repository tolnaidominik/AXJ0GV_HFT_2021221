using AXJ0GV_HFT_2021221.Models;
using AXJ0GV_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Logic
{
    public class DogLogic : IDogLogic
    {
        IDogRepository repo;

        public DogLogic(IDogRepository repo)
        {
            this.repo = repo;
        }

        public void Create(Dog dog)
        {
            if (dog.Species == null)
            {
                throw new Exception("Wrong Dog type.");
            }
            else
            {
                repo.Create(dog);
            }
        }

        public void Delete(int dogId)
        {
            repo.Delete(dogId);
        }

        public int CountByOwner(Owner owner)
        {
            return repo
                .ReadAll()
                .Where(x => x.OwnerID == owner.Id)
                .Count()
                ;
        }
        public int CountByInjection(Injection injection)
        {
            return repo
                .ReadAll()
                .Where(x => x.InjectionID == injection.Id)
                .Count()
                ;
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
