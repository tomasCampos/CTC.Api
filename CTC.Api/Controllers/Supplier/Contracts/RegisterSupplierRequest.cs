namespace CTC.Api.Controllers.Supplier.Contracts
{
    public class RegisterSupplierRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Document { get; set; }
    }
}
