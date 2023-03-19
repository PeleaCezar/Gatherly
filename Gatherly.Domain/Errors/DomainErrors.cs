using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Member
        {
            public static readonly Error EmailAlreadyInUse = new(
                "Member.EmailAlreadyInUse",
                "The specified email is already in use");

            public static readonly Func<Guid, Error> NotFound = id => new Error(
                "Member.NotFound",
                $"The member with the identifier {id} was not found.");

            public static readonly Error NotExist = new Error(
                "Members.NotExist",
                $"There is no members");

            public static readonly Error InvalidCredentials = new(
               "Member.InvalidCredentials",
               "The provided credentials are invalid");
        }

        public static class Gathering
        {
            public static readonly Func<Guid, Error> NotFound = id => new Error(
                "Gathering.NotFound",
                $"The gathering with the identifier {id} was not found.");

            public static readonly Error InvitingCreator = new(
                "Gathering.InvitingCreator",
                "Can't send invitation to the gathering creator");

            public static readonly Error AlreadyPassed = new(
                "Gathering.AlreadyPassed",
                "Can't send invitation for gathering in the past");

            public static readonly Error Expired = new(
                "Gathering.Expired",
                "Can't accept invitation for expired gathering");
        }

        public static class Invitation
        {
            public static readonly Func<Guid, Error> AlreadyAccepted = id => new Error(
                "Invitation.AlreadyAccepted",
                $"The invitation with Id {id} has already been accepted");
        }

        public static class Email
        {
            public static readonly Error Empty = new(
                "Email.Empty",
                "Email is empty");

            public static readonly Error InvalidFormat = new(
                "Email.InvalidFormat",
                "Email format is invalid");
        }

        public static class FirstName
        {
            public static readonly Error Empty = new(
                "FirstName.Empty",
                "First name is empty");

            public static readonly Error TooLong = new(
                "LastName.TooLong",
                "FirstName name is too long");
        }

        public static class LastName
        {
            public static readonly Error Empty = new(
                "LastName.Empty",
                "Last name is empty");

            public static readonly Error TooLong = new(
                "LastName.TooLong",
                "Last name is too long");
        }
    }
}
