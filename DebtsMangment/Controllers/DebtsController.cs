using AutoMapper;
using DebtsManagement.Core.Entities.DTO.CustomerDTO;
using DebtsManagement.Core.Entities.DTO.DebtsDTO;
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
    public class DebtsController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public DebtsController(AppDbContext dbContext , IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        [HttpGet]
        public  async Task< ActionResult<ApiResponse>> GetAllDebts() {

            var model = await dbContext.Debts.Include(x => x.Customer).ToListAsync();

                if (!model.Any())
                {
                    return NotFound(new ApiValidationResponse(400, new List<string> { "no debts found" }));
                }
                var mappedCustomer = mapper.Map<IEnumerable<Debts>, IEnumerable<DebtsDTOResponse>>(model);

                return Ok(new ApiResponse(200, result: mappedCustomer));

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddNewDebt(DebtsDTORequest debts)
        {
            if (debts == null)
            {
                return NotFound(new ApiValidationResponse(400, new List<string> { "the debts didnt found" }));
            }

            var mapped = mapper.Map<DebtsDTORequest, Debts>(debts);


            await dbContext.Debts.AddAsync(mapped);
            dbContext.SaveChanges();
            return Ok(new ApiResponse(200));
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<ApiResponse>> EditDebt (int id, DebtsDTORequest model)
        {
            if (model == null)
            {
                return NotFound(new ApiValidationResponse(404, new List<string> { "the  model is empty" }));
            }

            var founded = await dbContext.Debts.FindAsync(id);

            if (founded == null)
            {
                return NotFound(new ApiValidationResponse(404, new List<string> { "the  founded model didnt found" }));
            }
            var mapped = mapper.Map(model, founded);

            dbContext.Debts.Update(mapped);
            dbContext.SaveChanges();
            var result = mapper.Map<Debts, DebtsDTORequest>(mapped);

            return Ok(new ApiResponse(200, result: result));
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteDebt(int id)
        {
            var model = dbContext.Debts.Find(id);
            if (model == null)
            {
                return NotFound(new ApiValidationResponse(400, new List<string> { "Not Found" }));
            }
            dbContext.Debts.Remove(model);
            dbContext.SaveChanges();
            return Ok(new ApiResponse(200, "the product deleted successfuly"));
        }

        [HttpGet ("CustomerDebts/{customerId}")]
        public async Task< ActionResult<ApiResponse>>GetDebtByCustomerId(int customerId)
        {
            if (customerId <= 0)
            {
                return BadRequest(new ApiValidationResponse(400, new List<string> { "invalid id number" }));
            }

            var debts = await dbContext.Debts.Include(x => x.Customer).
                Where(x => x.CustomerId == customerId).ToListAsync();

            var mapped = mapper.Map<IEnumerable<Debts>, IEnumerable<DebtsDTOResponse>>(debts);

            return Ok(new ApiResponse(200, result: mapped));

        }






    }
}
