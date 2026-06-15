using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.Controllers.Members;

[ApiController]
[Route("api/[controller]")]
public class MembersController(
    ISender mediator) 
    : ControllerBase
{
    
}