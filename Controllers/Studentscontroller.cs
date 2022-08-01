using Microsoft.AspNetCore.Mvc;
using StudentApi.Data;
using StudentApi.Models;
using StudentApi.Repository;

namespace StudentApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    public StudentController(
        ILogger<StudentController> logger,
        AppDbContext context,
        StudentRepo repo
    )
    {
        _logger = logger;
        _context = context;
        _repo = repo;
    }
    public static List<Student> students = new List<Student>
    {
        new Student
        {
            Id = Guid.NewGuid(),
            Name = "Fayozjon",
            Grade = "2",
        },
         new Student
        {
            Id = Guid.NewGuid(),
            Name = "Sardor",
            Grade = "2",
        },
    };
    private readonly ILogger<StudentController> _logger;
    private readonly AppDbContext _context;
    private readonly StudentRepo _repo;

    [HttpGet]
    public IActionResult GetStudent()
        => Ok(students);

    [HttpGet("{id}")]
    public IActionResult GetStudent([FromRoute]Guid id)
    {
        var student = students.FirstOrDefault(b => b.Id == id);
        if(student == default) return NotFound("Bunday talaba yo'q");
        return Ok(students);
    }

    [HttpPost]
    public async Task<IActionResult> PostStudentAsync([FromForm] Student studentModel)
    {
        // students.Add(student);
        var student = new Student()
        {
            Id = Guid.NewGuid(),
            Name = studentModel.Name,
            Grade = studentModel.Grade
        };
        await _repo.CreateStudentAsync(student);
        return Created("api/student", student);
    }

    [HttpPut("{id}")]
    public IActionResult PutStudent([FromRoute]Guid id, [FromForm]Student student)
    {
        var old = students.FirstOrDefault(b => b.Id == id);
        if(old == default) return NotFound();
        old.Name = student.Name;
        old.Grade = student.Grade;
        return Accepted();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook([FromRoute]Guid id)
    {
        var student = students.FirstOrDefault(b => b.Id == id);
        if(student == default) return NotFound();
        students.Remove(student);
        return Accepted();
    }
}