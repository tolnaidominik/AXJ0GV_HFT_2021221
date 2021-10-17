using AXJ0GV_HFT_2021221.Data;
using System;
using System.Linq;

namespace AXJ0GV_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            DogDbContext context = new DogDbContext();
            var res = context.Dogs.ToList();
            ;
        }
    }
}
