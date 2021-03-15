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
        Task<ImageUploadResult> AddTipPhotoAsync(IFormFile file);

        Task<ImageUploadResult> AddUserPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string id);

    }
}
