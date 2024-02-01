using Infrastructure.EFCore.Controllers;
using Infrastructure.EFCore.Service;
using Microsoft.AspNetCore.Mvc;
using OAuth2Service.DTOs;
using OAuth2Service.Models;

namespace AuthorizeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : SingletonController<Assignment, AssignmentRequest, AssignmentResponse>
    {
        public AssignmentsController(IService<Assignment, AssignmentRequest, AssignmentResponse> service) : base(service)
        {
        }
    }
}
