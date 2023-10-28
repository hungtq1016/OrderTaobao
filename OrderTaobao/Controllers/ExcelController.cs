using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Table;
using OfficeOpenXml;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExcelController : ControllerBase
    {
        private readonly IUserService _userService;

        public ExcelController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("export-user")]
        [AllowAnonymous]
        public async Task<IActionResult> ExportUser()
        {
            string reportname = $"User_Sheet";
            var list = await _userService.GetAll();
            if (list.Count > 0)
            {
                var exportbytes = ExportToExcel<UserResponse>(list, reportname);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
            }
            return NotFound(list);
        }

        private byte[] ExportToExcel<T>(List<T> table, string filename)
        {
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1);
            return pack.GetAsByteArray();
        }


        [HttpPost("import-user")]
        [AllowAnonymous]
        public async Task<IActionResult> UserImport(IFormFile file)
        {
            var result = await WriteFile(file);
            return Ok(result);
        }
        private async Task<List<UserRequest>> WriteFile(IFormFile file)
        {
            List<UserRequest> customerList = new List<UserRequest>();
            List<string> arr = new List<string> { ".xlsx", ".xls", ".csv", ".xml", ".html", ".tsv", ".ods" };
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                if (!arr.Contains(extension))
                    return null;

                string fileName = DateTime.Now.Ticks.ToString() + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Excel");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Excel", fileName);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                FileInfo excelFile = new FileInfo(exactpath);

                using (ExcelPackage package = new ExcelPackage(excelFile))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets["User_Sheet"];
                    int totalRows = workSheet.Dimension.Rows;


                    for (int i = 2; i <= totalRows; i++)
                    {
                        var user = new UserRequest
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = workSheet.Cells[i, 2].Value.ToString() + DateTime.Now.Ticks,
                            UserName = workSheet.Cells[i, 3].Value.ToString() + DateTime.Now.Ticks,
                            FirstName = workSheet.Cells[i, 4].Value.ToString(),
                            LastName = workSheet.Cells[i, 5].Value.ToString(),
                            Password = "H#ng123456",
                            Phone = workSheet.Cells[i, 6].Value.ToString(),
                        };
                        await _userService.StoreUser(user);

                        customerList.Add(user);
                    }
                    Console.WriteLine(totalRows);

                    return customerList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return customerList;
        }
    }
}
