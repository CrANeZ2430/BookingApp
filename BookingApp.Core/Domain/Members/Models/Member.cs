using System.Text.RegularExpressions;
using BookingApp.Core.Abstractions;
using BookingApp.Core.Common;
using BookingApp.Core.Domain.Bookings.Models;
using BookingApp.Core.Domain.Members.DomainEvents;
using BookingApp.Core.Exceptions;

namespace BookingApp.Core.Domain.Members.Models;

public class Member : AggregateRoot
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
        string phoneNumber,
        IDateTimeProvider dateTimeProvider)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new BadRequestException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new BadRequestException("Last name is required.");

        if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            throw new BadRequestException("A valid email is required.");
        
        if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+[1-9]\d{1,14}$"))
            throw new BadRequestException("A valid phone number is required.");
        
        var member =  new Member(
            firstName,
            lastName,
            role,
            email,
            phoneNumber);
        
        member.RaiseDomainEvent(new CreateMemberEvent(dateTimeProvider.GetCurrentDateTime(), member.MemberId));

        return member;
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

        if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            throw new BadRequestException("A valid email is required.");
        
        if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+[1-9]\d{1,14}$"))
            throw new BadRequestException("A valid phone number is required.");
        
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}