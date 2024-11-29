public class Service
{
    public int ServiceId { get; set; }
    public int ServiceTypeId { get; set; }
    public string Name { get; set; }
    public decimal Cost { get; set; }

    public Service(int serviceId, int serviceTypeId, string name, decimal cost)
    {
        ServiceId = serviceId;
        ServiceTypeId = serviceTypeId;
        Name = name;
        Cost = cost;
    }

    public override string ToString()
    {
        return $"{ServiceId}, {ServiceTypeId}, {Name}, {Cost}";
    }
}
