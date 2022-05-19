using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProj
{
    /// <summary>
    /// Класс состояний
    /// </summary>
    public class Condition
    {
        public int conditionKey { get; set; }
        //статус
        public string State { get; set; }

        //Дата техосмотра
        public DateTime Date { get; set; }

        //владение - "личное" или "по найму"
        public string owner { get; set; }
        
        public Condition()
        {
        }
        
        public Condition(string s, DateTime d, string o)
        {
            State = s;
            Date = d;
            owner = o;
        }
    }
}
