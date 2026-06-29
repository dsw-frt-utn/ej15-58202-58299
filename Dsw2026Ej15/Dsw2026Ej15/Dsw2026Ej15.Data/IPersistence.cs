using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data
{
    public interface IPersistence
    {
        void AddDoctor(Doctor doctor);

        List<Doctor> GetDoctors();

        Doctor? GetDoctor(Guid id);

        Speciality? GetSpeciality(Guid id);
    }
}
