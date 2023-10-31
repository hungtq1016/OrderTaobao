
using BaseSource.Dto;
using Microsoft.AspNetCore.Http;

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
                    return null;
                }
                try
                {
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
                catch (Exception ex)
                {
                    throw new ArithmeticException("Access denied - You must be at least 18 years old.");
                }
            }

            return fileRes;
        }

        public static string GetExtension(string name)
        {
            return name.Split('.')[name.Split('.').Length - 1];
        }
    }
}
