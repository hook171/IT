using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using domain.Services;
using domain.Models;
using WebProject.Views;
namespace WebProject.Controllers
{
    [ApiController]
    [Route("doctor")]
    public class DoctorController : ControllerBase
    {

        private readonly DoctorService _service;

        public DoctorController(DoctorService service)
        {
            _service = service;
        }


        [HttpGet("getalldoctors")]
        public ActionResult<UserSearchView> GetAllDoctors()
        {

            var docRes = _service.GetAllDoctors();
            List<DoctorSearchView> arr_docs = new List<DoctorSearchView>();

            foreach (var doc in docRes.Value)
            {
                var result = new DoctorSearchView
                {
                    Id = doc.Id,
                    Fio = doc.Fio,
                    Spec = doc.Spec,
                };
                arr_docs.Add(result);
            }

            if (docRes.IsFailure)
            {
                return Problem(statusCode: 404, detail: docRes.Error);
            }

            return Ok(arr_docs);
        }

        [HttpPost("create_doctor")]
        public ActionResult CreateDoctor(string fio, Specialization specialization)
        {
            Doctor doctor = new Doctor(0, fio, specialization);
            var docRes = _service.CreateDoctor(doctor);

            if (docRes.IsFailure)
            {
                return Problem(statusCode: 404, detail: docRes.Error);
            }
            return Ok(docRes.Value);
        }

        [HttpGet("getbyiddoctor")]
        public ActionResult FindDoctorById(int id)
        {
            var docRes = _service.FindDoctorById(id);

            if (docRes.IsFailure)
            {
                return Problem(statusCode: 404, detail: docRes.Error);
            }
            return Ok(docRes.Value);
        }

        [HttpGet("getbyspecdoctor")]
        public ActionResult<List<DoctorSearchView>> GetBySpec(Specialization specialization)
        {
            var docRes = _service.GetBySpec(specialization);
            List<DoctorSearchView> arr_docs = new List<DoctorSearchView>();

            foreach (var doc in docRes.Value)
            {
                var result = new DoctorSearchView
                {
                    Id = doc.Id,
                    Fio = doc.Fio,
                    Spec = doc.Spec,
                };
                arr_docs.Add(result);
            }

            if (!docRes.Success)
                return Problem(statusCode: 404, detail: docRes.Error);

            return Ok(arr_docs);
        }

        [HttpDelete("delete_doctor")]
        public ActionResult DeleteDoctor(int id)
        {
            var docRes = _service.DeleteDoctor(id);

            if (docRes.IsFailure)
            {
                return Problem(statusCode: 404, detail: docRes.Error);
            }
            return Ok();
        }


    }
}