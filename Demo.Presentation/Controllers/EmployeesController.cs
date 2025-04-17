using Demo.BusinessLogic.DTOs.EmployeeDTOs;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Models.SharedModels;
using Demo.Presentation.ViewModels.EmployeeViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService ,
        ILogger<EmployeesController> _logger,
        IWebHostEnvironment _environment ) : Controller
    {
        public IActionResult Index()
        {
            var employees=_employeeService.GetAllEmployees();
            return View(employees);
        }

        #region Create Employee

        [HttpGet]
        public IActionResult Create([FromServices]IDepartmentService _departmentService) //injection to use the department service in this action
        {
            ViewData["Departments"]= _departmentService.GetAllDepartments(); // to pass the department's data (used in CreateEditPartialView)
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int res = _employeeService.CreateEmployee(createEmployeeDto);
                    if (res > 0) return RedirectToAction(nameof(Index)); // back to list 
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee can't be created");
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

            return View(createEmployeeDto);
        }
        #endregion


        #region Employee Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);

            return employee is null ?  NotFound() : View(employee);
        }

        #endregion


        #region Edit Employee

        [HttpGet]
        public IActionResult Edit(int? id , [FromServices] IDepartmentService _departmentService)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null) return NotFound();
            else
            {
                //mapping from EmployeeDetailsDto to EmployeeEditViewModel
                var employeeViewModel = new EmployeeEditViewModel()
                {
                    Name = employee.Name,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    PhoneNumber = employee.PhoneNumber,
                    EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                    Gender = Enum.Parse<Gender>(employee.Gender),
                    Email = employee.Email,
                    Age=(int)employee.Age,
                    HiringDate = employee.HiringDate,
                    Address=employee.Address,
                    DepartmentId = employee.DepartmentId,
                };

                ViewData["Departments"] = _departmentService.GetAllDepartments();
                return View(employeeViewModel);
            }
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeEditViewModel employeeEditViewModel)
        {
            if (!ModelState.IsValid) return View(employeeEditViewModel); // return it with no editing
            try
            {
                // mapping from EmployeeEditViewModel to UpdateEmployeeDto
                var updatedEmp = new UpdateEmployeeDto()
                {
                    Id=id,
                    Name= employeeEditViewModel.Name,
                    Age= employeeEditViewModel.Age,
                    Salary= employeeEditViewModel.Salary,
                    IsActive= employeeEditViewModel.IsActive,
                    PhoneNumber= employeeEditViewModel.PhoneNumber,
                    Email= employeeEditViewModel.Email,
                    Address= employeeEditViewModel.Address,
                    EmployeeType=employeeEditViewModel.EmployeeType,
                    Gender= employeeEditViewModel.Gender,
                    DepartmentId= employeeEditViewModel.DepartmentId,
                };
                var res = _employeeService.UpdateEmployee(updatedEmp);

                if (res > 0) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee can't be updated");
                    return View(employeeEditViewModel);
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
            return View(employeeEditViewModel);
        }
        #endregion



        #region Delete Employee

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var isDeleted = _employeeService.DeleteEmployee(id);
                if (isDeleted) return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError(string.Empty, "Employee can't be deleted");
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

            return RedirectToAction(nameof(Delete), new { id });
        }

        #endregion

    }
}
