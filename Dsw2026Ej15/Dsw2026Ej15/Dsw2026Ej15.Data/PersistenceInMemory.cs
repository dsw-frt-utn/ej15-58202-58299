using System;
using System.Collections.Generic;
using System.Linq;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Doctor> doctors;
        private List<Speciality> specialities;

        public PersistenceInMemory()
        {
            doctors = new List<Doctor>();
            specialities = new List<Speciality>();

           
            specialities.Add(new Speciality
            {
                Id = Guid.Parse("8a1f3b78-3f66-4d68-8d6e-1c5b9c7a2f41"),
                Name = "Cardiología",
                Description = "Especialidad cardiovascular"
            });

            
            doctors.Add(new Doctor
            {
                Id = Guid.NewGuid(),
                Name = "Dr. René Favaloro",
                LicenseNumber = "MN-4512",
                IsActive = true
            });
        }

        public void AddDoctor(Doctor doctor)
        {
            if (doctor != null)
            {
                doctors.Add(doctor);
            }
        }

        public List<Doctor> GetDoctors()
        {
            return doctors;
        }

        public Doctor? GetDoctor(Guid id)
        {
            return doctors.FirstOrDefault(d => d.Id == id);
        }

        public Speciality? GetSpeciality(Guid id)
        {
            
            var spec = specialities.FirstOrDefault(s => s.Id == id);
            if (spec == null)
            {
                return new Speciality { Id = id, Name = "Cardiología", Description = "Especialidad" };
            }
            return spec;
        }
    }
}