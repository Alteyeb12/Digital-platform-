using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreatIntelligence.Application.DTOs;
using ThreatIntelligence.Domain.Entities;
using ThreatIntelligence.Domain.Repositories;

namespace ThreatIntelligence.Application.Services
{
    /// <summary>
    /// تطبيق خدمة إدارة المنظمات
    /// Organization Management Service Implementation
    /// </summary>
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrganizationResponseDto> CreateOrganizationAsync(CreateOrganizationDto dto)
        {
            var organization = new Organization
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                City = dto.City,
                Country = dto.Country,
                Website = dto.Website,
                SubscriptionPlan = dto.SubscriptionPlan,
                Status = OrganizationStatus.TrialMode,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SubscriptionExpiresAt = DateTime.UtcNow.AddDays(14), // Trial for 14 days
                EnableMfa = true,
                EnableAuditLog = true,
                AlertRetentionDays = 90,
                AutomaticallyExecuteActions = false,
                MaxEmails = GetMaxEmails(dto.SubscriptionPlan),
                MaxDomains = GetMaxDomains(dto.SubscriptionPlan),
                MaxApiKeys = GetMaxApiKeys(dto.SubscriptionPlan),
                MaxUsers = GetMaxUsers(dto.SubscriptionPlan)
            };

            await _unitOfWork.Organizations.AddAsync(organization);
            return MapToResponseDto(organization);
        }

        public async Task<OrganizationResponseDto> GetOrganizationAsync(Guid organizationId)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(organizationId);
            if (organization == null)
                throw new Exception("المنظمة غير موجودة / Organization not found");
            return MapToResponseDto(organization);
        }

        public async Task<IEnumerable<OrganizationResponseDto>> GetAllOrganizationsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var (organizations, _) = await _unitOfWork.Organizations.GetPagedAsync(pageNumber, pageSize);
            return organizations.Select(MapToResponseDto).ToList();
        }

        public async Task<OrganizationResponseDto> UpdateOrganizationAsync(UpdateOrganizationDto dto)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(dto.Id);
            if (organization == null)
                throw new Exception("المنظمة غير موجودة / Organization not found");

            organization.Name = dto.Name ?? organization.Name;
            organization.Email = dto.Email ?? organization.Email;
            organization.Phone = dto.Phone ?? organization.Phone;
            organization.Website = dto.Website ?? organization.Website;
            organization.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Organizations.UpdateAsync(organization);
            return MapToResponseDto(organization);
        }

        public async Task<bool> DeleteOrganizationAsync(Guid organizationId)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(organizationId);
            if (organization == null)
                return false;

            await _unitOfWork.Organizations.DeleteAsync(organization);
            return true;
        }

        public async Task<bool> SuspendOrganizationAsync(Guid organizationId)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(organizationId);
            if (organization == null)
                return false;

            organization.Status = OrganizationStatus.Suspended;
            organization.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Organizations.UpdateAsync(organization);
            return true;
        }

        public async Task<bool> ResumeOrganizationAsync(Guid organizationId)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(organizationId);
            if (organization == null)
                return false;

            organization.Status = OrganizationStatus.Active;
            organization.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Organizations.UpdateAsync(organization);
            return true;
        }

        public async Task<bool> UpgradeSubscriptionAsync(Guid organizationId, string newPlan)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(organizationId);
            if (organization == null)
                return false;

            if (Enum.TryParse<SubscriptionPlan>(newPlan, out var plan))
            {
                organization.SubscriptionPlan = plan;
                organization.MaxEmails = GetMaxEmails(plan);
                organization.MaxDomains = GetMaxDomains(plan);
                organization.MaxApiKeys = GetMaxApiKeys(plan);
                organization.MaxUsers = GetMaxUsers(plan);
                organization.UpdatedAt = DateTime.UtcNow;
                organization.NextBillingDate = DateTime.UtcNow.AddMonths(1);
                await _unitOfWork.Organizations.UpdateAsync(organization);
                return true;
            }
            return false;
        }

        private OrganizationResponseDto MapToResponseDto(Organization organization)
        {
            return new OrganizationResponseDto
            {
                Id = organization.Id,
                Name = organization.Name,
                Email = organization.Email,
                Website = organization.Website,
                SubscriptionPlan = organization.SubscriptionPlan,
                Status = organization.Status,
                CurrentEmailCount = organization.CurrentEmailCount,
                MaxEmails = organization.MaxEmails,
                CurrentDomainCount = organization.CurrentDomainCount,
                MaxDomains = organization.MaxDomains,
                SubscriptionExpiresAt = organization.SubscriptionExpiresAt
            };
        }

        private int GetMaxEmails(SubscriptionPlan plan) => plan switch
        {
            SubscriptionPlan.Starter => 10,
            SubscriptionPlan.Professional => 50,
            SubscriptionPlan.Enterprise => 1000,
            _ => 10
        };

        private int GetMaxDomains(SubscriptionPlan plan) => plan switch
        {
            SubscriptionPlan.Starter => 2,
            SubscriptionPlan.Professional => 5,
            SubscriptionPlan.Enterprise => 50,
            _ => 2
        };

        private int GetMaxApiKeys(SubscriptionPlan plan) => plan switch
        {
            SubscriptionPlan.Starter => 5,
            SubscriptionPlan.Professional => 20,
            SubscriptionPlan.Enterprise => 100,
            _ => 5
        };

        private int GetMaxUsers(SubscriptionPlan plan) => plan switch
        {
            SubscriptionPlan.Starter => 3,
            SubscriptionPlan.Professional => 10,
            SubscriptionPlan.Enterprise => 100,
            _ => 3
        };
    }
}
