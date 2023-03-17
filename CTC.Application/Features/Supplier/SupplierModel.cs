using CTC.Application.Shared.Person;
using System;

namespace CTC.Application.Features.Supplier
{
    internal sealed class SupplierModel : PersonModel
    {
        public SupplierModel(in string supplierId, in string personId, in string name, in string email, in string phone, in string document) : base(personId, name, email, phone, document)
        {
            SupplierId = supplierId;
        }

        public SupplierModel(in string name, in string email, in string phone, in string document) : base(Guid.NewGuid().ToString(), name, email, phone, document)
        {
            SupplierId = Guid.NewGuid().ToString();
        }

        public SupplierModel() : base()
        {
        }

        public string SupplierId { get; set; }
    }
}
