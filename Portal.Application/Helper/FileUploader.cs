
using Microsoft.AspNetCore.Http;
using Portal.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Helper
{
    public static class FileUploader
    {
        private static string CreateDirectoryIfIsNotExist(FoldersNamesEnum folder)
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(), FoldersNamesEnum.wwwroot.ToString(), folder.ToString());
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory;
        }
        private static void CreateDirectoryIfIsNotExist(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
        private static bool IsImageInAllowExtension(IFormFile File)
        {
            var allowedExtenstions = new List<string> { ".jpg", ".png", ".jpeg" };
            if (!allowedExtenstions.Contains(Path.GetExtension(File.FileName).ToLower()))
            {
                return false;
            }
            return true;
        }

        public static string UploadFile(FoldersNamesEnum FileOrImage, string FolderName, IFormFile File)
        {
            string FolderPath = null;
            if (FileOrImage == FoldersNamesEnum.imgs)
            {
                FolderPath = CreateDirectoryIfIsNotExist(FoldersNamesEnum.imgs);
                if (!IsImageInAllowExtension(File))
                    return null!;
            }
            else if (FileOrImage == FoldersNamesEnum.files)
                FolderPath = CreateDirectoryIfIsNotExist(FoldersNamesEnum.files);            

            if (FolderPath == null)
                return null!;

            try
            {
                FolderPath = Path.Combine(FolderPath, FolderName);
                CreateDirectoryIfIsNotExist(FolderPath);
                string FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);
                string FinalPath = Path.Combine(FolderPath, FileName);
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    File.CopyTo(Stream);
                }
                return FileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public static string RemoveFile(FoldersNamesEnum folder, string FolderName, string fileName)
        {
            try
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), $"{FoldersNamesEnum.wwwroot}/{folder}", FolderName, fileName);
                if (File.Exists(directory))
                {
                    File.Delete(directory);
                    return null;
                }
                return "File Not Deleted";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
