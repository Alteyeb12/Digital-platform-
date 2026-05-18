using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreatIntelligence.Application.DTOs;

namespace ThreatIntelligence.Application.Services
{
    /// <summary>
    /// خدمة إدارة المستخدمين
    /// User Management Service
    /// </summary>
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(Guid organizationId, CreateUserDto dto);
        Task<UserResponseDto> GetUserAsync(Guid userId);
        Task<IEnumerable<UserResponseDto>> GetOrganizationUsersAsync(Guid organizationId, int pageNumber = 1, int pageSize = 10);
        Task<UserResponseDto> UpdateUserAsync(UpdateUserDto dto);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto dto);
        Task<bool> EnableMfaAsync(Guid userId, EnableMfaDto dto);
        Task<bool> VerifyMfaCodeAsync(Guid userId, VerifyMfaCodeDto dto);
        Task<bool> LockUserAsync(Guid userId);
        Task<bool> UnlockUserAsync(Guid userId);
    }
}
