using AutoMapper;
using Health_Care_System.DoctorDTOs;
using Health_Care_System.Entities;

namespace Health_Care_System.Helper
{
    public class DoctorProfile:Profile
    {
        public DoctorProfile()
        {
            CreateMap<CreateDoctorRequest, Doctor>();
            CreateMap<Doctor,GetDoctorResponse>();
            CreateMap<UpdateDoctorRequest, Doctor>();
        }
    }
}
