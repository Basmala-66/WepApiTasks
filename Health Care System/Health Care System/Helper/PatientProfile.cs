using AutoMapper;
using Health_Care_System.DTOs.PatientDTOs;
using Health_Care_System.Entities;

namespace Health_Care_System.Helper
{
    public class PatientProfile:Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientRequest, Patient>();

            CreateMap<Patient, GetPatientResponse>();

           

            CreateMap<UpdatePatientRequest, Patient>();
        }
    }
}
