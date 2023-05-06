namespace Shared.Products
{
    public interface IProductService
    {
        // GEBRUIKT DOOR REAL SERVICE --> HAALT DUS DATA UIT DATABANK
        Task<ProductResponse.GetIndex> GetIndexAsync(ProductRequest.GetIndex request);
        Task<ProductResponse.GetDetail> GetDetailAsync(ProductRequest.GetDetail request);
    }
}
