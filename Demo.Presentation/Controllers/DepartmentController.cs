using Microsoft.AspNetCore.Mvc;
using Demo.BusinessLogic.DTOs;
using Demo.Presentation.ViewModels.DepartmentViewModels;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.BusinessLogic.DTOs.DepartmentDTOs;


namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            //ViewData["Message01"] = " Hi from department's index => ViewData ";
            //ViewBag.Message02 = " Hi from department's index => ViewBag ";

            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }


        #region Create Department

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto createDepartmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int res = _departmentService.CreateDepartment(createDepartmentDto);
                    var message = string.Empty;
                    if (res > 0) 
                        message = "Department created successfully";
                    else
                        message = "Department can't be created";
                    
                    ViewData["SpecialMsg01"] = message;
                    TempData["SpecialMsg02"] = message;

                    return RedirectToAction(nameof(Index)); // back to list 
                }
                catch (Exception ex)
                {
                     // log exception
                     if (_environment.IsDevelopment())
                     {
                         ModelState.AddModelError(string.Empty, ex.Message);
                     }
                    else 
                    {
                        _logger.LogError(ex.Message);

                    }
                }

               
            }
           
            return View(createDepartmentDto);
        }
        #endregion


        #region Department Details

        public IActionResult Details(int? id) 
        {
            if (!id.HasValue) return BadRequest();
            var department=_departmentService.GetDepartmentById(id.Value);

            if (department is null) return NotFound();

            return View(department);
        }

        #endregion


        #region Edit Department

        [HttpGet]
        public IActionResult Edit(int? id) 
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null) return NotFound();
            else
            {
                //mapping from DepartmentDetailsDto to DepartmentEditViewModel
                var departmentViewModel = new DepartmentEditViewModel()
                {
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    DateOfCreation = department.DateOfCreation,
                };
                return View(departmentViewModel);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute]int id , DepartmentEditViewModel departmentEditViewModel) 
        {
            if(!ModelState.IsValid) return View(departmentEditViewModel); // return it with no editing
            try
            {
                // mapping from DepartmentEditViewModel to UpdateDepartmentDto
                var updatedDept = new UpdateDepartmentDto() 
                {
                    Id = id,
                    Code = departmentEditViewModel.Code,
                    Name = departmentEditViewModel.Name,
                    Description = departmentEditViewModel.Description,
                    DateOfCreation = departmentEditViewModel.DateOfCreation
                };
                var res=_departmentService.UpdateDepartment(updatedDept);
                if(res>0) return RedirectToAction(nameof(Index));
                else 
                {
                    ModelState.AddModelError(string.Empty, "Department can't be updated");
                    return View(departmentEditViewModel);
                }
            }
            catch (Exception ex) 
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);

                }
            }
            return View(departmentEditViewModel);
        }
        #endregion


        #region Delete Department

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department=_departmentService.GetDepartmentById(id.Value);

            if (department is null) return NotFound();
            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id==0) return BadRequest();
            try
            {
                var isDeleted=_departmentService.DeleteDepartment(id);
                if (isDeleted) return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError(string.Empty, "Department can't be deleted");
            }
            catch (Exception ex) 
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);

                }
            }

            return RedirectToAction(nameof(Delete) , new { id });
        }

        #endregion
    }
}