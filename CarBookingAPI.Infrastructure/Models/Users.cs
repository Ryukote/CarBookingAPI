using CarBookingAPI.Core.Contracts.Models;
using System;

namespace CarBookingAPI.Infrastructure.Models
{
    public class Users : IModel
    {
        private string _username = "";

        public Guid Id { get; set; }
        public string Username 
        { 
            get { return _username; }
            set { _username = value.ToLowerInvariant(); } 
        }
        public string HashedPassword { get; set; }
        public byte[] Salt { get; set; }
    }
}
