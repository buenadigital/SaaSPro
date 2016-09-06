using SaaSPro.Common.Web;
using SaaSPro.Web.Common.Scheduling;
using SaaSPro.Web.Management.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SaaSPro.Data;

namespace SaaSPro.Web.Management.Controllers
{
    public class SchedulerController : ManagementControllerBase
    {
        private readonly ISchedulerClient _schedulerClient;
        private readonly EFDbContext _dbContext;

        public SchedulerController(ISchedulerClient schedulerClient, EFDbContext dbContext)
        {
            _schedulerClient = schedulerClient;
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            // Get the scheduler jobs
            var jobs = _schedulerClient.GetJobs().OrderBy(j => j.GroupName);
            var customerIds = jobs.Select(j => Guid.Parse(j.GroupName)).ToArray();

            // Map the Customer details
            var customers = _dbContext.Customers
                .Where(t => customerIds.Contains(t.Id)).ToList();

            var jobsWithCustomer = AutoMapper.Mapper.Map<IEnumerable<SchedulerListModel.JobSummary>>(jobs);

            foreach (var job in jobsWithCustomer)
            {
                var customer = customers.FirstOrDefault(t => t.Id == Guid.Parse(job.GroupName));
                if (customer != null)
                {
                    job.CustomerId = customer.Id;
                    job.CustomerName = customer.FullName;
                }
            }

            var model = new SchedulerListModel
            {
                Jobs = jobsWithCustomer
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Pause(string groupName, string jobName)
        {
            _schedulerClient.PauseJob(jobName, groupName);
            return RedirectToAction("index")
                .AndAlert(AlertType.Success, "Job Paused.", $"Job '{jobName}' was paused successfully.");
        }

        [HttpPost]
        public ActionResult Resume(string groupName, string jobName)
        {
            _schedulerClient.ResumeJob(jobName, groupName);
            return RedirectToAction("index")
                .AndAlert(AlertType.Success, "Job Resumed.", $"Job '{jobName}' was resumed successfully.");
        }

        [HttpGet]
        public ActionResult Update(string groupName, string jobName)
        {
            var job = _schedulerClient.GetJob(jobName, groupName);

            if (job == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapper.Mapper.Map<SchedulerUpdateModel>(job);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(SchedulerUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _schedulerClient.UpdateJobProperties(model.Name, model.GroupName, TimeSpan.FromMinutes(model.RepeatInterval), model.Properties);

            return RedirectToAction("update", new {id = model.Name})
                .AndAlert(AlertType.Success, "Job Updated.", $"Job '{model.Name}' was updated successfully.");
        }
    }
}