using AXJ0GV_HFT_2021221.Data;
using AXJ0GV_HFT_2021221.Models;
using System;
using System.Linq;

namespace AXJ0GV_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RestService rest = new RestService("http://localhost:18683");

            var result1 = rest.Get<Dog>("/dog");

            var result2 = rest.Get<Owner>("/owner");

            var result3 = rest.Get<Injection>("/injection");
        }
    }
}
