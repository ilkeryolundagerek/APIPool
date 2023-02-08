namespace API01.Entities
{
    public abstract class BaseEntity
    {
        public int Id;
        public DateTime CreateTime;
        public DateTime? UpdateTime;
        public DateTime? DeleteTime;
        public bool Active;
        public bool Deleted;
    }
}
