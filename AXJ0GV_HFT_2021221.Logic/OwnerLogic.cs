using AXJ0GV_HFT_2021221.Models;
using AXJ0GV_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Logic
{
    class OwnerLogic : IOwnerLogic
    {
        OwnerRepository repo;

        public OwnerLogic(OwnerRepository repo)
        {
            this.repo = repo;
        }

        public void CountAndOrderByDogs()
        {
            throw new NotImplementedException();
        }

        public void Create(Owner owner)
        {
            repo.Create(owner);
        }

        public void Delete(int ownerId)
        {
            repo.Delete(ownerId);
        }

        public void GroupByAndCountByName()
        {
            throw new NotImplementedException();
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
