
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Models;

namespace Controllers
{
    public class CustomersController : ODataController
    {
        private AppDbContext _dbContext;

        public CustomersController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_dbContext.Customer);
        }

        public IActionResult Get(int key)
        {
            return Ok(_dbContext.Customer.Where(w => w.Id == key));
        }

        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (null==customer) {
                return BadRequest("Query is null");
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // TODO: check permission
            _dbContext.Customer.Add(customer);
            await _dbContext.SaveChangesAsync();
            return Ok(customer);
        }

        public async Task<IActionResult> Patch(int key, [FromBody] Delta<Customer> delta)
        {
            // TODO: check permission
            var Query = await _dbContext.Customer.FindAsync(key);
            delta.Patch(Query);
            _dbContext.Customer.Update(Query);
            await _dbContext.SaveChangesAsync();
            return Ok(Query);
        }

        public async Task<IActionResult> Delete(int key)
        {
            /*
            var Customer = await _dbContext.Customer.FindAsync(key);
            _dbContext.Customer.Remove(Query);
            await _dbContext.SaveChangesAsync();
            return Ok(Customer);
            */
            return BadRequest("Not implemented");
        }


    }
}