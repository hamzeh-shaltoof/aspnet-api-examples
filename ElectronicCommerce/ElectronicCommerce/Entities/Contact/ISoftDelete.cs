namespace ElectronicCommerce.Entities.Contact
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }

        public void Delete()
        {
            this.IsDeleted = true;
            this.DateDeleted = DateTime.Now;
        }
        public void UndoDelete()
        {
            this.IsDeleted = false;
            this.DateDeleted = null;
        }
    }
}
