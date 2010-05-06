﻿//-----------------------------------------------------------------------
// <copyright file="JobsElement.cs" company="Tasty Codes">
//     Copyright (c) 2010 Tasty Codes.
// </copyright>
//-----------------------------------------------------------------------

namespace Tasty.Configuration
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Represents the jobs configuration element.
    /// </summary>
    public class JobsElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the delegate type to use if the job runner is started without a delegate.
        /// </summary>
        [ConfigurationProperty("delegateType", IsRequired = false)]
        public string DelegateType
        {
            get { return (string)this["delegateType"]; }
            set { this["delegateType"] = value; }
        }

        /// <summary>
        /// Gets or sets the heartbeat timeout (in miliseconds) to use for the job runner. The runner will 
        /// pause for this duration at the end of each cancel/finish/timeout/dequeue loop.
        /// When not configured, defaults to 10,000 (10 seconds).
        /// </summary>
        [ConfigurationProperty("heartbeat", IsRequired = false, DefaultValue = 10000)]
        public int Heartbeat
        {
            get { return (int)this["heartbeat"]; }
            set { this["heartbeat"] = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of jobs that are allowed to be
        /// running simultaneously. When not configured, defaults to 4.
        /// </summary>
        [ConfigurationProperty("maximumConcurrency", IsRequired = false, DefaultValue = 4)]
        public int MaximumConcurrency
        {
            get { return (int)this["maximumConcurrency"]; }
            set { this["maximumConcurrency"] = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of retries to perform when a job
        /// fails or is timed out. When not configured, defaults to 0 (no retries).
        /// </summary>
        [ConfigurationProperty("maximumFailedRetries", IsRequired = false)]
        public int MaximumFailedRetries
        {
            get { return (int)this["maximumFailedRetries"]; }
            set { this["maximumFailedRetries"] = value; }
        }

        /// <summary>
        /// Gets the configured collection of scheduled jobs.
        /// </summary>
        [ConfigurationProperty("schedules", IsRequired = false)]
        public JobScheduleElementCollection Schedules
        {
            get { return (JobScheduleElementCollection)(this["schedules"] ?? (this["schedules"] = new JobScheduleElementCollection())); }
        }

        /// <summary>
        /// Gets the configured <see cref="Tasty.Jobs.IJobStore"/> implementation to use for persisting job data.
        /// </summary>
        [ConfigurationProperty("store", IsRequired = false)]
        public JobStoreElement Store
        {
            get { return (JobStoreElement)(this["store"] ?? (this["store"] = new JobStoreElement())); }
        }

        /// <summary>
        /// Gets a value indicating if the System.Configuration.ConfigurationElement object is read-only.
        /// </summary>
        /// <returns>True if the System.Configuration.ConfigurationElement object is read-only, false otherwise.</returns>
        public override bool IsReadOnly()
        {
            return false;
        }
    }
}
