//using Eparafia.Identity.Application.Actions.Priest;
//using Eparafia.Identity.Application.Actions.User;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Shared.BaseModels.ApiControllerModels;

//namespace Eparafia.Identity.API.Controllers;

//[ApiController]
//[Route("User")]
//[Authorize]
//public class UserController : BaseApiController
//{
//    public UserController(IMediator mediator) : base(mediator)
//    {
//    }

//    [HttpPost]
//    [AllowAnonymous]
//    [Route("Login")]
//    public Task<IActionResult> Login(LoginUser.Query request)
//    {
//        return Endpoint(request);
//    }

//    [HttpPost]
//    [AllowAnonymous]
//    [Route("RefreshToken")]
//    public Task<IActionResult> RefreshToken(RefreshToken.Query request)
//    {
//        return Endpoint(request);
//    }

//    [HttpPost]
//    public Task<IActionResult> RegisterUser(RegisterUser.Command request)
//    {
//        return Endpoint(request);
//    }

//    [HttpPut]
//    public Task<IActionResult> UpdateUser(UpdateUser.Command request)
//    {
//        return Endpoint(request);
//    }

//    [HttpDelete]
//    public Task<IActionResult> DeleteUser(RemoveUser.Command request)
//    {
//        return Endpoint(request);
//    }
//}