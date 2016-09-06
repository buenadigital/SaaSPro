using System;

namespace SaaSPro.Web.Management.ViewModels
{
    public class DeleteConfirmationModel
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
    }
}