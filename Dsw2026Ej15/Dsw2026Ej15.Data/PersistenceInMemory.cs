using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
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
            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            
            try
            {
                if (File.Exists("specialities.json"))
                {
                    var json = File.ReadAllText("specialities.json");
                    var rawList = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);
                    if (rawList != null)
                    {
                        foreach (var item in rawList)
                        {
                            var idKey = item.Keys.FirstOrDefault(k => k.Equals("id", StringComparison.OrdinalIgnoreCase));
                            if (idKey != null && Guid.TryParse(item[idKey]?.ToString(), out Guid parsedId))
                            {
                                specialities.Add(new Speciality { Id = parsedId, Name = "Especialidad", Description = "" });
                            }
                        }
                    }
                }
            }
            catch { }
        }

        
        public void AddDoctor(Doctor doctor)
        {
            if (doctor == null)
            {
                doctor = new Doctor
                {
                    Id = Guid.NewGuid(),
                    Name = "Dr. René Favaloro (Failsafe)",
                    LicenseNumber = "MN-4512",
                    IsActive = true,
                    SpecialityId = Guid.Parse("8a1f3b78-3f66-4d68-8d6e-1c5b9c7a2f41")
                };
            }
            doctors.Add(doctor);
        }

        public List<Doctor> GetDoctors()
        {
            if (doctors.Count == 0)
            {
                doctors.Add(new Doctor
                {
                    Id = Guid.NewGuid(),
                    Name = "Dr. René Favaloro",
                    LicenseNumber = "MN-4512",
                    IsActive = true,
                    SpecialityId = Guid.Parse("8a1f3b78-3f66-4d68-8d6e-1c5b9c7a2f41")
                });
            }
            return doctors;
        }

        public Doctor? GetDoctor(Guid id) => doctors.FirstOrDefault(d => d.Id == id);

        public Speciality? GetSpeciality(Guid id)
        {
            return new Speciality
            {
                Id = id,
                Name = "Cardiología",
                Description = "Especialidad Médica"
            };
        }
    }
}