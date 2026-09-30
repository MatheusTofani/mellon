using Mellon.Domain.ValueObjects;

namespace Mellon.Domain.Entities;

public class Salon
{

    private Salon(
            string id,
            Name name,
            Cnpj cnpj,
            Email email,
            Phone phone
    )
    {
        Id = id; Name = name;
        Cnpj = cnpj;
        Email = email; 
        Phone = phone;
    }

    public string Id { get; private set; }
    public Name Name { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public Phone Phone { get; private set; }

    public static Salon Create(
        Name name,
        Cnpj cnpj,
        Email email,
        Phone phone
    )
    {

        return new Salon(
            id: string.Empty,
            name: name,
            cnpj: cnpj,
            email: email,
            phone: phone
            );
    }
}