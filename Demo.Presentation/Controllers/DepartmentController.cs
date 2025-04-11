using Microsoft.AspNetCore.Mvc;
using Demo.BusinessLogic.Services;
using Demo.BusinessLogic.DTOs;
using Demo.Presentation.ViewModels.DepartmentViewModels;


namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
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
                    if (res > 0) return RedirectToAction(nameof(Index)); // back to list 
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department can't be created");
                    }
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


        #region MyRegion

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
    }
}