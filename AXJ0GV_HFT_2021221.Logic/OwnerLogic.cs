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
        IOwnerRepository repo;

        public OwnerLogic(IOwnerRepository repo)
        {
            this.repo = repo;
        }



        //CRUD
        public void Create(Owner owner)
        {
            if(owner.Name == null)
            {
                throw new Exception("Wrong owner type.");
            }
            else
            {
                repo.Create(owner);
            }
        }
        public IQueryable<Owner> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Owner owner)
        {
            repo.Update(owner);
        }
        public void Delete(int ownerId)
        {
            repo.Delete(ownerId);
        }






        //NONCRUD
        public List<Owner> OrderByIdentityCardNumber()
        {
            return repo
               .ReadAll()
               .OrderBy(x => x.IdentityCardNumber)
               .ToList();
        }
        public IEnumerable<KeyValuePair<string, int>> GroupByAndCountByName()
        {
            return repo
               .ReadAll()
               .GroupBy(x => x)
               .Select(x => new KeyValuePair<string, int>(
                   x.Key.Name, x.Key.Name.Count()));
        }
    }
}
