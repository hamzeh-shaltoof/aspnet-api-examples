namespace UnitOfWork.Request
{
    public class CreateProductReview
    {
        public string? Reviewer { get; set; }
        public Guid ProductId{ get; set; }
        public int Stars { get; set; }
    }

}
