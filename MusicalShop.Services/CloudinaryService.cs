namespace MusicalShop.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    using System.IO;
    using System.Threading.Tasks;
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;

        public CloudinaryService(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public async Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName)
        {
            byte[] destinationData;
            using var ms = new MemoryStream();
            await pictureFile.CopyToAsync(ms);
            destinationData = ms.ToArray();

            UploadResult uploadResult = null;

            using var memoryStream = new MemoryStream(destinationData);
            var uploadParams = new ImageUploadParams
            {
                Folder = "musical_shop_product_images",
                File = new FileDescription(fileName, memoryStream)
            };

            uploadResult = await this.cloudinaryUtility.UploadAsync(uploadParams);
            return uploadResult?.SecureUrl.AbsoluteUri;
        }
    }
}