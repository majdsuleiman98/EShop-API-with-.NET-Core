

using Core.Entities;
using Core.Specifications.SpecParams;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(int id) : base(x=>x.Id == id)
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Category);
        }

        public ProductSpecification(ProductSpecParams specParams) 
            :base(x=>
                (!specParams.BrandIds.Any() || specParams.BrandIds.Contains(x.BrandId)) &&
                (!specParams.CategoryIds.Any() || specParams.CategoryIds.Contains(x.CategoryId)) && 
                (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)))
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Category);

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            
            switch (specParams.Sort)
            {
                case "PriceAsc":
                    AddOrderBy(x => x.Price);
                    break;
                case "PriceDesc":
                    AddOrderByDesc(x => x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
        }
    }
}
