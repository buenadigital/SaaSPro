using System;
using System.Collections.Generic;
using SaaSPro.Domain;

namespace SaaSPro.Services.Interfaces
{
    public interface IAuditEntryService
    {
        IEnumerable<AuditEntry> GetEntries(string entityType, Guid entityId, Customer customer);
    }
}