﻿

namespace Tasty.Web
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a commit in a GitHub webhook.
    /// </summary>
    [DataContract]
    public class GitHubWebhookCommit
    {
        private string[] added, modified, removed;
        private string id, message, timestamp, url;
        private GitHubWebhookPerson author;
        private DateTime? timestampParsed;

        /// <summary>
        /// Gets or sets the collection of added paths.
        /// </summary>
        [DataMember(Name = "added")]
        public string[] Added
        {
            get { return this.added ?? (this.added = new string[0]); }
            set { this.added = value; }
        }

        /// <summary>
        /// Gets or sets the commit author.
        /// </summary>
        [DataMember(Name = "author")]
        public GitHubWebhookPerson Author
        {
            get { return this.author ?? (this.author = new GitHubWebhookPerson()); }
            set { this.author = value; }
        }

        /// <summary>
        /// Gets or sets the commit ID.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id
        {
            get { return this.id ?? String.Empty; }
            set { this.id = value; }
        }

        /// <summary>
        /// Gets or sets the commit message.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message
        {
            get { return this.message ?? String.Empty; }
            set { this.message = value; }
        }

        /// <summary>
        /// Gets or sets the collection of modified paths.
        /// </summary>
        [DataMember(Name = "modified")]
        public string[] Modified
        {
            get { return this.modified ?? (this.modified = new string[0]); }
            set { this.modified = value; }
        }

        /// <summary>
        /// Gets or sets the collection of removed paths.
        /// </summary>
        [DataMember(Name = "removed")]
        public string[] Removed
        {
            get { return this.removed ?? (this.removed = new string[0]); }
            set { this.removed = value; }
        }

        /// <summary>
        /// Gets or sets the commit timestamp.
        /// </summary>
        [DataMember(Name = "timestamp")]
        public string Timestamp
        {
            get 
            { 
                return this.timestamp ?? String.Empty; 
            }
            set
            {
                this.timestamp = value;
                this.timestampParsed = null;
            }
        }

        /// <summary>
        /// Gets the value of <see cref="Timestamp"/> parsed as a <see cref="DateTime"/>.
        /// </summary>
        public DateTime TimestampParsed
        {
            get
            {
                if (this.timestampParsed == null)
                {
                    if (!String.IsNullOrEmpty(this.Timestamp))
                    {
                        this.timestampParsed = Convert.ToDateTime(this.Timestamp, CultureInfo.InvariantCulture);
                    }
                }

                return this.timestampParsed ?? DateTime.MinValue;
            }
        }

        /// <summary>
        /// Gets or set the commit URL.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url
        {
            get { return this.url ?? String.Empty; }
            set { this.url = value; }
        }
    }
}
