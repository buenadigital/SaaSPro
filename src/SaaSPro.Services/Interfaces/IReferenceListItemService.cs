using System;
using SaaSPro.Services.ViewModels;
using SaaSPro.Domain;

namespace SaaSPro.Services.Interfaces
{
    public interface IReferenceListItemService
    {
        ReferenceListsModel ReferenceLists();
        ReferenceListsDetailsModel ReferenceDetails(Guid id, Guid customerId);
        void AddItem(Guid id, ReferenceListsAddItemModel model, Customer customer);
        bool RemoveItem(Guid id, Guid itemId);
    }
}