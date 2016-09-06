using System;
using System.Linq;
using System.Collections.Generic;
using SaaSPro.Common;
using SaaSPro.Services.Interfaces;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;

namespace SaaSPro.Services.Implementations
{
    public class AuditEntryService : IAuditEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditEntryRepository _auditEntryRepository;


        public AuditEntryService(IUnitOfWork unitOfWork, IAuditEntryRepository auditEntryRepository)
        {
            _unitOfWork = unitOfWork;
            _auditEntryRepository = auditEntryRepository;
        }

        public IEnumerable<AuditEntry> GetEntries(string entityType, Guid entityId, Customer customer)
        {
            return _auditEntryRepository.Fetch(q =>
                q.Where(x => x.EntityId == entityId && x.EntityType == entityType && x.CustomerId == customer.Id)
                 .OrderByDescending(x => x.TimeStamp));
        }
    }
}
