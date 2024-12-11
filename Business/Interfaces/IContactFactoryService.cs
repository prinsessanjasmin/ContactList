using Business.DTOs;
using Business.Models;

namespace Business.Interfaces
{
    public interface IContactFactoryService
    {
        Contact Create(ContactDto dto);
    }
}