public class Order
{
    public int OrderId { get; set; }
    public int ClientId { get; set; }
    public DateTime OrderDate { get; set; }
    public int ServiceId { get; set; }
    public int Quantity { get; set; }

    public Order(int orderId, int clientId, DateTime orderDate, int serviceId, int quantity)
    {
        OrderId = orderId;
        ClientId = clientId;
        OrderDate = orderDate;
        ServiceId = serviceId;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"{OrderId}, {ClientId}, {OrderDate}, {ServiceId}, {Quantity}";
    }
}
