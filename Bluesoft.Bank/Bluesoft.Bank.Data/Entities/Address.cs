namespace Bluesoft.Bank.Data.Entities;

#nullable disable

/// <summary>
/// Estandarizes the addres in the whole system.
/// </summary>
public class Address
{
    public int Id { get; set; }
    public string Street1 { get; set; } = string.Empty;

    //Debería tener una lista de Estados/Departamentos/Provincias
    //pero eso haría más largo el problema y la prueba ya es bastante larga.
    public string State { get; set; } = string.Empty;

    //Debería tener una lista de Ciudades
    //pero eso haría más largo el problema y la prueba ya es bastante larga.
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}
