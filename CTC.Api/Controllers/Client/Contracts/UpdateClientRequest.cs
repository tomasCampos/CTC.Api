﻿namespace CTC.Api.Controllers.Client.Contracts
{
    public class UpdateClientRequest
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Document { get; set; }
    }
}