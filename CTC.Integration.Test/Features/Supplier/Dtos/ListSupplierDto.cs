namespace CTC.Integration.Test.Features.Supplier.Dtos
{
    internal sealed class ListSupplierDto
    {
        public int TotalCount { get; set; }

        public List<GetSupplierDto>? Results { get; set; }
    }
}
