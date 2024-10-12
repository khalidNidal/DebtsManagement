using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtsManagement.Core.Entities.DTO.CustomerDTO
{
    public class CustomerDTOResponse
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
    }
}
