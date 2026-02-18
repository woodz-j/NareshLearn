using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NareshLearn.Domain.Common;


namespace NareshLearn.Domain.Users
{
    public class User : AuditableEntity
    {
        private static readonly Regex EmailRegex =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public Role Role { get; private set; }

        private User() { } // For EF Core

        public User(
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            Role role)
        {
            Validate(firstName, lastName, email, passwordHash);


            FirstName = firstName;
            LastName = lastName;
            Email = email.ToLowerInvariant();
            PasswordHash = passwordHash;
            Role = role;
        }

        private static void Validate(
            string firstName,
            string lastName,
            string email,
            string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First name cannot be empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Last name cannot be empty.");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email cannot be empty.");

            if (!EmailRegex.IsMatch(email))
                throw new DomainException("Email format is invalid.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("Password hash cannot be empty.");
        }


        public void ChangeRole(Role newRole)
        {
            Role = newRole;
            MarkUpdated();
        }

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Names cannot be empty.");

            FirstName = firstName;
            LastName = lastName;
            MarkUpdated();
        }
    }

}
