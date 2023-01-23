using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using domain.Services;
using domain.Models;
using WebProject.Views;
namespace WebProject.Controllers
{
    [ApiController]
    [Route("schedule")]
    public class ScheduleController : ControllerBase
    {

        private readonly ScheduleService _service;

        public ScheduleController(ScheduleService service)
        {
            _service = service;
        }

        [HttpGet("getbydoctor")]
        public ActionResult<List<ScheduleSearchView>> GetByDoctor(DoctorSearchView doc, [FromQuery] DateOnly date)
        {
            var doctor = new Doctor(doc.Id, doc.Fio, doc.Spec);
            var schedRes = _service.GetByDoctor(doctor, date);

            if (!schedRes.Success)
                return Problem(statusCode: 404, detail: schedRes.Error);

            List<ScheduleSearchView> result_arr = new List<ScheduleSearchView>();

            foreach (var schedule in schedRes.Value)
            {
                result_arr.Add(new ScheduleSearchView
                {
                    Id = schedule.Id,
                    DoctorId = schedule.DoctorId,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    Date = schedule.Date,
                });
            }

            return Ok(result_arr);
        }

        [HttpPost("add_schedule")]
        public ActionResult<ScheduleSearchView> Add(ScheduleSearchView schedule)
        {
            var result = new Schedule(schedule.Id, schedule.DoctorId, schedule.StartTime, schedule.EndTime, schedule.Date);
            var schedRes = _service.Add(result);

            if (!schedRes.Success)
                return Problem(statusCode: 404, detail: schedRes.Error);

            return Ok(schedule);
        }

        [HttpPost("update_schedule")]
        public ActionResult<ScheduleSearchView> Update(ScheduleSearchView schedule)
        {
            var result = new Schedule(schedule.Id, schedule.DoctorId, schedule.StartTime, schedule.EndTime, schedule.Date);
            var schedRes = _service.Update(result);

            if (!schedRes.Success)
                return Problem(statusCode: 404, detail: schedRes.Error);

            return Ok(schedule);
        }

        [HttpDelete("delete_schedule")]
        public ActionResult<ScheduleSearchView> Delete(ScheduleSearchView schedule)
        {
            var result = new Schedule(schedule.Id, schedule.DoctorId, schedule.StartTime, schedule.EndTime, schedule.Date); ;
            var schedRes = _service.Delete(result);

            if (!schedRes.Success)
                return Problem(statusCode: 404, detail: schedRes.Error);

            return Ok(schedule);
        }
    }
}