using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Repository
{
    interface IOwnerRepository
    {
        void Create(Owner owner);
        Owner ReadOne(int id); // GetById
        IQueryable<Owner> ReadAll(); // Query
        void Update(Owner owner);
        void Delete(int ownerId);
    }
}
