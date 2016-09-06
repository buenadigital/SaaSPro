using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SaaSPro.Common;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Data.Repositories;
using AutoMapper;

namespace SaaSPro.Services.Implementations
{
    public class IPSService : IIPSService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIPSRepository _ipsRepository;

        public IPSService(IUnitOfWork unitOfWork, IIPSRepository ipsRepository)
        {
            _unitOfWork = unitOfWork;
            _ipsRepository = ipsRepository;
        }

        public IPSListModel List(Guid customerId)
        {
            var entries = _ipsRepository.Fetch(q => q.OrderBy(t => t.Name)).Where(i => i.CustomerId == customerId);

            var model = new IPSListModel
            {
                Entries = Mapper.Map<IEnumerable<IPSListModel.IPSEntrySummary>>(entries)
            };

            return model;
        }

        public void Add(IPSAddModel model, Customer customer)
        {
            var entry = new IPSEntry(
                customer,
                model.Name,
                IPAddress.Parse(model.StartIPAddress),
                IPAddress.Parse(model.EndIPAddress));
            _ipsRepository.Add(entry);
            _unitOfWork.Commit();
        }

        public void Delete(Guid id)
        {
            IPSEntry entry = _ipsRepository.Get(id);

            _ipsRepository.Delete(entry);

            _unitOfWork.Commit();
        }
    }
}
