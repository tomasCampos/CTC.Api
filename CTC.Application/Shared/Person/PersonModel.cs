using System;

namespace CTC.Application.Shared.Person
{
    internal abstract class PersonModel
    {
        protected PersonModel(in string personId, in string firstName, in string email, in string phone, in string document)
        {
            if (string.IsNullOrWhiteSpace(personId))
                throw new ArgumentNullException(nameof(personId));
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            PersonId = personId;
            FirstName = firstName;
            Email = email;
            Phone = phone;
            Document = document;
        }

        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Document { get; set; }
    }
}
