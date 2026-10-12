namespace E_commerce_System.OrderDetailsDTOs
{
    public class UpdateOrderDetailRequest
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
