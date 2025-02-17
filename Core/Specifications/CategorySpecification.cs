
using Core.Entities;
using Core.Specifications.SpecParams;

namespace Core.Specifications
{
    public class CategorySpecification : BaseSpecification<Category>
    {
        public CategorySpecification(CategorySpecParams specParams)
            : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)))
        {
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            switch (specParams.Sort)
            {
                case "AtoZ":
                    AddOrderBy(x => x.Name);
                    break;
                case "ZtoA":
                    AddOrderByDesc(x => x.Name);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
        }
    }
}
