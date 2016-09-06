using System;
using System.Collections.Generic;
using System.Linq;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Common;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using AutoMapper;

namespace SaaSPro.Services.Implementations
{
    public class ReferenceListItemService : IReferenceListItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReferenceListRepository _referenceListRepository;

        public ReferenceListItemService(IUnitOfWork unitOfWork, IReferenceListRepository referenceListRepository)
        {
            _unitOfWork = unitOfWork;
            _referenceListRepository = referenceListRepository;
        }


        public ReferenceListsDetailsModel ReferenceDetails(Guid id, Guid customerId)
        {
            var referenceList = _referenceListRepository.Get(id);

            ReferenceListsDetailsModel model = Mapper.Map<ReferenceListsDetailsModel>(referenceList);
            model.Items = referenceList.Items.Where(i => i.CustomerId == customerId).Select(i => Mapper.Map<ReferenceListsDetailsModel.ReferenceListItem>(i));

            model.AddItemModel = new ReferenceListsAddItemModel { ReferenceListId = referenceList.Id };

            return model;
        }

        public void AddItem(Guid id, ReferenceListsAddItemModel model, Customer customer)
        {
            var referenceList = _referenceListRepository.Get(id);
            referenceList.AddItem(customer, model.Value);

            _unitOfWork.Commit();
        }

        public bool RemoveItem(Guid id, Guid itemId)
        {
            var referenceList = _referenceListRepository.Get(id);

            var result = referenceList.RemoveItem(itemId);
            _unitOfWork.Commit();

            return result;
        }

        ReferenceListsModel IReferenceListItemService.ReferenceLists()
        {
            throw new NotImplementedException();
        }

        ReferenceListsDetailsModel IReferenceListItemService.ReferenceDetails(Guid id, Guid customerId)
        {
            throw new NotImplementedException();
        }

        void IReferenceListItemService.AddItem(Guid id, ReferenceListsAddItemModel model, Customer customer)
        {
            throw new NotImplementedException();
        }

        bool IReferenceListItemService.RemoveItem(Guid id, Guid itemId)
        {
            throw new NotImplementedException();
        }
    }
}

