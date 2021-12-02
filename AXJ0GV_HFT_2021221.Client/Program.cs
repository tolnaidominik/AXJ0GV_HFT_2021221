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
            System.Threading.Thread.Sleep(8000);

            RestService rest = new RestService("http://localhost:18683");

            var result1 = rest.Get<Dog>("/dog");
        }
    }
}
