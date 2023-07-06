using Core.APP.DomainObjects;

namespace StoreEnterprise.Clients.API.Models
{
    public class Address : Entity
    {
        public Address(string streetAddress, string number, string unit, string neighborhood, string postalCode, string city, string state)
        {
            StreetAddress = streetAddress;
            Number = number;
            Unit = unit;
            Neighborhood = neighborhood;
            PostalCode = postalCode;
            City = city;
            State = state;
        }

        public string StreetAddress { get; private set; }
        public string Number { get; private set; }
        public string Unit { get; private set; }
        public string Neighborhood { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Guid ClientId { get; private set; }
        public Client Client { get; private set; }

    }
}
