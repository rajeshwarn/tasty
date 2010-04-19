﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasty.Web.UrlTokens;

namespace Tasty.Test
{
    [TestClass]
    public class PostgresUrlTokenStoreTests
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["Postgres"] != null ? ConfigurationManager.ConnectionStrings["Postgres"].ConnectionString : String.Empty;

        [TestMethod]
        public void PostgresUrlTokenStore_CreateUrlToken()
        {
            if (!String.IsNullOrEmpty(ConnectionString))
            {
                UrlTokenTests.Store_CreateUrlToken(new PostgresUrlTokenStore(ConnectionString));
            }
        }

        [TestMethod]
        public void PostgresUrlTokenStore_ExpireUrlToken()
        {
            if (!String.IsNullOrEmpty(ConnectionString))
            {
                UrlTokenTests.Store_ExpireUrlToken(new PostgresUrlTokenStore(ConnectionString));
            }
        }

        [TestMethod]
        public void PostgresUrlTokenStore_GetUrlToken()
        {
            if (!String.IsNullOrEmpty(ConnectionString))
            {
                UrlTokenTests.Store_GetUrlToken(new PostgresUrlTokenStore(ConnectionString));
            }
        }
    }
}
