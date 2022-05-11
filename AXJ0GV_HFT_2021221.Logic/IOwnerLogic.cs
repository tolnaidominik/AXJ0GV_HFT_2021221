using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Logic
{
    public interface IOwnerLogic
    {
        void Create(Owner owner);
        IQueryable<Owner> ReadAll();
        Owner Read(int id);
        void Update(Owner owner);
        void Delete(int ownerId);
        List<Owner> OrderByIdentityCardNumber();
        IEnumerable<KeyValuePair<string, int>> GroupByAndCountByName();


    }
}
