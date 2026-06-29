using Dsw2026Ej15.Data;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult Get()
    {
        var doctors = persistence
            .GetDoctors()
            .Where(d => d.IsActive);

        return Ok(doctors);
    }
    [HttpGet("{id}")]
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
    public IActionResult TestSpecialities()
    {
        // Esto llamará al método de tu persistencia para ver qué cargó
        var repo = HttpContext.RequestServices.GetService(typeof(Dsw2026Ej15.Data.IPersistence)) as Dsw2026Ej15.Data.PersistenceInMemory;

        // Si usaste la interfaz estricta, mapeá una función que devuelva la lista o usá reflexión de prueba:
        return Ok(repo);
    }
}