﻿//-----------------------------------------------------------------------
// <copyright file="MemoryJobStore.cs" company="Tasty Codes">
//     Copyright (c) 2010 Tasty Codes.
// </copyright>
//-----------------------------------------------------------------------

namespace Tasty.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tasty.Configuration;

    /// <summary>
    /// Implements <see cref="IJobStore"/> as a transient, in-memory job store.
    /// </summary>
    public class MemoryJobStore : JobStore
    {
        private List<JobRecord> committed = new List<JobRecord>();
        
        /// <summary>
        /// Deletes a job by ID.
        /// </summary>
        /// <param name="id">The ID of the job to delete.</param>
        /// <param name="transaction">The transaction to execute the command in.</param>
        public override void DeleteJob(int id, IJobStoreTransaction transaction)
        {
            lock (this.committed)
            {
                if (transaction != null)
                {
                    transaction.AddForDelete(id);
                }
                else
                {
                    this.committed.RemoveAll(r => r.Id.Value == id);
                }
            }
        }

        /// <summary>
        /// Gets a job by ID.
        /// </summary>
        /// <param name="id">The ID of the job to get.</param>
        /// <param name="transaction">The transaction to execute the command in.</param>
        /// <returns>The job with the given ID.</returns>
        public override JobRecord GetJob(int id, IJobStoreTransaction transaction)
        {
            lock (this.committed)
            {
                return (from r in this.committed
                        where r.Id.Value == id
                        select new JobRecord(r)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets a collection of jobs that match the given collection of IDs.
        /// </summary>
        /// <param name="ids">The IDs of the jobs to get.</param>
        /// <param name="transaction">The transaction to execute the command in.</param>
        /// <returns>A collection of jobs.</returns>
        public override IEnumerable<JobRecord> GetJobs(IEnumerable<int> ids, IJobStoreTransaction transaction)
        {
            lock (this.committed)
            {
                if (ids != null && ids.Count() > 0)
                {
                    return (from r in this.committed
                            join i in ids on r.Id.Value equals i
                            orderby r.QueueDate
                            select new JobRecord(r)).ToArray();
                }

                return new JobRecord[0];
            }
        }

        /// <summary>
        /// Gets a collection of jobs with the given status, returning
        /// at most the number of jobs identified by <paramref name="count"/>.
        /// </summary>
        /// <param name="status">The status of the jobs to get.</param>
        /// <param name="count">The maximum number of jobs to get.</param>
        /// <param name="transaction">The transaction to execute the command in.</param>
        /// <returns>A collection of jobs.</returns>
        public override IEnumerable<JobRecord> GetJobs(JobStatus status, int count, IJobStoreTransaction transaction)
        {
            lock (this.committed)
            {
                var query = from r in this.committed
                            where r.Status == status
                            orderby r.QueueDate
                            select new JobRecord(r);

                if (count > 0)
                {
                    return query.Take(count).ToArray();
                }

                return query.ToArray();
            }
        }

        /// <summary>
        /// Gets a collection of the most recently scheduled persisted job for each
        /// scheduled job in the configuration.
        /// </summary>
        /// <param name="transaction">The transaction to execute the command in.</param>
        /// <returns>A collection of recently scheduled jobs.</returns>
        public override IEnumerable<JobRecord> GetLatestScheduledJobs(IJobStoreTransaction transaction)
        {
            lock (this.committed)
            {
                return (from r in this.committed
                        group r by new { r.JobType, r.ScheduleName } into g
                        select new JobRecord(g.OrderByDescending(gr => gr.QueueDate).First())).ToArray();
            }
        }

        /// <summary>
        /// Saves the given job record, either creating it or updating it.
        /// </summary>
        /// <param name="record">The job to save.</param>
        /// <param name="transaction">The transaction to execute the command in.</param>
        public override void SaveJob(JobRecord record, IJobStoreTransaction transaction)
        {
            lock (this.committed)
            {
                if (record.Id == null)
                {
                    record.Id = GetNewId();
                }

                if (transaction != null)
                {
                    transaction.AddForSave(record);
                }
                else
                {
                    this.committed.RemoveAll(r => r.Id.Value == record.Id.Value);
                    this.committed.Add(record);
                }
            }
        }

        /// <summary>
        /// Starts a transaction.
        /// </summary>
        public override IJobStoreTransaction StartTransaction()
        {
            return new MemoryJobStoreTransaction(this);
        }

        /// <summary>
        /// Gets a new, unique ID.
        /// </summary>
        /// <returns>A new ID.</returns>
        private static int GetNewId()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }
    }
}
