using DebtsMangment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtsManagement.Core.Entities.DTO.DebtsDTO
{
    public class DebtsDTOResponse
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public double Price { get; set; }
        public DateTime DateAdded { get; set; }
        public string CustomerName { get; set; }


    }
}
