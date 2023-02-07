using CompanyAPIFV.Domain.Models.Common;

namespace CompanyAPIFV.Domain.Models
{
    public class Employee : Entity
    {
        public /*Email*/string Email { get; }
        public string Name { get; private set; }
        //public Address[] Addresses { get; private set; }

        public Employee(string email, string name/*, Address[] addresses*/)
        {
            Email = email;
        }

        public void EditPersonalInformation(string name/*, Address[] addresses*/)
        {
            Name = name;
            //Addresses = addresses;
        }


    }
}
