using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace ContactPage.Controllers
{
    public class ContactController : Controller
    {
        public readonly EmployeeContext _context;

        public ContactController(EmployeeContext context)
        {
            _context = context;
        }

        //GET: Contact/filter?
        public async Task<IActionResult> Index(string filter)
        {
            if (filter != null && filter.Length > 0)
            {
                return View(await _context.Employees.Where(x => x.FieldOfActivity == filter).ToListAsync());
            }
            else
            {
                return View(await _context.Employees.ToListAsync());
            }
        }

        //GET: Contact/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Employee());
            }
            else
            {
                return View(_context.Employees.Find(id));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("EmployeeId,Name,Surname,MiddleName,Phone,Mail,Company,Description,FieldOfActivity,CompletedProjects")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.EmployeeId == 0)
                {
                    _context.Add(employee);
                }
                else
                {
                    _context.Update(employee);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<FileResult> ExportExcel(string filter)
        {
            string[] col_names = new string[]
            {
                "ФИО",
                "Телефон",
                "Почта",
                "Компания",
                "Сфера деятельности",
                "Описание",
                "Завершенные проекты"
            };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Контакты");

                for (int i = 0; i < col_names.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Style.Font.Size = 18;
                    worksheet.Cells[1, i + 1].Value = col_names[i];
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                    worksheet.Cells[1, i + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(130, 130, 130));
                }

                int row = 2;

                var data = _context.Employees.ToList();
                if (filter != null && filter.Length > 0)
                {
                    data = _context.Employees.Where(x => x.FieldOfActivity == filter).ToList();
                }

                foreach (Employee employee in data)
                {
                    for (int col = 1; col <= col_names.Length; col++)
                    {
                        worksheet.Cells[row, col].Style.Font.Size = 14;
                        worksheet.Cells[row, col].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }

                    worksheet.Cells[row, 1].Value = String.Join(" ", employee.Surname, employee.Name, employee.MiddleName);
                    worksheet.Cells[row, 2].Value = employee.Phone;
                    worksheet.Cells[row, 3].Value = employee.Mail;
                    worksheet.Cells[row, 4].Value = employee.Company;
                    worksheet.Cells[row, 5].Value = employee.FieldOfActivity;
                    worksheet.Cells[row, 6].Value = employee.Description;
                    worksheet.Cells[row, 7].Value = employee.CompletedProjects;

                    if (row % 2 == 0)
                    {
                        for (int col = 1; col <= col_names.Length; col++)
                        {
                            worksheet.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(226, 226, 226));
                        }
                    }
                    row++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                result = package.GetAsByteArray();
            }
            string nameFile = "Контакты_Все_сферы_Дизайн_Досье.xls";
            if (filter != null && filter.Length > 0)
            {
                nameFile = String.Join("_", "Контакты", filter, "Дизайн_Досье.xls");
            }

            return File(result, "application/vnd.ms-excel", nameFile);

        }
    }
}
