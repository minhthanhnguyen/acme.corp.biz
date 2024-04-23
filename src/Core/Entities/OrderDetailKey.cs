namespace Core.Entities
{
    public class OrderDetailKey : IComparable, IEquatable<OrderDetailKey>
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int CompareTo(object? obj)
        {
            OrderDetailKey otherObj = obj as OrderDetailKey;

            if (otherObj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (this.OrderId == otherObj.OrderId)
            {
                return this.ProductId.CompareTo(otherObj.ProductId);
            }
            else
            {
                return this.OrderId.CompareTo(otherObj.OrderId);
            }
        }

        public bool Equals(OrderDetailKey? other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return this.OrderId == other.OrderId && 
                   this.ProductId == other.ProductId;
        }
    }
}
