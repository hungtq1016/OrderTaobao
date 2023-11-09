
using BaseSource.Dto;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml.Table;
using OfficeOpenXml;

namespace BaseSource.BackendAPI.Services.Helpers
{
    public class FileHelper
    {
        public static async Task<List<FileResponse>> WriteFile(List<IFormFile> files, string folder, string type)
        {
            List<FileResponse> fileRes = new List<FileResponse>();
            foreach (var file in files)
            {
                //Denine if file
                if (file.Length > Math.Pow(2,20)*8)
                {
                    return null!;
                }

                var oldName = file.FileName;
                var extension = "." + GetExtension(oldName);
                string newName = DateTime.Now.Ticks.ToString() + extension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), $"Upload\\{folder}");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), $"Upload\\{folder}", newName);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    fileRes.Add(new FileResponse
                    {
                        Name = oldName,
                        Extension = extension,
                        Path = newName,
                        Size = Convert.ToUInt64(file.Length),
                        Type = type
                    });
                }
            }

            return fileRes;
        }

        public static string GetExtension(string name)
        {
            return name.Split('.')[name.Split('.').Length - 1];
        }

        public static byte[] ExportToExcel<T>(List<T> table, string filename)
        {
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1);
            return pack.GetAsByteArray();
        }
    }
}
