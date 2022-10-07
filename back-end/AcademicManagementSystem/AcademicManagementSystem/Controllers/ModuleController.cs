using AcademicManagementSystem.Context;
using AcademicManagementSystem.Context.AmsModels;
using AcademicManagementSystem.Models.AddressController.DistrictModel;
using AcademicManagementSystem.Models.AddressController.ProvinceModel;
using AcademicManagementSystem.Models.AddressController.WardModel;
using AcademicManagementSystem.Models.CenterController;
using AcademicManagementSystem.Models.CourseModuleSemester;
using AcademicManagementSystem.Models.ModuleController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagementSystem.Controllers;

[ApiController]
public class ModuleController : ControllerBase
{
    private readonly AmsContext _context;

    public ModuleController(AmsContext context)
    {
        _context = context;
    }

    // get all modules
    [HttpGet]
    [Route("api/modules")]
    public IActionResult GetModules()
    {
        var modules = _context.Modules.Include(m => m.Center)
            .Include(m => m.Center.Province)
            .Include(m => m.Center.District)
            .Include(m => m.Center.Ward)
            .Include(m => m.CoursesModulesSemesters)
            .ToList()
            .Select(m => new ModuleResponse()
            {
                Id = m.Id, CenterId = m.CenterId, SemesterNamePortal = m.SemesterNamePortal, ModuleName = m.ModuleName,
                ModuleExamNamePortal = m.ModuleExamNamePortal, ModuleType = m.ModuleType,
                MaxTheoryGrade = m.MaxTheoryGrade, MaxPracticalGrade = m.MaxPracticalGrade, Hours = m.Hours,
                Days = m.Days, ExamType = m.ExamType, CreatedAt = m.CreatedAt, UpdatedAt = m.UpdatedAt,
                Center = new CenterResponse()
                {
                    Id = m.Center.Id,
                    Name = m.Center.Name, CreatedAt = m.Center.CreatedAt, UpdatedAt = m.Center.UpdatedAt,
                    Province = new ProvinceResponse()
                    {
                        Id = m.Center.Province.Id,
                        Name = m.Center.Province.Name,
                        Code = m.Center.Province.Code
                    },
                    District = new DistrictResponse()
                    {
                        Id = m.Center.District.Id,
                        Name = m.Center.District.Name,
                        Prefix = m.Center.District.Prefix,
                    },
                    Ward = new WardResponse()
                    {
                        Id = m.Center.Ward.Id,
                        Name = m.Center.Ward.Name,
                        Prefix = m.Center.Ward.Prefix,
                    }
                },
                CoursesModulesSemesters = m.CoursesModulesSemesters.Select(cms => new CourseModuleSemesterResponse()
                {
                    CourseCode = cms.CourseCode,
                    ModuleId = cms.ModuleId,
                    SemesterId = cms.SemesterId,
                }).ToList()
            });
        return Ok(CustomResponse.Ok("Modules retrieved successfully", modules));
    }

    // get module by id
    [HttpGet]
    [Route("api/modules/{id}")]
    public IActionResult GetModuleById(int id)
    {
        var module = _context.Modules.Include(m => m.Center)
            .Include(m => m.Center.Province)
            .Include(m => m.Center.District)
            .Include(m => m.Center.Ward)
            .Include(m => m.CoursesModulesSemesters)
            .FirstOrDefault(m => m.Id == id);
        if (module == null)
        {
            return NotFound(CustomResponse.NotFound("Module not found"));
        }

        var moduleResponse = GetModuleResponse(module);
        return Ok(CustomResponse.Ok("Module retrieved successfully", moduleResponse));
    }

    // create module with course code and semester id
    [HttpPost]
    [Route("api/modules")]
    public IActionResult CreateModule([FromBody] CreateModuleRequest request)
    {
        request.CourseCode = request.CourseCode?.ToUpper().Trim();
        request.ModuleName = request.ModuleName?.Trim();
        request.ModuleExamNamePortal = request.ModuleExamNamePortal?.Trim();
        request.ModuleType = request.ModuleType?.Trim();
        request.ExamType = request.ExamType?.Trim();
        request.SemesterNamePortal = request.SemesterNamePortal?.Trim();

        var module = new Module()
        {
            CenterId = request.CenterId,
            SemesterNamePortal = request.SemesterNamePortal,
            ModuleName = request.ModuleName,
            ModuleExamNamePortal = request.ModuleExamNamePortal,
            ModuleType = request.ModuleType,
            MaxTheoryGrade = request.MaxTheoryGrade,
            MaxPracticalGrade = request.MaxPracticalGrade,
            Hours = request.Hours,
            Days = request.Days,
            ExamType = request.ExamType,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        var courseCode = request.CourseCode;
        var semesterId = request.SemesterId;

        _context.Modules.Add(module);
        _context.SaveChanges();
        var courseModuleSemester = new CourseModuleSemester()
        {
            CourseCode = courseCode,
            ModuleId = module.Id,
            SemesterId = semesterId
        };

        _context.CoursesModulesSemesters.Add(courseModuleSemester);
        _context.SaveChanges();
        var newModule = _context.Modules.Include(m => m.Center)
            .Include(m => m.Center.Province)
            .Include(m => m.Center.District)
            .Include(m => m.Center.Ward)
            .Include(m => m.CoursesModulesSemesters)
            .FirstOrDefault(m => m.Id == module.Id);
        if (newModule == null)
        {
            return NotFound(CustomResponse.NotFound("Module not found"));
        }
        var moduleResponse = GetModuleResponse(newModule);
        return Ok(CustomResponse.Ok("Module created successfully", moduleResponse));
    }

    private static ModuleResponse GetModuleResponse(Module module)
    {
        var moduleResponse = new ModuleResponse()
        {
            Id = module.Id, CenterId = module.CenterId, SemesterNamePortal = module.SemesterNamePortal,
            ModuleName = module.ModuleName,
            ModuleExamNamePortal = module.ModuleExamNamePortal, ModuleType = module.ModuleType,
            MaxTheoryGrade = module.MaxTheoryGrade, MaxPracticalGrade = module.MaxPracticalGrade, Hours = module.Hours,
            Days = module.Days, ExamType = module.ExamType, CreatedAt = module.CreatedAt, UpdatedAt = module.UpdatedAt,
            Center = new CenterResponse()
            {
                Id = module.Center.Id,
                Name = module.Center.Name, CreatedAt = module.Center.CreatedAt, UpdatedAt = module.Center.UpdatedAt,
                Province = new ProvinceResponse()
                {
                    Id = module.Center.Province.Id,
                    Name = module.Center.Province.Name,
                    Code = module.Center.Province.Code
                },
                District = new DistrictResponse()
                {
                    Id = module.Center.District.Id,
                    Name = module.Center.District.Name,
                    Prefix = module.Center.District.Prefix,
                },
                Ward = new WardResponse()
                {
                    Id = module.Center.Ward.Id,
                    Name = module.Center.Ward.Name,
                    Prefix = module.Center.Ward.Prefix,
                }
            },
            CoursesModulesSemesters = module.CoursesModulesSemesters.Select(cms => new CourseModuleSemesterResponse()
            {
                CourseCode = cms.CourseCode,
                ModuleId = cms.ModuleId,
                SemesterId = cms.SemesterId,
            }).ToList()
        };
        return moduleResponse;
    }
}