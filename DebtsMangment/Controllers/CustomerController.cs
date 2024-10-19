using AutoMapper;
using DebtsManagement.Core.Entities.DTO.CustomerDTO;
using DebtsMangment.Core.Entities;
using DebtsMangment.Infastructure.Data;
using EC.Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DebtsManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ApiResponse response;

        public CustomerController(AppDbContext dbContext , IMapper mapper, ApiResponse response) {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.response = response;
        }


        [HttpGet]
        public async Task<ActionResult<ApiResponse>>GetAllCustomer()
        {
          
            var result = await dbContext.Customers.ToListAsync();
            var check = result.Any();
            if (check)
            {
                response.IsSuccess = check;
                var mappedCustomer = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTOResponse>>(result);
                return Ok(new ApiResponse(200 , result:mappedCustomer));
            }
                else
                {
                return Problem(statusCode: StatusCodes.Status404NotFound);
            }



        }



        [HttpGet("{id}")]
        public async Task< IActionResult >GetCustomerById(int id)
        {

           
                if (id < 0)
                {

                return Problem(statusCode: StatusCodes.Status400BadRequest);
                }

                var Customer = await dbContext.Customers.FindAsync(id);
                if (Customer == null)
                {
                return Problem(statusCode: StatusCodes.Status404NotFound);
            }
            return Ok(new ApiResponse(200 , result:Customer));
         
        }


        [HttpPost]
        public  async Task<IActionResult >CreateCustomer(CustomerDTORequest model)
        {
            if (model == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound);

            }

            var mapped = mapper.Map<CustomerDTORequest , Customer>(model);
            if (mapped == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound);
            }
            await dbContext.Customers.AddAsync(mapped);
            dbContext.SaveChanges();
            return Ok(new ApiResponse(200));
        }




        [HttpPut ("{id}")]
        public async Task< IActionResult >UpdateCustomer( int id, CustomerDTORequest model)
        {

                if (model == null)
                {
                return Problem(statusCode: StatusCodes.Status404NotFound);
            }

            var founded = await dbContext.Customers.FindAsync(id);

                 if (founded == null)
                    {
                return Problem(statusCode: StatusCodes.Status404NotFound);
            }
            var mapped = mapper.Map(model, founded);

                dbContext.Customers.Update(mapped);
                dbContext.SaveChanges();
                var result = mapper.Map<Customer, CustomerDTORequest>(mapped);

                return Ok(new ApiResponse(200, result: result));
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var model = dbContext.Customers.Find(id);
            if (model == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound);
            }
            dbContext.Customers.Remove(model);
            dbContext.SaveChanges();
            return Ok(new ApiResponse(200, "the Customer deleted successfuly"));
        }


    }
}
