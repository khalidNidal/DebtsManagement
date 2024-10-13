using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtsManagement.Core.Entities.DTO.DebtsDTO
{
    public class DebtsDTORequest
    {
        public int CustomerId { get; set; }
        public string MedicineName { get; set; }
        public double Price { get; set; }

    }
}
