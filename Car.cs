using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProj
{
    internal class Car
    {
        public int carKey { get; set; }
        public string Firm { get; set; }    
        public string Model { get; set; }   
        //тип кузова 
        public string Type { get; set; }    
        //расход топлива
        public int Consumption { get; set; }
        public int Cost { get; set; }
        public Car()
        {
        }
        
        public Car(string F, string M, string T, int cons, int cost)
        {
            Firm = F;
            Model = M;
            Type = T;
            Consumption = cons;
            Cost = cost;
        }
    }
}
