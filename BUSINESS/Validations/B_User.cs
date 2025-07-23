using DOMAIN.ENTITIES;
using INFRASTRUCTURE;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class B_User
{
    private readonly D_User _userData;

    public B_User(D_User userData)
    {
        _userData = userData ?? throw new ArgumentNullException(nameof(userData));
    }

    public async Task<int> Create(E_User user)
    {
        try
        {
            ValidateUserForCreation(user);
            await CheckForExistingUsers(user.Email, user.Nickname);
            return await _userData.Create(user);
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseOperationException("Failed to create user in database", ex);
        }
        catch (Exception ex)
        {
            throw new UserOperationException("Unexpected error while creating user", ex);
        }
    }

    public async Task<bool> Update(E_User user)
    {
        try
        {
            ValidateUserForUpdate(user);
            await VerifyUserExists(user.UserID);
            await CheckForDuplicateCredentials(user);
            return await _userData.Update(user);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DatabaseOperationException("Concurrency conflict while updating user", ex);
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseOperationException("Failed to update user in database", ex);
        }
        catch (Exception ex)
        {
            throw new UserOperationException("Unexpected error while updating user", ex);
        }
    }

    public async Task<bool> Delete(int userId)
    {
        try
        {
            if (userId <= 0) throw new ArgumentException("Invalid user ID");
            return await _userData.Delete(userId);
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseOperationException("Failed to delete user from database", ex);
        }
        catch (Exception ex)
        {
            throw new UserOperationException("Unexpected error while deleting user", ex);
        }
    }

    public async Task<IEnumerable<E_User>> GetAll()
    {
        try
        {
            var users = await _userData.GetAll();
            if (!users.Any()) throw new NotFoundException("No users found in database");
            return users;
        }
        catch (Exception ex)
        {
            throw new UserOperationException("Failed to retrieve users", ex);
        }
    }

    public async Task<E_User> GetById(int userId)
    {
        try
        {
            if (userId <= 0) throw new ArgumentException("Invalid user ID");
            var user = await _userData.GetById(userId);
            if (user == null) throw new NotFoundException($"User with ID {userId} not found");
            return user;
        }
        catch (Exception ex)
        {
            throw new UserOperationException("Failed to retrieve user by ID", ex);
        }
    }

    public async Task<E_User> GetByEmail(string email)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty");
            var user = await _userData.GetByEmail(email);
            if (user == null) throw new NotFoundException($"User with email {email} not found");
            return user;
        }
        catch (Exception ex)
        {
            throw new UserOperationException("Failed to retrieve user by email", ex);
        }
    }

    public async Task<E_User> GetByNickname(string nickname)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(nickname)) throw new ArgumentException("Nickname cannot be empty");
            var user = await _userData.GetByNickname(nickname);
            if (user == null) throw new NotFoundException($"User with nickname {nickname} not found");
            return user;
        }
        catch (Exception ex)
        {
            throw new UserOperationException("Failed to retrieve user by nickname", ex);
        }
    }

    public async Task<IEnumerable<E_User>> Search(string keyword)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(keyword)) throw new ArgumentException("Search term cannot be empty");
            if (keyword.Length < 3) throw new ArgumentException("Search term must be at least 3 characters");

            var results = await _userData.Search(keyword);
            if (!results.Any()) throw new NotFoundException("No users match the search criteria");
            return results;
        }
        catch (Exception ex)
        {
            throw new UserOperationException("Failed to search users", ex);
        }
    }

    private void ValidateUserForCreation(E_User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrWhiteSpace(user.Email)) throw new ArgumentException("Email is required");
        if (string.IsNullOrWhiteSpace(user.Nickname)) throw new ArgumentException("Nickname is required");
        if (string.IsNullOrWhiteSpace(user.PasswordHash)) throw new ArgumentException("Password is required");
    }

    private void ValidateUserForUpdate(E_User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (user.UserID <= 0) throw new ArgumentException("Invalid user ID");
        if (string.IsNullOrWhiteSpace(user.Email)) throw new ArgumentException("Email is required");
        if (string.IsNullOrWhiteSpace(user.Nickname)) throw new ArgumentException("Nickname is required");
    }

    private async Task VerifyUserExists(int userId)
    {
        var existingUser = await _userData.GetById(userId);
        if (existingUser == null) throw new NotFoundException("User not found");
    }

    private async Task CheckForExistingUsers(string email, string nickname)
    {
        if (await _userData.GetByEmail(email) != null)
            throw new DuplicateException("Email already registered");

        if (await _userData.GetByNickname(nickname) != null)
            throw new DuplicateException("Nickname already in use");
    }

    private async Task CheckForDuplicateCredentials(E_User user)
    {
        var emailUser = await _userData.GetByEmail(user.Email);
        if (emailUser != null && emailUser.UserID != user.UserID)
            throw new DuplicateException("Email already in use by another user");

        var nicknameUser = await _userData.GetByNickname(user.Nickname);
        if (nicknameUser != null && nicknameUser.UserID != user.UserID)
            throw new DuplicateException("Nickname already in use by another user");
    }
}

// Custom exceptions for better error handling
public class DatabaseOperationException : Exception
{
    public DatabaseOperationException(string message, Exception inner) : base(message, inner) { }
}

public class UserOperationException : Exception
{
    public UserOperationException(string message, Exception inner) : base(message, inner) { }
}

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class DuplicateException : Exception
{
    public DuplicateException(string message) : base(message) { }
}