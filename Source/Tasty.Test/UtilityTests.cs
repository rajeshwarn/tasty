﻿//-----------------------------------------------------------------------
// <copyright file="UtilityTests.cs" company="Tasty Codes">
//     Copyright (c) 2010 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------

namespace Tasty.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Utility tests.
    /// </summary>
    [TestClass]
    public class UtilityTests
    {
        /// <summary>
        /// Test description enum.
        /// </summary>
        private enum DescriptionEnum
        {
            /// <summary>
            /// Identifies one.
            /// </summary>
            [System.ComponentModel.Description("I")]
            One,

            /// <summary>
            /// Identifies two.
            /// </summary>
            [System.ComponentModel.Description("II")]
            Two,

            /// <summary>
            /// Identifies three.
            /// </summary>
            [System.ComponentModel.Description("III")]
            Three
        }

        /// <summary>
        /// Camel case to lowercase underscore tests.
        /// </summary>
        [TestMethod]
        public void UtilityCamelCaseToLowercaseUnderscore()
        {
            Assert.AreEqual("pascal_case", "PascalCase".ToLowercaseUnderscore());
            Assert.AreEqual("camel_case", "camelCase".ToLowercaseUnderscore());
            Assert.AreEqual("'camel0_case'", "'Camel0Case'".ToLowercaseUnderscore());
            Assert.AreEqual("cc", "CC".ToLowercaseUnderscore());
            Assert.AreEqual("bcc", "Bcc".ToLowercaseUnderscore());
            Assert.AreEqual("bcc", "BCC".ToLowercaseUnderscore()); 
        }

        /// <summary>
        /// Copy properties tests.
        /// </summary>
        [TestMethod]
        public void UtilityCopyProperties()
        {
            var source1 = new CopyPropertiesTest() { Name = Guid.NewGuid().ToString(), Number = 42, Date = DateTime.UtcNow };
            var dest1 = new CopyPropertiesTest() { Name = "some string", Number = 0, Date = DateTime.MinValue };

            source1.CopyProperties(dest1);

            Assert.AreEqual(source1.Name, dest1.Name);
            Assert.AreEqual(source1.Number, dest1.Number);
            Assert.AreEqual(source1.Date, dest1.Date);
        }

        /// <summary>
        /// Enum descriptions tests.
        /// </summary>
        [TestMethod]
        public void UtilityEnumDescriptions()
        {
            Assert.AreEqual("II", DescriptionEnum.Two.ToDescription());
            Assert.AreEqual(DescriptionEnum.Three, "III".EnumFromDescription<DescriptionEnum>());
        }

        /// <summary>
        /// Lowercase underscore to camel case tests.
        /// </summary>
        [TestMethod]
        public void UtilityLowercaseUnderscoreToCamelCase()
        {
            Assert.AreEqual("PascalCase", "pascal_case".FromLowercaseUnderscore());
            Assert.AreEqual("camelCase", "camel_case".FromLowercaseUnderscore(true));
            Assert.AreEqual("'Pascal0Case'", "'pascal0_case'".FromLowercaseUnderscore());
        }

        /// <summary>
        /// To pretty time tests.
        /// </summary>
        [TestMethod]
        public void UtilityToPrettyTime()
        {
            TimeSpan timeSpan = new TimeSpan(3, 6, 27, 38);
            Assert.AreEqual("3d 6h", timeSpan.ToPrettyString());

            timeSpan = new TimeSpan(0, 4, 36, 59, 875);
            Assert.AreEqual("4h 36m", timeSpan.ToPrettyString());

            timeSpan = new TimeSpan(0, 0, 12, 17, 0);
            Assert.AreEqual("12m 17s", timeSpan.ToPrettyString());

            timeSpan = new TimeSpan(0, 0, 0, 2, 850);
            Assert.AreEqual("2.8s", timeSpan.ToPrettyString());
            
            timeSpan = new TimeSpan(0, 0, 0, 0, 300);
            Assert.AreEqual("0.3s", timeSpan.ToPrettyString());
        }

        /// <summary>
        /// Test copy properties class.
        /// </summary>
        private class CopyPropertiesTest
        {
            /// <summary>
            /// Gets or sets the date.
            /// </summary>
            public DateTime Date { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the number.
            /// </summary>
            public int Number { get; set; }
        }
    }
}
