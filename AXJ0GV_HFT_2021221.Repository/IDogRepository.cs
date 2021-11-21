using AXJ0GV_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Repository
{
    public interface IDogRepository
    {
        void Create(Dog dog);
        Dog ReadOne(int id); // GetById
        IQueryable<Dog> ReadAll(); // Query
        void Update(Dog car);
        void Delete(int carId);
    }
}
