using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace ContactPage.Controllers
{
    public class EquipmentController : Controller
    {
        public readonly EmployeeContext _context;

        public EquipmentController(EmployeeContext context)
        {
            _context = context;
        }

        //GET: Contact/filter?
        public async Task<IActionResult> Index(string filter, string number)
        {
                if (filter != null && filter.Length > 0)
                {
                    if(number == "0")
                    {
                        return View(await _context.Items.Where(x => x.TypeItem.ToString() == filter).ToListAsync());
                    }
                    else if(number == "1")
                    {
                        return View(await _context.Items.Where(x => x.AssignedEmployee.ToString() == filter).ToListAsync());
                    }
                    else
                    {
                        return View(await _context.Items.ToListAsync());
                    }
                }
                else
                {
                    return View(await _context.Items.ToListAsync());
                }
        }

        //GET: Item/Add
        public IActionResult Add()
        {
            Guid newGuid = Guid.NewGuid();
            return View(new Item() { GuidId = newGuid });
        }
        //POST: Item/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("ItemId,GuidId,TypeItem,Name,Company,Serial,NameDescriptionInfo,DescriptionInfo,NameDescriptionProgramm,DescriptionProgramm,NameDescriptionLicense,DescriptionLicense,AssignedEmployee,WhereIs")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (item.ItemId == 0)
                {
                    _context.Add(item);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(item);
        }

        //POST: Update item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ItemId,GuidId,TypeItem,Name,Company,Serial,NameDescriptionInfo,DescriptionInfo,NameDescriptionProgramm,DescriptionProgramm,NameDescriptionLicense,DescriptionLicense,AssignedEmployee,WhereIs")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (item.ItemId != 0)
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ItemView), new { id = item.ItemId });
                }
            }
            return View(item);
        }

        //GET: Item/id=1
        public IActionResult Edit(int id)
        {
            return View(_context.Items.Find(id));
        }
        //GET: Item/id=1
        public IActionResult ItemView(int id)
        {
            return View(_context.Items.Find(id));
        }
        

        public async Task<IActionResult> Delete(int? id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<FileResult> ExportExcel(string filter)
        {
            string[] col_names = new string[]
            {
                "№",
                "Внутрений номер",
                "Тип оборудования",
                "Наименование",
                "Компания",
                "Серия",
                "Закрепленный сотрудник",
                "Где находится"
            };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Оборудование");

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

                var data = _context.Items.ToList();
                if (filter != null && filter.Length > 0)
                {
                    data = _context.Items.Where(x => x.TypeItem.ToString() == filter).ToList();
                }

                foreach (Item item in data)
                {
                    for (int col = 1; col <= col_names.Length; col++)
                    {
                        worksheet.Cells[row, col].Style.Font.Size = 14;
                        worksheet.Cells[row, col].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }

                    worksheet.Cells[row, 1].Value = row - 1;
                    worksheet.Cells[row, 2].Value = item.GuidId;
                    worksheet.Cells[row, 3].Value = item.TypeItem;
                    worksheet.Cells[row, 4].Value = item.Name;
                    worksheet.Cells[row, 5].Value = item.Company;
                    worksheet.Cells[row, 6].Value = item.Serial;
                    worksheet.Cells[row, 7].Value = item.AssignedEmployee;
                    worksheet.Cells[row, 8].Value = item.WhereIs;

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
            string nameFile = "Оборудование_Все_типы_Дизайн_Досье.xls";
            if (filter != null && filter.Length > 0)
            {
                nameFile = String.Join("_", "Оборудование", filter, "Дизайн_Досье.xls");
            }

            return File(result, "application/vnd.ms-excel", nameFile);
        }

        [HttpGet]
        public async Task<FileResult> GetImage(int id)
        {
            Item item = _context.Items.Find(id);
            string textForPrint = String.Join("\n", item.Name, item.Company + " - " + item.Serial, item.TypeItem + " - \\ " + item.GuidId + " \\");
            Font font = new Font(FontFamily.GenericSansSerif, 14);
            string nameFile = String.Join(" ", item.Name, item.GuidId + ".png");

            Bitmap objBmpImage = new Bitmap(1, 1);

            int intWidth = 0;
            int intHeight = 0;

            // Create the Font object for the image text drawing.
            Font objFont = new Font("Arial", 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

            // Create a graphics object to measure the text's width and height.
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            // This is where the bitmap size is determined.
            intWidth = (int)objGraphics.MeasureString(textForPrint, objFont).Width;
            intHeight = (int)objGraphics.MeasureString(textForPrint, objFont).Height;

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);

            // Set Background color
            objGraphics.Clear(Color.White);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(textForPrint, objFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 0, 0);
            objGraphics.Flush();
            //return (objBmpImage);

            byte[] imgByte = await ImageToByteArray(objBmpImage);
            
            return File(imgByte, "image/png", nameFile);
        }

        public async Task<byte[]> ImageToByteArray(Bitmap imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
