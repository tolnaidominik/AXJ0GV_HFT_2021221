using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Logic
{
    public interface IDogLogic
    {
        void Create(Dog dog);
        IQueryable<Dog> ReadAll();
        void Update(Dog dog);
        void Delete(int dogId);

        IEnumerable<KeyValuePair<string, int>> GroupByAndCountBySpecies();
    }
}
