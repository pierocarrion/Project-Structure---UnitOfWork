
namespace API.DTOs
{
    public record PersonForCreationDTO
    (
        string Name,
        string LastName,
        string Cellphone,
        string Address,
        string Dni,
        string Email,
        DateTime? RegistrationDate
    );
}
