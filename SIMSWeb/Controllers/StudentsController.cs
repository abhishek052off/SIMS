using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SIMSWeb.Business.IService;
using SIMSWeb.Data.IRepository;

namespace SIMSWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentService studentService,
            ITeacherService teacherService, IMapper mapper)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
