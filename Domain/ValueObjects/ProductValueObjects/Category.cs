

namespace Domain.ValueObjects.ProductValueObjects
{
    public class Category : ValueObject
    {
        public string CategoryVO { get; }

        public Category(string categoryVO)
        {
            if (categoryVO == null) throw new ArgumentNullException("La categoria es nula");
            CategoryVO = categoryVO.ToUpper();
        }
        protected Category() { }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CategoryVO;
        }
    }
}
