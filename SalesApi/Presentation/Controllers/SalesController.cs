using Application.EntityDbContext;
using Application.UseCases.SaleCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class SalesController : BaseController
    {
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Sale([FromBody] SaleQuery query)
        {
            await Mediator.Send(query);

            return NoContent();
        }
    }
}
