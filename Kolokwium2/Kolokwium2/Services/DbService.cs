using Kolokwium2.Data;
using Kolokwium2.DTOs;
using Kolokwium2.Entities;
using Kolokwium2.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2;

public class DbService : IDbService
{
    private readonly AppDbContext _context;

    public DbService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetPatientsDto>> GetPatientsAsync(string? lastName)
    {
        var res = await _context.Patients
            .Where(p => lastName == null || p.LastName.Contains(lastName))
            .Select(p => new GetPatientsDto
        {
            FirstName = p.FirstName,
            LastName = p.LastName,
            DateOfBirth = p.DateOfBirth,
            Phone =  p.Phone,
            Appointments = p.Appointments.Select(ap => new GetAppointmentDto
            {
                AppointmentId = ap.AppointmentId,
                Doctor = new GetDoctorDto
                {
                    FirstName = ap.Doctor.FirstName,
                    LastName = ap.Doctor.LastName,
                    Specialization =  ap.Doctor.Specialization,
                    Phone = ap.Doctor.Phone
                },
                AppointmentDate = ap.AppointmentDate,
                Status = ap.Status.ToString(),
                AppointmentServices = ap.AppointmentServices.Select(aps => new GetAppointmentServicesDto
                {
                    Quantity = aps.Quantity,
                    PerformedAt =  aps.PerformedAt,
                    MedicalService = new GetMedicalServiceDto
                    {
                        ServiceId =  aps.MedicalService.ServiceId,
                        Name = aps.MedicalService.Name,
                        Description = aps.MedicalService.Description,
                        Price = (int) aps.MedicalService.Price,
                        DurationMinutes =  aps.MedicalService.DurationMinutes
                    }
                }).ToList()
                
            }).ToList()
        }).ToListAsync();
        
        return res;
    }

    public async Task AddPatientAsync(AddPatientDto addPatientDto)
    {
        var anyDoctor = await _context.Doctors.AnyAsync(d => d.DoctorId == addPatientDto.Appointment.DocctorId);
        if (!anyDoctor)
        {
            throw new NotFoundException("Nie znaleziono doktora o podanym Id");
        }
        
        if (addPatientDto.Appointment.AppointmentDate < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new BadRequestException("Zła data");
        }
        
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            var patient = new Patient()
            {
                FirstName = addPatientDto.FirstName,
                LastName = addPatientDto.LastName,
                // Date = DateOnly.FromDateTime(addPrescriptionDto.Date),
                // DateOfBirth = addPatientDto.DateOfBirth.ToDateTime(),
                DateOfBirth = DateTime.Now,
                // DateOfBirth = addPatientDto.DateOfBirth.ToDateTime(),
                Phone = addPatientDto.Phone
            };
            await _context.AddAsync(patient);
            await _context.SaveChangesAsync();

            var appointment = new Appointment()
            {
                PatientId = patient.PatientId,
                DoctorId = addPatientDto.Appointment.DocctorId,
                AppointmentDate = addPatientDto.Appointment.AppointmentDate,
                Status = "Active"
            };
                
            await _context.AddAsync(appointment);
            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}