using Application.UseCases.Login;
using Application.UseCases.Signin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthVm>> Login([FromQuery] AuthenticationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthVm>> Signin([FromBody] RegisterUserQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
