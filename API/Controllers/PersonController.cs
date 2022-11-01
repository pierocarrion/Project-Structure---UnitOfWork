using API.DTOs;
using AutoMapper;
using Contracts;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Razor.Hosting;
using System;
using System.Net;
using System.Xml.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PersonController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _unitOfWork.Person.GetAll();
            return Ok(persons);
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(PersonForCreationDTO personDTO)
        {
            if (ModelState.IsValid)
            {
                var person = new Person()
                {
                    IdPerson = Guid.NewGuid(),
                    Name = personDTO.Name,
                    LastName = personDTO.LastName,
                    Cellphone = personDTO.Cellphone,
                    Address = personDTO.Address,
                    Dni = personDTO.Dni,
                    Email = personDTO.Email,
                    RegistrationDate = personDTO.RegistrationDate
                };

                await _unitOfWork.Person.Create(person);

                if (await _unitOfWork.CompleteAsync())
                    return CreatedAtAction(nameof(Insert), new { person.IdPerson }, person);

            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid personId)
        {
            var person = await _unitOfWork.Person.GetById(personId);

            if (person is null)
                return NotFound();

            return Ok(person);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(PersonForUpdateDTO personDTO)
        {
            if (ModelState.IsValid)
            {
                var person = new Person()
                {
                    IdPerson = personDTO.IdPerson,
                    Name = personDTO.Name,
                    LastName = personDTO.LastName,
                    Cellphone = personDTO.Cellphone,
                    Address = personDTO.Address,
                    Dni = personDTO.Dni,
                    Email = personDTO.Email
                };

                await _unitOfWork.Person.Update(person);

                if (await _unitOfWork.CompleteAsync())
                    return CreatedAtAction(nameof(Update), new { person.IdPerson }, person);

            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid personId)
        {
            var personToDelete = await _unitOfWork.Person.GetById(personId);

            if (personToDelete == null)
                return BadRequest();

            await _unitOfWork.Person.Delete(personToDelete);

            if (await _unitOfWork.CompleteAsync())
                return Ok();

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }
    }
}
