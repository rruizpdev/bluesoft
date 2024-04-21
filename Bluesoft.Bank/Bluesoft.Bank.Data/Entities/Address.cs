﻿namespace Bluesoft.Bank.Data.Entities;

#nullable disable
public class Address
{
    public int Id { get; set; }
    public string Street1 { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}
