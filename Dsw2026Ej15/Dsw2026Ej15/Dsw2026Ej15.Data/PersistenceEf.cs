using Dsw2026Ej15.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly ApplicationDbContext context;

    public PersistenceEf(ApplicationDbContext context)
    {
        this.context = context;
    }

    public void AddDoctor(Doctor doctor)
    {
        context.Doctors.Add(doctor);
        context.SaveChanges();
    }

    public Doctor? GetDoctor(Guid id)
    {
        return context.Doctors
            .Include(d => d.Speciality)
            .FirstOrDefault(d => d.Id == id);
    }

    public List<Doctor> GetDoctors()
    {
        return context.Doctors
            .Include(d => d.Speciality)
            .ToList();
    }

    public Speciality? GetSpeciality(Guid id)
    {
        return context.Specialities
            .FirstOrDefault(s => s.Id == id);
    }
}
