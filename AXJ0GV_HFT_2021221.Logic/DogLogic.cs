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
            repo.Create(dog);
        }

        public void Delete(int dogId)
        {
            repo.Delete(dogId);
        }

        public IEnumerable<KeyValuePair<string, int>> GroupByAndCountBySpecies()
        {
            return repo
                .ReadAll()
                .GroupBy(x => x.Species)
                .Select(x => new KeyValuePair<string, int>(
                    x.Key, x.Key.Count()));
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
