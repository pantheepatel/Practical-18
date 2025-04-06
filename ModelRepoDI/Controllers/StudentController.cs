using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ViewModels;
using ViewModels.Models;
using ViewModels.Repository.StudentRepo;

namespace ViewModels.Controllers
{
    public class StudentController(IStudentRepository studentRepository, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var students = await studentRepository.GetAllAsync();
            var studentVMs = mapper.Map<List<StudentViewModel>>(students);
            return View(studentVMs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await studentRepository.GetByIdAsync(id);
            if (student == null) return NotFound();
            var viewModel = mapper.Map<StudentViewModel>(student);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,EnrollmentNo,Email,Course,AdmissionDate")] Student studentData)
        {
            var student = mapper.Map<Student>(studentData);
            await studentRepository.AddAsync(student);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await studentRepository.GetByIdAsync(id);
            if (student == null) return NotFound();
            var viewModel = mapper.Map<StudentViewModel>(student);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EnrollmentNo,Email,Course,AdmissionDate")] Student viewModel)
        {
            if (id != viewModel.Id) return BadRequest();
            if (!ModelState.IsValid) return View(viewModel);

            var student = mapper.Map<Student>(viewModel);
            await studentRepository.UpdateAsync(student);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await studentRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
