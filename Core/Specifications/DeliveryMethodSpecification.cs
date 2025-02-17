
using Core.Entities;
using Core.Specifications.SpecParams;

namespace Core.Specifications
{
    public class DeliveryMethodSpecification : BaseSpecification<DeliveryMethod>
    {
        public DeliveryMethodSpecification(DeliveryMethodSpecParams specParams)
            : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.ShortName.ToLower().Contains(specParams.Search)))
        {
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            switch (specParams.Sort)
            {
                case "AtoZ":
                    AddOrderBy(x => x.ShortName);
                    break;
                case "ZtoA":
                    AddOrderByDesc(x => x.ShortName);
                    break;
                case "PriceAsc":
                    AddOrderBy(x => x.Price);
                    break;
                case "PriceDesc":
                    AddOrderByDesc(x => x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Price);
                    break;
            }
        }
    }
}
