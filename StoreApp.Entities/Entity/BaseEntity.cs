namespace StoreApp.Entities.Entity;

    public abstract class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }