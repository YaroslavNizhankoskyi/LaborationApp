using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddSmallPhotoAsync(IFormFile file);

        Task<ImageUploadResult> AddMediumPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string id);

    }
}
