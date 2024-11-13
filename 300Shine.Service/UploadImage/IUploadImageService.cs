using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.UploadImage
{
    public interface IUploadImageService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);

        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}
