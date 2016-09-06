using System;
using System.Collections.Generic;
using SaaSPro.Services.Messaging.PlanService;
using SaaSPro.Services.ViewModels;
using SaaSPro.Domain;

namespace SaaSPro.Services.Interfaces
{
    public interface IPlanService
    {
        List<Plan> List();
        PlansListModel List(PagingCommand commands);
        void Add(PlanAddModel model);
        Plan Get(Guid id);
        void Update(Plan plan);
        void Delete(Plan plan);
        PricingModel GetPricing();
        bool PlanCodeExist(string plan);
        bool IsHostNameAvailable(string hostName);
        PlanSignUpResponse PlanSignUp(PlanSignUpRequest request);
    }
}