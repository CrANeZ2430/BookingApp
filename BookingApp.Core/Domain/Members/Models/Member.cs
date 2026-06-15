using BookingApp.Core.Domain.Bookings.Models;

namespace BookingApp.Core.Domain.Members.Models;

public class Member
{
    private readonly List<Booking> _bookings = new();
    
    public Guid MemberId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Roles Role { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }

    public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();
    
    private Member() {}

    private Member(
        string firstName, 
        string lastName, 
        Roles role, 
        string email, 
        string phoneNumber)
    {
        MemberId = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static Member Create(
        string firstName, 
        string lastName,
        Roles role,
        string email,
        string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("A valid email is required.", nameof(email));
        
        return new Member(
            firstName,
            lastName,
            role,
            email,
            phoneNumber);
    }

    public void Update(
        string firstName, 
        string lastName,
        Roles role,
        string email,
        string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("A valid email is required.", nameof(email));
        
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}