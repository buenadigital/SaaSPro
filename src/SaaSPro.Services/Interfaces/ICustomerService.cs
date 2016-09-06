using System;
using SaaSPro.Services.Messaging.CustomerService;
using SaaSPro.Services.ViewModels;
using System.Linq;
using SaaSPro.Domain;

namespace SaaSPro.Services.Interfaces
{
    public interface ICustomerService
    {
        CustomersDetailsModel GetCustomerDetails(Guid id);
        void Save(CustomersDetailsModel model);
        Guid ResetPassword(Guid id, UsersResetPasswordModel model);
        void Delete(Guid id);
        CustomersListModel List(PagingCommand command, Func<IQueryable<Customer>, IQueryable<Customer>> sortOrderQuery);
        Guid Provision(CustomersProvisionModel model);
        CustomerPaymentsModel Payments(Guid id);
        SetupStripeResponse SetupStripe(Guid customerId);
        string ClosePlan(Guid customerId);
        RefundModel GetRefundPayment(Guid customerId, string paymentId);
        string CheckUserName(string input);
        SetupPaymentPlanReponse SetupPaymentPlan(PlanSetupModel planModel);
        NotesViewModel Notes(Guid customerId, PagingCommand command);
        void AddNote(NotesViewModel.Note note);
        void DeleteNote(Guid id);
    }
}