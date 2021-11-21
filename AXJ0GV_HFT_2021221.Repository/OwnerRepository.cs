using AXJ0GV_HFT_2021221.Data;
using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        DogDbContext context;

        public OwnerRepository(DogDbContext context)
        {
            this.context = context;
        }

        public void Create(Owner owner)
        {
            context.Owners.Add(owner);
            context.SaveChanges();
        }
        public void Delete(int objid)
        {
            context.Owners.Remove(ReadOne(objid));

            context.SaveChanges();
        }

        public IQueryable<Owner> ReadAll()
        {
            return context.Owners;
        }

        public Owner ReadOne(int id)
        {
            return context
                .Owners
                .FirstOrDefault(c => c.Id == id);
        }

        public void Update(Owner owner)
        {
            Owner old = ReadOne(owner.Id);

            old.Name = owner.Name;
            old.IdentityCardNumber = owner.IdentityCardNumber;
            old.Sex = owner.Sex;
            old.Dogs = owner.Dogs;
            
            context.SaveChanges();
        }
    }
}
