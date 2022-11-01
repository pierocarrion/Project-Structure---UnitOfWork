
namespace API.DTOs
{
    public record PersonForUpdateDTO
    (
        Guid IdPerson,
        string Name,
        string LastName,
        string Cellphone,
        string Address,
        string Dni,
        string Email
    );
}
