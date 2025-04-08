using Lab5TestTask.Data;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// UserService implementation.
/// Implement methods here.
/// </summary>
public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> GetUserAsync()
    {
        return await _dbContext.Users
                    .AsNoTracking()
                    .Include(item => item.Sessions)
                    .OrderByDescending(item => item.Sessions.Count)
                    .FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _dbContext.Users
          .AsNoTracking()
         .Include(user => user.Sessions.Where(session => session.DeviceType == Enums.DeviceType.Mobile))
         .ToListAsync();
    }
}
