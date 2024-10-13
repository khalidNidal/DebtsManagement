using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtsMangment.Core.Entities
{
    public class Customer
    {
            
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public ICollection<Debts> Debts { get; set; } = new HashSet<Debts>();   


    }
}
