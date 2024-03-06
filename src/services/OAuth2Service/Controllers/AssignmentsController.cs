namespace OAuth2Service.Controllers
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
