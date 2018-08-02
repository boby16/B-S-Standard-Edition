using LoyalFilial.Framework.Data.Table;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using LoyalFilial.Framework.Core.Data;
using LoyalFilial.Framework.Data.DataMap.Core;

namespace ResetteStone.Framework.Test
{
    
    
    /// <summary>
    ///This is a test class for TableQueryTest and is intended
    ///to contain all TableQueryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TableQueryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Select
        ///</summary>
        public void SelectTestHelper<T>()
            where T : class
        {
            var entity = LoyalFilial.Framework.Core.LFFK.DataManager.TableQuery<CqsEntity>().Select().From().Where(t => t.Age > 15).Execute();

            //TableQuery<T> target = new TableQuery<T>(); // TODO: Initialize to an appropriate value
            //Expression<Func<T, object>>[] columnNameFileterExps = null; // TODO: Initialize to an appropriate value
            //ITableQueryFrom<T> expected = null; // TODO: Initialize to an appropriate value
            //ITableQueryFrom<T> actual;
            //actual = target.Select(columnNameFileterExps);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void SelectTest()
        {
            SelectTestHelper<GenericParameterHelper>();
        }


        [Table("biz_dev", "cqs_test")]
        public class CqsEntity
        {
            [Column("idx")]
            public int No { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public int Age { get; set; }

            public DateTime BoardTime { get; set; }

            public string OP { get; set; }

            public DateTime UpdateTime { get; set; }
        }
    }
}
