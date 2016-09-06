using System;
using System.Collections.Generic;

namespace SaaSPro.Services.ViewModels
{
    public class ReferenceListItemsModel
    {
        public IEnumerable<ReferenceListsItem> Item { get; set; }

        public class ReferenceListsItem
        {
            public Guid Id { get; set; }
            public string SystemName { get; set; }
        }
    }
}
