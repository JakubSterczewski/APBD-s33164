using C8.Data;
using C8.DTOs;
using C8.Exceptions;
using C8.Models;
using Microsoft.EntityFrameworkCore;

namespace C8.Services;

public class DbService : IDbService
{
    private readonly C8Context _context;

    public DbService(C8Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetPatientDto>> GetPatientsAsync(string? search)
    {
        var result = await _context.Patients
            .Where(e => search == null || e.FirstName.Contains(search) || e.LastName.Contains(search))
            .Select(e => new GetPatientDto
            {
                Pesel = e.Pesel,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Age = e.Age,
                Sex = e.Sex ? "Male" : "Female",
                PatientAdmissions = e.Admissions.Select(e => new GetPatientAdmissionDto
                {
                    Id = e.Id,
                    AdmissionDate = e.AdmissionDate,
                    DischargeDate = e.DischargeDate,
                    Ward = new GetWardDto
                    {
                        Id = e.Ward.Id,
                        Name = e.Ward.Name,
                        Description = e.Ward.Description
                    }
                }).ToList(),
                PatientBedAssignments = e.BedAssignments.Select(e => new GetPatientBedAssignmentDto
                {
                    Id = e.Id,
                    From = e.From,
                    To = e.To,
                    Bed = new GetBedDto
                    {
                        Id = e.Bed.Id,
                        BedType = new GetBedTypeDto
                        {
                            Id = e.Bed.BedType.Id,
                            Name = e.Bed.BedType.Name,
                            Description = e.Bed.BedType.Description
                        },
                        Room = new GetRoomDto
                        {
                            Id = e.Bed.Room.Id,
                            HasTv = e.Bed.Room.HasTv,
                            Ward = new GetWardDto
                            {
                                Id = e.Bed.Room.Ward.Id,
                                Name = e.Bed.Room.Ward.Name,
                                Description = e.Bed.Room.Ward.Description
                            }
                        }
                    }
                }).ToList()
            }).ToListAsync();

        return result;
    }

    public async Task AddBedAssignmentAsync(string pesel, addBedAssignmentDto addBedAssignmentDto)
    {
        var anyPatient = await _context.Patients.AnyAsync(e => e.Pesel == pesel);
        if (!anyPatient) throw new NotFoundException("Nie znaleziono Pacjenta o takim Peselu");

        var bed = await _context.Beds
            .Where(b => b.BedType.Name == addBedAssignmentDto.BedType
                        && b.Room.Ward.Name == addBedAssignmentDto.Ward
                        && !b.BedAssignments.Any(ba =>
                            (addBedAssignmentDto.To == null || ba.From < addBedAssignmentDto.To)
                            && (ba.To == null || ba.To > addBedAssignmentDto.From)))
            .FirstOrDefaultAsync();

        if (bed == null) throw new NotFoundException("Nie znaleziono łóżka spełniającego warunki.");

        var assignment = new BedAssignment
        {
            PatientPesel = pesel,
            BedId = bed.Id,
            From = addBedAssignmentDto.From,
            To = addBedAssignmentDto.To
        };

        await _context.BedAssignments.AddAsync(assignment);
        await _context.SaveChangesAsync();
    }
}