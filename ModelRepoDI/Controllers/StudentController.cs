using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Models;
using ViewModels.Repository.StudentRepo;

namespace ViewModels.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController(IStudentRepository studentRepository, IMapper mapper) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await studentRepository.GetAllAsync();
            var studentVMs = mapper.Map<List<StudentViewModel>>(students);
            return Ok(studentVMs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await studentRepository.GetByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(mapper.Map<StudentViewModel>(student));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentProfile studentData)
        {
            var student = mapper.Map<Student>(studentData);
            await studentRepository.AddAsync(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, mapper.Map<StudentViewModel>(student));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateStudentProfile studentData)
        {
            var student = mapper.Map<Student>(studentData);
            student.Id = id;
            await studentRepository.UpdateAsync(student);
            return Ok(new { message = "Student updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await studentRepository.DeleteAsync(id);
            return Ok(new { message = "Student deleted successfully" });
        }
    }
}
