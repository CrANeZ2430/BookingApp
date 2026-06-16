using BookingApp.Core.Domain.Bookings.Models;
using BookingApp.Core.Exceptions;

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
            throw new BadRequestException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new BadRequestException("Last name is required.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new BadRequestException("A valid email is required.");
        
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
            throw new BadRequestException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new BadRequestException("Last name is required.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new BadRequestException("A valid email is required.");
        
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}