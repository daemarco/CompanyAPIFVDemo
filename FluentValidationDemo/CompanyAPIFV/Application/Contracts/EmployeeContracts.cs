#nullable disable

namespace CompanyAPIFV.Application.Contracts
{
    public class RegisterEmployeeRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //public AddressDto[] Addresses { get; set; }
    }

    public class RegisterEmployeeResponse
    {
        public long Id { get; set; }
    }
}
