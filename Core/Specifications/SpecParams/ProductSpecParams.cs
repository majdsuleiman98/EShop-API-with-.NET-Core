namespace Core.Specifications.SpecParams
{
    public class ProductSpecParams:BaseSpecParams
    {     
        private List<int> _brandIds = [];
        public List<int> BrandIds
        {
            get => _brandIds;
            set => _brandIds = value;
        }
        private List<int> _categoryIds = [];
        public List<int> CategoryIds
        {
            get => _categoryIds;
            set => _categoryIds = value;
        }
        
    }
}
