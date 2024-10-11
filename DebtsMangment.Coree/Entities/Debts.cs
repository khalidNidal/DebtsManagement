using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtsMangment.Core.Entities
{
    public class Debts
    {
       public int Id { get; set; }
        public int CustomerId { get; set; }
        public string MedicineName { get; set; }
        public double Price { get; set; }
        public DateTime DateAdded { get; set; }



}
}
