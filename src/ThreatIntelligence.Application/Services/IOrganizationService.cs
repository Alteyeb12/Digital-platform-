using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreatIntelligence.Application.DTOs;

namespace ThreatIntelligence.Application.Services
{
    /// <summary>
    /// خدمة إدارة المنظمات
    /// Organization Management Service
    /// </summary>
    public interface IOrganizationService
    {
        Task<OrganizationResponseDto> CreateOrganizationAsync(CreateOrganizationDto dto);
        Task<OrganizationResponseDto> GetOrganizationAsync(Guid organizationId);
        Task<IEnumerable<OrganizationResponseDto>> GetAllOrganizationsAsync(int pageNumber = 1, int pageSize = 10);
        Task<OrganizationResponseDto> UpdateOrganizationAsync(UpdateOrganizationDto dto);
        Task<bool> DeleteOrganizationAsync(Guid organizationId);
        Task<bool> SuspendOrganizationAsync(Guid organizationId);
        Task<bool> ResumeOrganizationAsync(Guid organizationId);
        Task<bool> UpgradeSubscriptionAsync(Guid organizationId, string newPlan);
    }
}
