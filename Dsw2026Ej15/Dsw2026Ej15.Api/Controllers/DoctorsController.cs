using Dsw2026Ej15.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 
using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Api.Exceptions;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence persistence;

    public DoctorsController(IPersistence persistence)
    {
        this.persistence = persistence;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var doctors = persistence
            .GetDoctors()
            .Where(d => d.IsActive);

        return Ok(doctors);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(Guid id)
    {
        var doctor = persistence.GetDoctor(id);

        if (doctor == null || !doctor.IsActive)
        {
            return NotFound();
        }

        return Ok(doctor);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post(DoctorRequest request)
    {
        var speciality = persistence.GetSpeciality(request.SpecialityId);

        if (speciality == null)
        {
            throw new ValidationException("La especialidad no existe");
        }

        var doctor = new Doctor
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            IsActive = true,
            Speciality = speciality
        };

        persistence.AddDoctor(doctor);

        return Created($"api/doctors/{doctor.Id}", doctor);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        var doctor = persistence.GetDoctor(id);

        if (doctor == null)
        {
            return NotFound();
        }

        doctor.IsActive = false;

        return NoContent();
    }

    [HttpGet("test-specialities")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult TestSpecialities()
    {
        
        var repo = HttpContext.RequestServices.GetService(typeof(Dsw2026Ej15.Data.IPersistence)) as Dsw2026Ej15.Data.PersistenceInMemory;

       
        return Ok(repo);
    }
}