﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionService.cs" company="Ark Labs">
//   MIT License
//   // 
//   // Copyright (c) 2017 Kristjan Košič
//   // 
//   // Permission is hereby granted, free of charge, to any person obtaining a copy
//   // of this software and associated documentation files (the "Software"), to deal
//   // in the Software without restriction, including without limitation the rights
//   // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   // copies of the Software, and to permit persons to whom the Software is
//   // furnished to do so, subject to the following conditions:
//   // 
//   // The above copyright notice and this permission notice shall be included in all
//   // copies or substantial portions of the Software.
//   // 
//   // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//   // SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Messages.Transaction;
using ArkNet.Model.Transactions;
using ArkNet.Utils;
using Newtonsoft.Json;

namespace ArkNet.Service
{
    /// <summary>
    /// Provides functionality for requesting transaction information and creating transactions.
    /// </summary>
    /// 
    public static class TransactionService
    {
        #region Methods

        /// <summary>
        /// Gets a list of all transactions.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="ArkTransactionList"/> type.</returns>
        /// 
        public static ArkTransactionList GetAll()
        {
            return GetAllAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets a list of all transactions.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{ArkTransactionList}"/> type.</returns>
        /// 
        public async static Task<ArkTransactionList> GetAllAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Transaction.GET_ALL);

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        /// <summary>
        /// Gets transactions by range.
        /// </summary>
        /// 
        /// <param name="req">The range paramters.</param>
        /// 
        /// <returns>Returns an <see cref="ArkTransactionList"/> type.</returns>
        /// 
        public static ArkTransactionList GetTransactions(ArkTransactionRequest req)
        {
            return GetTransactionsAsync(req).Result;
        }

        /// <summary>
        /// Asynchronously gets transactions by range.
        /// </summary>
        /// 
        /// <param name="req">The range paramters.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkTransactionList}"/> type.</returns>
        /// 
        public async static Task<ArkTransactionList> GetTransactionsAsync(ArkTransactionRequest req)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Transaction.GET_ALL + "{0}", req.ToQuery()));

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        /// <summary>
        /// Gets a list of all unconfirmed transactions.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="ArkTransactionList"/> type.</returns>
        /// 
        public static ArkTransactionList GetUnconfirmedAll()
        {
            return GetUnconfirmedAllAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets a list of all unconfirmed transactions.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{ArkTransactionList}"/> type.</returns>
        /// 
        public async static Task<ArkTransactionList> GetUnconfirmedAllAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Transaction.GET_ALL_UNCONFIRMED);

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        /// <summary>
        /// Gets unconfirmed transactions by range.
        /// </summary>
        /// 
        /// <param name="req">The range paramters.</param>
        /// 
        /// <returns>Returns an <see cref="ArkTransactionList"/> type.</returns>
        /// 
        public static ArkTransactionList GetUnconfirmedTransactions(ArkUnconfirmedTransactionRequest req)
        {
            return GetUnconfirmedTransactionsAsync(req).Result;
        }

        /// <summary>
        /// Asynchronously gets unconfirmed transactions by range.
        /// </summary>
        /// 
        /// <param name="req">The range paramters.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkTransactionList}"/> type.</returns>
        /// 
        public async static Task<ArkTransactionList> GetUnconfirmedTransactionsAsync(ArkUnconfirmedTransactionRequest req)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Transaction.GET_ALL_UNCONFIRMED + "{0}", req.ToQuery()));

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        /// <summary>
        /// Gets a transaction by id.
        /// </summary>
        /// 
        /// <param name="id">The transaction id.</param>
        /// 
        /// <returns>Returns an <see cref="ArkTransactionResponse"/> type.</returns>
        /// 
        public static ArkTransactionResponse GetById(string id)
        {
            return GetByIdAsync(id).Result;
        }

        /// <summary>
        /// Asynchronously gets a transaction by id.
        /// </summary>
        /// 
        /// <param name="id">The transaction id.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkTransactionResponse}"/> type.</returns>
        /// 
        public async static Task<ArkTransactionResponse> GetByIdAsync(string id)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Transaction.GET_BY_ID, id));

            return JsonConvert.DeserializeObject<ArkTransactionResponse>(response);
        }

        /// <summary>
        /// Gets an unconfirmed transaction by id.
        /// </summary>
        /// 
        /// <param name="id">The transaction id.</param>
        /// 
        /// <returns>Returns an <see cref="ArkTransactionResponse"/> type.</returns>
        /// 
        public static ArkTransactionResponse GetUnConfirmedById(string id)
        {
            return GetUnConfirmedByIdAsync(id).Result;
        }

        /// <summary>
        /// Asynchronously gets an unconfirmed transaction by id.
        /// </summary>
        /// 
        /// <param name="id">The transaction id.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkTransactionResponse}"/> type.</returns>
        /// 
        public async static Task<ArkTransactionResponse> GetUnConfirmedByIdAsync(string id)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Transaction.GET_BY_ID_UNCONFIRMED, id));

            return JsonConvert.DeserializeObject<ArkTransactionResponse>(response);
        }

        /// <summary>
        /// Gets transactions made by an account by range.
        /// </summary>
        /// 
        /// <param name="address">The account address.</param>
        /// 
        /// <param name="offset">The number of transactions to skip.</param>
        /// 
        /// <param name="limit">The maximum number of transactions.</param>
        /// 
        /// <returns>Returns an <see cref="ArkTransactionList"/> type.</returns>
        /// 
        public static ArkTransactionList GetTransactions(string address, int offset = 0, int limit = 50)
        {
            return GetTransactions(new ArkTransactionRequest { OrderBy = "timestamp:desc", RecipientId = address, SenderId = address, Offset = offset, Limit = limit });
        }

        /// <summary>
        /// Asynchronously gets transactions made by an account by range.
        /// </summary>
        /// 
        /// <param name="address">The account address.</param>
        /// 
        /// <param name="offset">The number of transactions to skip.</param>
        /// 
        /// <param name="limit">The maximum number of transactions.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkTransactionList}"/> type.</returns>
        /// 
        public async static Task<ArkTransactionList> GetTransactionsAsync(string address, int offset = 0, int limit = 50)
        {
            return await GetTransactionsAsync(new ArkTransactionRequest { OrderBy = "timestamp:desc", RecipientId = address, SenderId = address, Offset = offset, Limit = limit });
        }

        /// <summary>
        /// Gets unconfirmed transactions made by an account by range.
        /// </summary>
        /// 
        /// <param name="address">The account address.</param>
        /// 
        /// <returns>Returns an <see cref="ArkTransactionList"/> type.</returns>
        /// 
        public static ArkTransactionList GetUnconfirmedTransactions(string address)
        {
            return GetUnconfirmedTransactions(new ArkUnconfirmedTransactionRequest { Address = address });
        }

        /// <summary>
        /// Asynchronously gets unconfirmed transactions made by an account by range.
        /// </summary>
        /// 
        /// <param name="address">The account address.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkTransactionList}"/> type.</returns>
        /// 
        public async static Task<ArkTransactionList> GetUnconfirmedTransactionsAsync(string address)
        {
            return await GetUnconfirmedTransactionsAsync(new ArkUnconfirmedTransactionRequest { Address = address });
        }

        /// <summary>
        /// Creates a request to the peer to create a transaction.
        /// </summary>
        /// 
        /// <param name="transaction">A reference to a <see cref="TransactionApi"/> type.</param>
        /// 
        /// <param name="peer">The peer to create the request to.</param>
        /// 
        /// <returns>Returns an <see cref="ArkTransactionPostResponse"/> type.</returns>
        /// 
        public static ArkTransactionPostResponse PostTransaction(TransactionApi transaction, PeerApi peer = null)
        {
            return PostTransactionAsync(transaction, peer).Result;
        }

        /// <summary>
        /// Asynchronously creates a request to the peer to create a transaction.
        /// </summary>
        /// 
        /// <param name="transaction">A reference to a <see cref="TransactionApi"/> type.</param>
        /// 
        /// <param name="peer">The peer to create the request to.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkTransactionPostResponse}"/> type.</returns>
        /// 
        public async static Task<ArkTransactionPostResponse> PostTransactionAsync(TransactionApi transaction, PeerApi peer = null)
        {
            string body = "{transactions: [" + transaction.ToObject(true) + "]} ";

            var response = string.Empty;

            if (peer == null)
                response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.POST, ArkStaticStrings.ArkApiPaths.Transaction.POST, body);
            else
                response = await peer.MakeRequest(ArkStaticStrings.ArkHttpMethods.POST, ArkStaticStrings.ArkApiPaths.Transaction.POST, body);

            return JsonConvert.DeserializeObject<ArkTransactionPostResponse>(response);
        }

        /// <summary>
        /// Creates a request to multiple peers to create the transaction.
        /// </summary>
        /// 
        /// <param name="transaction">A reference to a <see cref="TransactionApi"/> type.</param>
        /// 
        /// <returns>Returns a <see cref="List{ArkTransactionResponse}"/> type.</returns>
        /// 
        public static List<ArkTransactionPostResponse> MultipleBroadCast(TransactionApi transaction)
        {
            var res = new List<ArkTransactionPostResponse>();

            for (var i = 0; i < ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts; i++)
            {
                res.Add(PostTransaction(transaction, NetworkApi.Instance.GetRandomPeer()));
            }

            return res;
        }

        /// <summary>
        /// Asynchronously creates a request to multiple peers to create the transaction.
        /// </summary>
        /// 
        /// <param name="transaction">A reference to a <see cref="TransactionApi"/> type.</param>
        /// 
        /// <returns>Returns a <see cref="Task{List{ArkTransactionResponse}}"/> type.</returns>
        /// 
        public async static Task<List<ArkTransactionPostResponse>> MultipleBroadCastAsync(TransactionApi transaction)
        {
            var res = new List<ArkTransactionPostResponse>();

            for (var i = 0; i < ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts; i++)
            {
                res.Add(await PostTransactionAsync(transaction, NetworkApi.Instance.GetRandomPeer()));
            }

            return res;
        }

        #endregion
    }
}