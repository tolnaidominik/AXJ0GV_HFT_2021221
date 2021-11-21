using AXJ0GV_HFT_2021221.Models;
using AXJ0GV_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Logic
{
    public class OwnerLogic : IOwnerLogic
    {
        OwnerRepository repo;

        public OwnerLogic(OwnerRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<KeyValuePair<string, int>> CountDogs()
        {
            return repo
               .ReadAll()
               .GroupBy(x => x)
               .Select(x => new KeyValuePair<string, int>(
                   x.Key.Name , x.Key.Dogs.Count));
        }

        public void Create(Owner owner)
        {
            repo.Create(owner);
        }

        public void Delete(int ownerId)
        {
            repo.Delete(ownerId);
        }

        public IEnumerable<KeyValuePair<string, int>> GroupByAndCountByName()
        {
            return repo
               .ReadAll()
               .GroupBy(x => x)
               .Select(x => new KeyValuePair<string, int>(
                   x.Key.Name, x.Key.Name.Count()));
        }

        public IQueryable<Owner> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Owner owner)
        {
            repo.Update(owner);
        }
    }
}
