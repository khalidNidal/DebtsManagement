 using DebtsMangment.Core.Entities;
using DebtsMangment.Infastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DebtsManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public CustomerController(AppDbContext dbContext) {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            var result = dbContext.Customers.ToList();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var Customer = dbContext.Customers.Find(id);
            if (Customer == null)
            {
                return NotFound();
            }
            return Ok(Customer);


        }


        [HttpPost]
        public IActionResult CreateCustomer(Customer model)
        {
            dbContext.Customers.Add(model);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetCustomerById), new { id = model.Id }, model);

        }


        [HttpPut]
        public IActionResult UpdateCustomer(int id)
        {
            var model = dbContext.Customers.Find(id);
            if (model == null)
            {
                return BadRequest();
            }

            dbContext.Customers.Update(model);
            dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var model = dbContext.Customers.Find(id);
            if (model == null)
            {
                return BadRequest();
            }
            dbContext.Customers.Remove(model);
            dbContext.SaveChanges();
            return Ok();
        }


    }
}
