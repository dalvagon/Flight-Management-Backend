﻿namespace FlightManagement.API.Dtos
{
    public class CreateAddressDto
    {
        public string Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}