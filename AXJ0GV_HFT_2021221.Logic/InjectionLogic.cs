using AXJ0GV_HFT_2021221.Models;
using AXJ0GV_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Logic
{
    public class InjectionLogic : IInjectionLogic
    {
        IInjectionRepository repo;

        public InjectionLogic(IInjectionRepository repo)
        {
            this.repo = repo;
        }
        public void Create(Injection injection)
        {
            if (injection.Price == null)
            {
                throw new Exception("Wrong injection type.");
            }
            else
            {
                repo.Create(injection);
            }
            
        }

        public void Delete(int injectionId)
        {
            repo.Delete(injectionId);
        }
        public List<Injection> OrderByPrice()
        {
            return repo
                .ReadAll()
                .OrderBy(x => x.Price)
                .ToList();
        }

        public IQueryable<Injection> ReadAll()
        {
            return repo.ReadAll();
        }

        public int SumPrice()
        {
            return repo
                .ReadAll()
                .Sum(x => x.Price ?? 0);
        }

        public void Update(Injection injection)
        {
            repo.Update(injection);
        }
    }
}
