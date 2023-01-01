public sealed class Contact : ValueObject
{
    public Contact(string phoneNumber, string email)
    {
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PhoneNumber;
        yield return Email;

    }
}