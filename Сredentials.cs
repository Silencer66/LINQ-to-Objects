using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProj
{
    //класс Учета данных 
    public class Credentials
    {
        public int creKey { get; set; } 
        public string Surname { get; set; }
        //Номерной знак
        public string Number { get; set; }  
        public string Address { get; set; }
        public bool IsGarage { get; set; }
        //год поставления на учет 
        public int YearRegistration { get; set; }
        //пробег машины
        public int Mileage { get; set; }
        public Credentials()
        {

        }
    }
}
