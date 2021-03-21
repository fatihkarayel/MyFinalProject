using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        /*
        public static IDataResult<string> CreateFile(IFormFile file)
        {
            string path = Directory.GetCurrentDirectory() + "\\wwwroot";
            string folder = "\\images\\";
            string defaultImage = (folder + "default_img.png").Replace("\\", "/");

            if (file == null)
            {
                return new SuccessDataResult<string>(defaultImage, "");
            }

            string extension = Path.GetExtension(file.FileName);
            string guid = Guid.NewGuid().ToString() + DateTime.Now.Millisecond + "_" + DateTime.Now.Hour + extension + "_" + DateTime.Now.Minute;
            string imagePath = folder + guid + extension;

            using (FileStream fileStream = File.Create(path + imagePath))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
                imagePath = (imagePath).Replace("\\", "/");

                return new SuccessDataResult<string>(imagePath, "");
            }

        }

        public static IResult DeleteFile(string filePath)
        {
            string path = Directory.GetCurrentDirectory() + "\\wwwroot";
            string folder = "\\images\\";
            string defaultImage = (folder + "default_img.png").Replace("\\", "/");
            filePath = filePath.Replace("\\", "/");

            try
            {
                if (filePath != defaultImage)
                {
                    File.Delete(path + (filePath));
                }
            }
            catch (Exception)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        public static IDataResult<string> UpdateFile(IFormFile file, string filePath)
        {
            string imapePath = (filePath).Replace("\\", "/");
            DeleteFile(imapePath);
            var newImage = CreateFile(file);

            return newImage;
        }
        */

        public static string AddAsync(IFormFile file)
        {
            var result = newPath(file);

            try
            {
                var sourcepath = Path.GetTempFileName();

                using (var stream = new FileStream(sourcepath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                File.Move(sourcepath, result.newPath);
            }
            catch (Exception exception)
            {

                return exception.Message;
            }

            return result.Path2;
        }

        public static string UpdateAsync(string sourcePath, IFormFile file)
        {
            var result = newPath(file);

            try
            {
                using (var stream = new FileStream(result.newPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                File.Delete(sourcePath);
            }
            catch (Exception excepiton)
            {
                return excepiton.Message;
            }

            return result.Path2;
        }

        public static IResult DeleteAsync(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }

        public static (string newPath, string Path2) newPath(IFormFile file)
        {
            FileInfo ff = new FileInfo(file.FileName);

            string fileExtension = ff.Extension;

            var creatingUniqueFilename = Guid.NewGuid().ToString("N") + fileExtension;

            string result = $@"{Environment.CurrentDirectory + @"\wwwroot\Images"}\{creatingUniqueFilename}";

            return (result, $"\\Images\\{creatingUniqueFilename}");
        }



    }

}
