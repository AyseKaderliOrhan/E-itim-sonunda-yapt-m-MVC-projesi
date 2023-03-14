using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BilgeShop.Business.Dtos;
using BilgeShop.Business.Services;
using BilgeShop.Business.Types;
using BilgeShop.Data.Entities;
using BilgeShop.Data.Repositories;

namespace BilgeShop.Business.Manager
{
    public class ProductManager : IProductService
    {
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly ICategoryService _categoryService;
        public ProductManager(IRepository<ProductEntity> productrepository, ICategoryService categoryService)
        {
            _productRepository = productrepository;
            _categoryService = categoryService;
        }
        public ServiceMessage AddProduct(AddProductDto addProductDto)
        {
            var hasProduct = _productRepository.GetAll(x => x.Name.ToLower() == addProductDto.Name.ToLower()).ToList();

            if (hasProduct.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu isimde bir ürün zaten mevcut."
                };
            }
            var productEntity = new ProductEntity()
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                UnitInStock = addProductDto.UnitInStock,
                UnitPrice = addProductDto.UnitPrice,
                CategoryId = addProductDto.CategoryId,
                ImagePath = addProductDto.ImagePath

            };
            _productRepository.Add(productEntity);

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Ürün başarıyla eklendi."
            };
        }

        public void DeleteProduct(int id)
        {
            _productRepository.Delete(id);
        }

        public EditProductDto GetProductById(int id)
        {
            var productEntity = _productRepository.GetById(id);

            var editProductDto = new EditProductDto()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                UnitInStock = productEntity.UnitInStock,
                UnitPrice = productEntity.UnitPrice,
                CategoryId = productEntity.CategoryId
                
            };
            return editProductDto;
        }

        public ProductDetailDto GetProductDetail(int id)
        {
            var productEntity = _productRepository.GetById(id);

            var productDetailDto = new ProductDetailDto()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,                
                UnitPrice = productEntity.UnitPrice,
                CategoryId = productEntity.CategoryId,                
                ImagePath = productEntity.ImagePath,
                UnitInStock= productEntity.UnitInStock

            };
            productDetailDto.CategoryName = _categoryService.GetCategoryName(productDetailDto.CategoryId);

            return productDetailDto;
        }

        public List<ProductDto> GetProducts()
        {
            var productEntites = _productRepository.GetAll().OrderBy(x => x.Category.Name).ThenBy(x => x.Name);

            var productDtoList = productEntites.Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description= x.Description,
                UnitInStock= x.UnitInStock,
                UnitPrice= x.UnitPrice,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                ImagePath = x.ImagePath


            }).ToList();

            return productDtoList;
        }

        public List<ProductDto> GetProductsByCategoryId(int? categoryId = null)
        {
            if (categoryId.HasValue) // is not null --> != null
            {
                var productEntites = _productRepository.GetAll(x => x.CategoryId == categoryId).OrderBy(x => x.Name);

                var productDtos = productEntites.Select(x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    UnitInStock = x.UnitInStock,
                    UnitPrice = x.UnitPrice,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    ImagePath = x.ImagePath


                }).ToList();

                return productDtos;
            }
            else
            {
                return GetProducts();
            }
        }

        public void UpdateProduct(EditProductDto editProductDto)
        {
            var productentity = _productRepository.GetById(editProductDto.Id);

            productentity.Name = editProductDto.Name;
            productentity.Description = editProductDto.Description;
            productentity.UnitInStock = editProductDto.UnitInStock;
            productentity.UnitPrice = editProductDto.UnitPrice;
            productentity.CategoryId = editProductDto.CategoryId;

            if (editProductDto.ImagePath is not null)
            {
                productentity.ImagePath = editProductDto.ImagePath;
            }

            _productRepository.Update(productentity);
        }
    }
}
