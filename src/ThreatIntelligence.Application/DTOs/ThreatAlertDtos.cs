using System;
using System.Collections.Generic;
using ThreatIntelligence.Domain.Entities;

namespace ThreatIntelligence.Application.DTOs
{
    public class CreateThreatAlertDto
    {
        public Guid MonitoredAssetId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ThreatType ThreatType { get; set; }
        public ThreatSeverity Severity { get; set; }
        public string Source { get; set; }
        public string SourceUrl { get; set; }
        public string Evidence { get; set; }
    }

    public class ThreatAlertResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ThreatType ThreatType { get; set; }
        public ThreatSeverity Severity { get; set; }
        public AlertStatus Status { get; set; }
        public string Source { get; set; }
        public DateTime DiscoveredAt { get; set; }
        public bool IsNotified { get; set; }
        public List<AlertActionResponseDto> Actions { get; set; }
    }

    public class UpdateThreatAlertStatusDto
    {
        public Guid Id { get; set; }
        public AlertStatus Status { get; set; }
        public string Notes { get; set; }
    }

    public class AlertActionResponseDto
    {
        public Guid Id { get; set; }
        public ActionType ActionType { get; set; }
        public ActionStatus Status { get; set; }
        public DateTime? ExecutedAt { get; set; }
    }
}
