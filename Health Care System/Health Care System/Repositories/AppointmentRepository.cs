using Health_Care_System.Data;
using Health_Care_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Health_Care_System.Repositories
{
    public class AppointmentRepository:IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
           await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task<bool> DeleteAsync(int patientId, int doctorId)
        {
           var appoiment=  await GetByIdAsync(patientId,doctorId);

            if(appoiment is null)
                return false;

            _context.Appointments.Remove(appoiment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                 .Include(a => a.Doctor)
                 .ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int patientId, int doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.PatientId == patientId && a.DoctorId == doctorId);
        }

        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
