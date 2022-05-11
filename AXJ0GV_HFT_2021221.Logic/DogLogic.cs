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


        //CRUD
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
        public IQueryable<Dog> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Dog dog)
        {
            repo.Update(dog);
        }
        public void Delete(int dogId)
        {
            repo.Delete(dogId);
        }





        //NONCRUD
        public int CountByOwner(int ownerID)
        {
            return repo
                .ReadAll()
                .Where(x => x.OwnerID == ownerID)
                .Count();
        }
        public int CountByInjection(int injectionID)
        {
            return repo
                .ReadAll()
                .Where(x => x.InjectionID == injectionID)
                .Count();
        }
        public List<Injection> GetUsedInjections()
        {
            return repo
                .ReadAll()
                .Where(x => x.InjectionID > 0)
                .Select(x => x.Injection)
                .ToList();
        }

        public Dog Read(int id)
        {
            return repo.ReadOne(id);
        }
    }
}
