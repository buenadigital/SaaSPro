using System;
using System.Collections.Generic;

namespace SaaSPro.Services.ViewModels
{
    public class ReferenceListsDetailsModel
    {
        public Guid Id { get; set; }
        public string SystemName { get; set; }
        public IEnumerable<ReferenceListItem> Items { get; set; }
        public ReferenceListsAddItemModel AddItemModel { get; set; }

        public class ReferenceListItem
        {
            public Guid Id { get; set; }
            public string Value { get; set; }
        }
    }
}