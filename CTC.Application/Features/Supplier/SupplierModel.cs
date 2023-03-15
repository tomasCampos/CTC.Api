using CTC.Application.Shared.Person;
using System;

namespace CTC.Application.Features.Supplier
{
    internal sealed class SupplierModel : PersonModel
    {
        public SupplierModel(in string supplierId, in string personId, in string firstName, in string email, in string phone, in string document) : base(personId, firstName, email, phone, document)
        {
            SupplierId = supplierId;
        }

        public SupplierModel(in string firstName, in string email, in string phone, in string document) : base(Guid.NewGuid().ToString(), firstName, email, phone, document)
        {
            SupplierId = Guid.NewGuid().ToString();
        }

        public string SupplierId { get; set; }
    }
}
