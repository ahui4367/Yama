using System;
using System.Linq;
using Data.Access;
using Data.Access.Impl;
using DbModel.AspnetDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebSite.Tests
{
    [TestClass]
    public class DBUnitTest
    {
        [TestMethod]
        public void UserCRUDTest()
        {
            #region Init
            RepositoryFactory factory = new RepositoryFactoryImpl();
            var repo = factory.Create<Organ_T>();
            #endregion

            #region add test
            repo.Add(new Organ_T()
            {
                Oid = 1,
                Orglevel = 1,
                Orgname = "Test",
                Parentid = -1,
                Created = DateTime.Now,
                Lastmodified = DateTime.Now,
                Active = true
            });
            #endregion

            #region load and test
            var row = repo.GetFiltered(r => r.Oid == 1).FirstOrDefault();

            Assert.IsNotNull(row);
            Assert.AreEqual(1, row.Oid);
            Assert.AreEqual(1, row.Orglevel);
            Assert.AreEqual("Test", row.Orgname);
            Assert.AreEqual(-1, row.Parentid);
            Assert.AreEqual(true, row.Active);
            #endregion

            #region delete test and clear data.
            repo.Remove(new Organ_T() 
            { 
                Oid = 1
            });
            #endregion
        }
    }
}
