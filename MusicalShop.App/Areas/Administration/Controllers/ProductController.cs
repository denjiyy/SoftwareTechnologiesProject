namespace MusicalShop.App.Areas.Administration.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MusicalShop.Services;
    using MusicalShop.Services.Mapping;
    using MusicalShop.Services.Models;
    using MusicalShop.Web.InputModels.Product;
    using MusicalShop.Web.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class ProductController : AdminController
    {
        private readonly IProductService productService;
        private readonly ICloudinaryService cloudinaryService;

        public ProductController(IProductService productService,
            ICloudinaryService cloudinaryService)
        {
            this.productService = productService;
            this.cloudinaryService = cloudinaryService;
        }
        public async Task<IActionResult> Create()
        {
            List<ProductTypeViewModel> productTypes = await productService.GetAllProductTypes()
                .Select(x => new ProductTypeViewModel
                {
                    Name = x.Name
                }).ToListAsync();

            ViewData["types"] = productTypes;

            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var productTypes = await productService.GetAllProductTypes().Select(x => new ProductTypeViewModel
                {
                    Name = x.Name
                })
                    .ToListAsync();

                ViewData["types"] = productTypes;

                return this.View();
            }

            var pictureUrl = await this.cloudinaryService.UploadPictureAsync(model.Picture, model.Name);

            var productServiceModel = Mapper.Map<ProductServiceModel>(model);
            productServiceModel.Picture = pictureUrl;

            await productService.CreateProductAsync(productServiceModel);

            return this.Redirect("/");
        }

        [HttpGet("/Administration/Product/Type/Create")]
        public async Task<IActionResult> CreateType()
        {
            return this.View("Type/Create");
        }

        [HttpPost("/Administration/Product/Type/Create")]
        public async Task<IActionResult> CreateType(ProductTypeCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var productType = Mapper.Map<ProductTypeServiceModel>(model);
            await productService.CreateProductTypeAsync(productType);
            return this.Redirect("/");
        }

        [HttpGet("/Administration/Product/Brand/Create")]
        public async Task<IActionResult> CreateBrand()
        {
            return this.View("Brand/Create");
        }

        [HttpPost("/Administration/Product/Brand/Create")]
        public async Task<IActionResult> CreateBrand(ProductBrandCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var productType = Mapper.Map<ProductBrandServiceModel>(model);
            await productService.CreateProductBrandAsync(productType);
            return this.Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]string id)
        {
            await productService.DeleteProductByIdAsync(id);

            return this.Redirect("/");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, ProductEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var productTypes = await productService.GetAllProductTypes().Select(x => new ProductTypeViewModel
                {
                    Name = x.Name
                })
                   .ToListAsync();

                ViewData["types"] = productTypes;

                return this.View(model);
            }

            var productServiceModel = Mapper.Map<ProductServiceModel>(model);


            if (model.Picture != null)
            {
                var pictureUrl = await this.cloudinaryService
                .UploadPictureAsync(model.Picture, model.Name);

                productServiceModel.Picture = pictureUrl;

            }

            await productService.EditProductAsync(id, productServiceModel);
            return this.Redirect("/");
        }
        [HttpGet("/Administration/Product/Edit/{id?}")]
        public async Task<IActionResult> Edit(string id)
        {
            var product = (await productService.GetProductByIdAsync(id)).To<ProductEditInputModel>();

            var productTypes = await productService.GetAllProductTypes()
                .Select(x => new ProductTypeViewModel
                {
                    Name = x.Name
                }).ToListAsync();

            ViewData["types"] = productTypes;

            return this.View(product);
        }

    }
}