using System;
using System.Collections.Generic;

namespace SaaSPro.Services.ViewModels
{
    public class ApiTokensListModel
    {
        public IEnumerable<ApiTokenSummary> Tokens { get; set; } 

        public class ApiTokenSummary
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Token { get; set; }
        }
    }
}