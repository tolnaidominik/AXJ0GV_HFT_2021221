using AXJ0GV_HFT_2021221.Models;
using AXJ0GV_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Logic
{
    class InjectionLogic : IInjectionLogic
    {
        InjectionRepository repo;

        public InjectionLogic(InjectionRepository repo)
        {
            this.repo = repo;
        }

        public void Create(Injection injection)
        {
            repo.Create(injection);
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

        public void Update(Injection injection)
        {
            repo.Update(injection);
        }
    }
}
