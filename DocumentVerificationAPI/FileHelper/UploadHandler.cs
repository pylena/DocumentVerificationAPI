namespace DocumentVerificationAPI.FileHelper
{
    
        
       
        public class UploadHandler
        {
            private readonly List<string> _validExtensions = new List<string> { ".pdf", ".txt" };
            private const long MaxFileSize = 5 * 1024 * 1024; 

            public string Upload(IFormFile file)
            {
                if (file == null || file.Length == 0)
                {
                    return "No file uploaded.";
                }

                // Validate extension
                string extension = Path.GetExtension(file.FileName).ToLower();
                if (!_validExtensions.Contains(extension))
                {
                    return $"Extension is not valid. Allowed: {string.Join(", ", _validExtensions)}";
                }

                // Validate file size
                if (file.Length > MaxFileSize)
                {
                    return "Max file size is 5MB.";
                }

                // Ensure Uploads directory exists
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate unique file name
                string uniqueFileName = $"{Guid.NewGuid()}{extension}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return filePath; // Return saved file path
            }
        }
    }









    /*
    //extention
    List<string> validExtenions = new List<string>() { ".pdf", ".txt" };

    string extention = Path.GetExtension(file.FileName);
    if (!validExtenions.Contains(extention))
    {
        return $"Extention is not vaild ({string.Join(',', validExtenions)})";

    }

    //file size
    long size = file.Length;
    if (size > (5 * 1024 * 1024))
    {
        return "Max size can be 5mb";
    }
    string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
    using FileStream stream = new FileStream(Path.Combine(path, file.Name), FileMode.Create);
    file.CopyTo(stream);    
    return file.Name;
    */


