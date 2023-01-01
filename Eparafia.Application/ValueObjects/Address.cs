public sealed class Address : ValueObject
{
    public Address(string region, string city, string street, string buildingNumber, string postCode)
    {
        Region = region;
        City = city;
        Street = street;
        BuildingNumber = buildingNumber;
        PostCode = postCode;
    }
    public string Region { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string BuildingNumber { get; set; } 
    public string PostCode { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Region;
        yield return City;
        yield return Street;
        yield return BuildingNumber;
        yield return PostCode;
    }
}