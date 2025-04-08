using Lab5TestTask.Data;
using Lab5TestTask.Enums;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// SessionService implementation.
/// Implement methods here.
/// </summary>
public class SessionService : ISessionService
{
    private readonly ApplicationDbContext _dbContext;

    public SessionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Session> GetSessionAsync()
    {
        return await _dbContext.Sessions
          .AsNoTracking()
          .Where(session => session.DeviceType == DeviceType.Desktop)
          .OrderBy(item => item.StartedAtUTC)
          .FirstOrDefaultAsync();
    }

    public async Task<List<Session>> GetSessionsAsync()
    {
          return await _dbContext.Sessions
        .AsNoTracking()
        .Where(s => s.User.Status == UserStatus.Active && s.EndedAtUTC.Year < 2025)
        .ToListAsync();
    }
}
