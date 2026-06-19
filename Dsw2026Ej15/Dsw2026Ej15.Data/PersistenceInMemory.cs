using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain;
using System.Text.Json;

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
            var json = File.ReadAllText("specialities.json");

            specialities = JsonSerializer.Deserialize<List<Speciality>>(json);
        }
        public void AddDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public List<Doctor> GetDoctors()
        {
            throw new NotImplementedException();
        }

        public Doctor? GetDoctor(Guid id)
        {
            throw new NotImplementedException();
        }

        public Speciality? GetSpeciality(Guid id)
        {
            throw new NotImplementedException();
        }

    }
    
    }
