using DOMAIN.ENTITIES;
using INFRASTRUCTURE;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class D_User
{
    private readonly AppDbContext _database;

    public D_User(AppDbContext context)
    {
        _database = context;
    }

    public async Task<int> Create(E_User user)
    {
        _database.Users.Add(user);
        await _database.SaveChangesAsync();
        return user.UserID;
    }

    public async Task<bool> Update(E_User user)
    {
        _database.Entry(user).State = EntityState.Modified;
        return await _database.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int userId)
    {
        var user = await _database.Users.FindAsync(userId);
        if (user == null) return false;

        _database.Users.Remove(user);
        return await _database.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<E_User>> GetAll()
    {
        return await _database.Users.AsNoTracking().ToListAsync();
    }

    public async Task<E_User?> GetById(int userId)
    {
        return await _database.Users.FindAsync(userId);
    }

    public async Task<E_User?> GetByEmail(string email)
    {
        return await _database.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<E_User?> GetByNickname(string nickname)
    {
        return await _database.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Nickname == nickname);
    }

    public async Task<IEnumerable<E_User>> Search(string keyword)
    {
        return await _database.Users
            .AsNoTracking()
            .Where(u => EF.Functions.Like(u.FirstName, $"%{keyword}%") ||
                        EF.Functions.Like(u.PaternalSurname, $"%{keyword}%") ||
                        EF.Functions.Like(u.Email, $"%{keyword}%") ||
                        EF.Functions.Like(u.Nickname, $"%{keyword}%"))
            .ToListAsync();
    }
}