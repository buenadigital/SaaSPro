using System;
using System.Collections.Generic;

namespace SaaSPro.Services.ViewModels
{
    public class ReferenceListsModel
    {
        public IEnumerable<ReferenceList> Lists { get; set; }

        public class ReferenceList
        {
            public Guid Id { get; set; }
            public string SystemName { get; set; }
        }
    }
}