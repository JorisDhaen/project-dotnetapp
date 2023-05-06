using Microsoft.EntityFrameworkCore;
using Server.Models;
using Shared.Products;
using Shared.Suppliers;

namespace Services.Products;

public class ProductsService : IProductService
{
    private readonly DelawareDbContext dbContext;
    private readonly DbSet<Product> products;

    public ProductsService(DelawareDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.products = dbContext.Products;
    }

    public async Task<ProductResponse.GetDetail> GetDetailAsync(ProductRequest.GetDetail request)
    {
        ProductResponse.GetDetail response = new();
        response.Product = await products.Select(p => new ProductDto.Index
        {
            ProductId = p.Idproduct,
            ProductCategoryId = p.ProductCatergoryId,
            Name = p.Name,
            UnitPrice = p.UnitPrice,
            LeftInStock = p.LeftInStock,
            Description = p.Description,
            Height = p.Height,
            Width = p.Width,
            Depth = p.Depth,
            Supplier = new SupplierDto.Index
            {
                SupplierId = p.SupplierId,
            }
        }).SingleAsync(p => p.ProductId == request.ProductId);

        return response;
    }

    public async Task<ProductResponse.GetIndex> GetIndexAsync(ProductRequest.GetIndex request)
    {
        ProductResponse.GetIndex response = new();
        var query = dbContext.Products.AsQueryable();

        response.Products = await query.Select(p => new ProductDto.Index
        {
            ProductId = p.Idproduct,
            ProductCategoryId = p.ProductCatergoryId,
            Name = p.Name,
            UnitPrice = p.UnitPrice,
            LeftInStock = p.LeftInStock,
            Description = p.Description,
            Height= p.Height,
            Width= p.Width,
            Depth= p.Depth,
            Supplier = new SupplierDto.Index
            {
                SupplierId = p.SupplierId,
            }
        }).ToListAsync();

        return response;    
    }
}
