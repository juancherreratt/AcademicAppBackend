using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Authorize(Roles = "Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EnrollmentsController(IEnrollmentService enrollmentService, IHttpContextAccessor httpContextAccessor)
        {
            _enrollmentService = enrollmentService;
            _httpContextAccessor = httpContextAccessor;
        }
        private string GetUserId() => _httpContextAccessor.HttpContext!.User.FindFirstValue("userId")!;

        [HttpGet("GetMyEnrollSubjects")]
        public async Task<IActionResult> GetMyEnrollSubjects()
        {
            var response = await _enrollmentService.GetMyEnrollmentsAsync(GetUserId());
            return Ok(response);
        }

        [HttpGet("StudentsBySubject/{subjectId}")]
        public async Task<IActionResult> GetStudentsBySubject(Guid subjectId)
        {
            var students = await _enrollmentService.GetStudentsBySubjectAsync(subjectId);
            return Ok(students);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> EnrollSubjects([FromBody] EnrollmentRequest request)
        {
            var result = await _enrollmentService.EnrollSubjectsAsync(request, GetUserId());
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateEnrollments([FromBody] EnrollmentRequest request)
        {
            var result = await _enrollmentService.UpdateEnrollSubjectsAsync( request, GetUserId());
            return Ok(result);
        }

        [HttpDelete("Cancel")]
        public async Task<IActionResult> CancelEnrollment()
        {
            var result = await _enrollmentService.CancelEnrollmentAsync(GetUserId());
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
