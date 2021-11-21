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
        void Update(Owner owner);
        void Delete(int ownerId);

        IEnumerable<KeyValuePair<string, int>> GroupByAndCountByName();

        IEnumerable<KeyValuePair<string, int>> CountDogs();


    }
}
