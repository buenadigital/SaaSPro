using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Web.Common.Scheduling
{
    public class QuartzSchedulerClient : ISchedulerClient, IDisposable
    {
        private readonly string schedulerAddress;
        private readonly Lazy<IScheduler> scheduler;

        public QuartzSchedulerClient(string schedulerAddress)
        {
            Ensure.Argument.NotNullOrEmpty(schedulerAddress, nameof(schedulerAddress));
            this.schedulerAddress = schedulerAddress;
            
            scheduler = new Lazy<IScheduler>(() => 
            {
                var factory = new StdSchedulerFactory(GetConfiguration(schedulerAddress));
                return factory.GetScheduler();
            });
        }

        protected IScheduler Scheduler => scheduler.Value;

        public IEnumerable<SchedulerJobSummary> GetJobs(string groupName = null)
        {
            // filter by groupName or get all job groups (customers)
            var jobGroups = groupName.IsNotNullOrEmpty() 
                ? new[] { groupName } 
                : Scheduler.GetJobGroupNames();

            var jobs = new List<SchedulerJobSummary>();

            foreach (var group in jobGroups)
            {
                jobs.AddRange(GetJobsInGroup(group));
            }
            
            return jobs;
        }

        public void PauseJob(string jobName, string groupName)
        {
            Ensure.Argument.NotNullOrEmpty(jobName, "jobName");
            Ensure.Argument.NotNullOrEmpty(groupName, "groupName");

            IJobDetail job;
            if (!TryGetJob(jobName, groupName, out job))
                return;

            Scheduler.PauseJob(job.Key);
        }

        public void ResumeJob(string jobName, string groupName)
        {
            Ensure.Argument.NotNullOrEmpty(jobName, "jobName");
            Ensure.Argument.NotNullOrEmpty(groupName, "groupName");

            IJobDetail job;
            if (!TryGetJob(jobName, groupName, out job))
                return;

            Scheduler.ResumeJob(job.Key);
        }

        public SchedulerJob GetJob(string jobName, string groupName)
        {
            Ensure.Argument.NotNullOrEmpty(jobName, "jobName");
            Ensure.Argument.NotNullOrEmpty(groupName, "groupName");

            IJobDetail job;
            if (!TryGetJob(jobName, groupName, out job))
                return null;

            var trigger = GetDefaultTrigger(job);
            // Assumes we have at least one

            var schedulerJob = new SchedulerJob
            {
                Name = job.Key.Name,
                GroupName = job.Key.Group,
                Properties = job.JobDataMap,
                RepeatInterval = trigger.RepeatInterval,
                JobDescription = job.Description
            };

            return schedulerJob;
        }

        public void UpdateJobProperties(string jobName, string groupName, TimeSpan repeatInterval, IDictionary<string, string> properties)
        {
            Ensure.Argument.NotNullOrEmpty(jobName, "jobName");
            Ensure.Argument.NotNullOrEmpty(groupName, "groupName");

            if (properties == null)
                return;

            IJobDetail job;
            if (!TryGetJob(jobName, groupName, out job))
                return;

            bool jobHasChanged = false;
            foreach (var property in properties)
            {
                object currentValue;
                if (job.JobDataMap.TryGetValue(property.Key, out currentValue) && property.Value != null && property.Value != (string) currentValue)
                {
                    job.JobDataMap.Put(property.Key, property.Value);
                    jobHasChanged = true;
                }
            }

            if (jobHasChanged)
            {
                // Replace existing job details with updated
                Scheduler.AddJob(job, replace: true);
            }

            var currentTrigger = GetDefaultTrigger(job);
            if (!currentTrigger.RepeatInterval.Equals(repeatInterval))
            {
                // repeat interval has changed, reschedule.
                var newTrigger = currentTrigger.GetTriggerBuilder()
                    .WithSimpleSchedule(s => s.WithInterval(repeatInterval).RepeatForever())
                    .Build();

                Scheduler.RescheduleJob(currentTrigger.Key, newTrigger);
            }
        }
        
        public void Dispose()
        {
        }

        private IEnumerable<SchedulerJobSummary> GetJobsInGroup(string groupName)
        {
            foreach (var jobKey in Scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName)))
            {
                var jobDetail = Scheduler.GetJobDetail(jobKey);
                var triggers = Scheduler.GetTriggersOfJob(jobKey);

                foreach (var trigger in triggers)
                {
                    var simpleTrigger = trigger as ISimpleTrigger;

                    yield return new SchedulerJobSummary
                    {
                        GroupName = groupName,
                        JobName = jobKey.Name,
                        JobDescription = jobDetail.Description,
                        JobType = jobDetail.JobType.Name,
                        TriggerName = trigger.Key.Name,
                        TriggerGroupName = trigger.Key.Group,
                        TriggerType = trigger.GetType().Name,
                        TriggerState = Scheduler.GetTriggerState(trigger.Key).ToString(),
                        NextFireTime = ConvertDateTimeOffset(trigger.GetNextFireTimeUtc()),
                        PreviousFireTime = ConvertDateTimeOffset(trigger.GetPreviousFireTimeUtc()),
                        RepeatInterval = simpleTrigger?.RepeatInterval ?? TimeSpan.Zero
                    };
                }
            }
        }

        private bool TryGetJob(string jobName, string groupName, out IJobDetail jobDetail)
        {
            jobDetail = null;
            var jobKey = JobKey.Create(jobName, groupName);

            if (Scheduler.CheckExists(jobKey))
            {
                jobDetail = Scheduler.GetJobDetail(jobKey);
                return true;
            }

            return false;
        }

        private ISimpleTrigger GetDefaultTrigger(IJobDetail job)
        {
            return Scheduler.GetTriggersOfJob(job.Key).OfType<ISimpleTrigger>().FirstOrDefault();
        }

        private static NameValueCollection GetConfiguration(string schedulerAddress)
        {
            var properties = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = "SaaSProClient",
                ["quartz.scheduler.proxy"] = "true",
                ["quartz.threadPool.threadCount"] = "0",
                ["quartz.scheduler.proxy.address"] = schedulerAddress
            };
            return properties;
        }

        private static DateTime? ConvertDateTimeOffset(DateTimeOffset? dateTimeOffset)
        {
            return dateTimeOffset?.UtcDateTime;
        }
    }
}