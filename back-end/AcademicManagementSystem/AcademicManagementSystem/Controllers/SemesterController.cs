using AcademicManagementSystem.Context;
using AcademicManagementSystem.Models.CourseModuleSemester;
using AcademicManagementSystem.Models.SemesterController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagementSystem.Controllers;

[ApiController]
public class SemesterController : ControllerBase
{
    private readonly AmsContext _context;
    
    public SemesterController(AmsContext context)
    {
        _context = context;
    }
    
    // get all semesters
    [HttpGet]
    [Route("api/semesters")]
    public IActionResult GetSemesters()
    {
        var semesters = _context.Semesters
            .Include(s => s.CoursesModuleSemesters)
            .ToList()
            .Select(s => new SemesterResponse()
            {
                Id = s.Id, Name = s.Name,
                CoursesModulesSemesters = s.CoursesModuleSemesters.Select(cms => new CourseModuleSemesterResponse()
                {
                    CourseCode = cms.CourseCode, ModuleId = cms.ModuleId, SemesterId = cms.SemesterId
                }).ToList()
            });
        return Ok(semesters);
    }
    
    // get a semester by id
    [HttpGet]
    [Route("api/semesters/{id}")]
    public IActionResult GetSemester(int id)
    {
        var semester = _context.Semesters
            .Include(s => s.CoursesModuleSemesters)
            .FirstOrDefault(s => s.Id == id);
        if (semester == null)
        {
            return NotFound();
        }
        var semesterResponse = new SemesterResponse()
        {
            Id = semester.Id, Name = semester.Name,
            CoursesModulesSemesters = semester.CoursesModuleSemesters.Select(cms => new CourseModuleSemesterResponse()
            {
                CourseCode = cms.CourseCode, ModuleId = cms.ModuleId, SemesterId = cms.SemesterId
            }).ToList()
        };
        return Ok(semesterResponse);
    }
}