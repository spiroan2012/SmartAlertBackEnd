using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        public IIncidentRepository _incidentRepository { get; set; }
        public ICategoriesRepository _categoryRepository { get; set; }
        public ISmsRepository _smsRepository { get; set; }

        public ReportsController(IIncidentRepository incidentRepository, ICategoriesRepository categoryRepository, ISmsRepository smsRepository)
        {
            _incidentRepository = incidentRepository;
            _categoryRepository = categoryRepository;
            _smsRepository = smsRepository;
        }

        //[HttpGet]
        //public ActionResult
    }
}
