﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;

namespace ArkNet.Service.Account.Tests
{
    [TestClass()]
    public class AccountServiceTests : AccountServiceTestsBase
    {
        [TestInitialize]
        public void Init()
        {
            base.InitializeAccountServiceTest();
        }

        [TestMethod()]
        public void GetByAddressTest()
        {
            var account = ArkNetApi.AccountService.GetByAddress(_address);

            GetByAddressResultTest(account);
        }

        [TestMethod()]
        public void GetByAddressErrorTest()
        {
            var account = ArkNetApi.AccountService.GetByAddress("BadAddress");

            GetByAddressErrorResultTest(account);
        }

        [TestMethod()]
        public void GetBalanceTest()
        {
            var res = ArkNetApi.AccountService.GetBalance(_address);

            GetBalanceResultTest(res);
        }

        [TestMethod()]
        public void GetBalanceErrorTest()
        {
            var res = ArkNetApi.AccountService.GetBalance("BadAddress");

            GetBalanceErrorResultTest(res);
        }

        [TestMethod()]
        public void GetDelegatesTest()
        {
            var delegates = ArkNetApi.AccountService.GetDelegates(_address);

            GetDelegatesResultTest(delegates);
        }

        [TestMethod()]
        public void GetDelegatesErrorTest()
        {
            var delegates = ArkNetApi.AccountService.GetDelegates("BadAddress");

            GetDelegatesErrorResultTest(delegates);
        }

        [TestMethod()]
        public void GetTopTest()
        {
            var top = ArkNetApi.AccountService.GetTop(null, null);

            GetTopResultTest(top);
        }

        [TestMethod()]
        public void GetTopLimitTest()
        {
            var top = ArkNetApi.AccountService.GetTop(10, null);

            GetTopLimitResultTest(top);
        }

        [TestMethod()]
        public void GetTopRecordsToSkipTest()
        {
            var top = ArkNetApi.AccountService.GetTop(null, 50);

            GetTopRecordsToSkipResultTest(top);
        }

        [TestMethod()]
        public void GetTopLimitAndRecordsToSkipTest()
        {
            var top = ArkNetApi.AccountService.GetTop(10, 50);

            GetTopLimitAndRecordsToSkipResultTest(top);
        }

        [TestMethod()]
        public void GetTopLimitErrorTest()
        {
            var top = ArkNetApi.AccountService.GetTop(1000, null);

            GetTopLimitErrorResultTest(top);
        }
    }
}