using AutoMapper;
using Health_Care_System.AppointmentDTOs;
using Health_Care_System.Entities;

namespace Health_Care_System.Helper
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<CreateAppointmentRequest, Appointment>();
            CreateMap<Appointment, GetAppointmentResponse>()
                .ForMember(
                  dest => dest.DoctortName,
                  opt => opt.MapFrom(src => src.Doctor.Name));



            CreateMap<UpdateAppointmentRequest, Appointment>()
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorId, opt => opt.Ignore());
        }
    }
}
