using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using domain.Services;
using domain.Models;
using WebProject.Views;
namespace WebProject.Controllers
{
    [ApiController]
    [Route("recepton")]
    public class ReceptionController : ControllerBase
    {

        private readonly ReceptionService _service;

        public ReceptionController(ReceptionService service)
        {
            _service = service;
        }

        [HttpPost("create_reception")]
        public ActionResult<ReceptionSearchView> CreateReception(ReceptionSearchView reception)
        {
            var result = new Reception(reception.Id, reception.StartTime, reception.EndTime, reception.PatientID, reception.DoctorID);

            var recRes = _service.CreateReception(result);
            if (!recRes.Success)
                return Problem(statusCode: 404, detail: recRes.Error);

            return Ok(reception);
        }


        [HttpGet("getfreebyspec")]
        public ActionResult<IEnumerable<DateTime>> GetFreeBySpec(Specialization specialization, [FromQuery] Schedule schedule)
        {
            var recRes = _service.GetFreeBySpec(specialization,schedule);
            if (recRes.IsFailure)
            {
                return Problem(statusCode: 404, detail: recRes.Error);
            }
            return Ok(recRes.Value);
        }
    }
}