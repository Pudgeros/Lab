public class ServiceType
{
    public int ServiceTypeId { get; set; }
    public string Name { get; set; }

    public ServiceType(int serviceTypeId, string name)
    {
        ServiceTypeId = serviceTypeId;
        Name = name;
    }

    public override string ToString()
    {
        return $"{ServiceTypeId}, {Name}";
    }
}
