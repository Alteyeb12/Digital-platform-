using System;
using ThreatIntelligence.Domain.Entities;

namespace ThreatIntelligence.Application.DTOs
{
    public class CreateOrganizationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }

    public class UpdateOrganizationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }

    public class OrganizationResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
        public OrganizationStatus Status { get; set; }
        public int CurrentEmailCount { get; set; }
        public int MaxEmails { get; set; }
        public int CurrentDomainCount { get; set; }
        public int MaxDomains { get; set; }
        public DateTime? SubscriptionExpiresAt { get; set; }
    }
}
