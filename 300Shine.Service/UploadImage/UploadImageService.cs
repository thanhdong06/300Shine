using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _300Shine.Service.UploadImage
{
    public class UploadImageService : IUploadImageService
    {
        private readonly Cloudinary cloudinary;
        public UploadImageService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
                (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            cloudinary = new Cloudinary ( acc );
        }

        public Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            return null;
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }
    }
}
