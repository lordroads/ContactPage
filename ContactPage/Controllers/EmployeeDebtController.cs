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
    public class EmployeeDebtController : Controller
    {
        public readonly EmployeeContext _context;

        public EmployeeDebtController(EmployeeContext context)
        {
            _context = context;
        }

        //GET: Contact/
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeesDebts.ToListAsync());
        }

        //GET: Contact/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new EmployeeDebt());
            }
            else
            {
                return View(_context.EmployeesDebts.Find(id));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("EmployeeDebtId,Name,Surname,MiddleName,Phone,Equipment,Description")] EmployeeDebt employeeDebt)
        {
            if (ModelState.IsValid)
            {
                if (employeeDebt.EmployeeDebtId == 0)
                {
                    _context.Add(employeeDebt);
                }
                else
                {
                    _context.Update(employeeDebt);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDebt);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var employeeDebt = await _context.EmployeesDebts.FindAsync(id);
            _context.EmployeesDebts.Remove(employeeDebt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<FileResult> ExportExcel()
        {
            string[] col_names = new string[]
            {
                "ФИО",
                "Телефон",
                "Оборудование",
                "Описание"
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

                foreach (EmployeeDebt employeeDebt in _context.EmployeesDebts.ToList())
                {
                    for (int col = 1; col <= col_names.Length; col++)
                    {
                        worksheet.Cells[row, col].Style.Font.Size = 14;
                        worksheet.Cells[row, col].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }

                    worksheet.Cells[row, 1].Value = String.Join(" ", employeeDebt.Surname, employeeDebt.Name, employeeDebt.MiddleName);
                    worksheet.Cells[row, 2].Value = employeeDebt.Phone;
                    worksheet.Cells[row, 3].Value = employeeDebt.Equipment;
                    worksheet.Cells[row, 4].Value = employeeDebt.Description;

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
            string nameFile = "Контакты_сотрудников_у_кого оборудование_Дизайн_Досье.xls";

            return File(result, "application/vnd.ms-excel", nameFile);

        }
    }
}
