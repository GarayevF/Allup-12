﻿namespace Allup.Extensions
{
    public static class FileExtension
    {
        public static async Task<string> CreateFileAsync(this IFormFile file, IWebHostEnvironment env, params string[] folders)
        {
            int lastIndex = file.FileName.LastIndexOf('.');

            string name = file.FileName.Substring(lastIndex);

            string fileName = $"{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}_{Guid.NewGuid()}{name}";

            string fullPath = Path.Combine(env.WebRootPath);

            foreach (string folder in folders)
            {
                fullPath = Path.Combine(fullPath, folder);
            }

            fullPath = Path.Combine(fullPath, fileName);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
