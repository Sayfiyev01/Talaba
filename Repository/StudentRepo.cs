using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Repository;

public class StudentRepo
{
    private readonly ILogger<StudentRepo> _logger;
    private readonly AppDbContext _context;

    public StudentRepo(
        ILogger<StudentRepo> logger,
        AppDbContext context
    )
    {
        _logger = logger;
        _context = context;
    }

    public async Task<(bool IsSuccess, string? Exception)> CreateStudentAsync(Student student)
    {
        await _context.AddAsync(student);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Student successfully created");
        return (true, string.Empty);
    }
}