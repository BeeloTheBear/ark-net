﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlockService.cs" company="Ark">
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

using System;
using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Messages.Block;
using ArkNet.Model.Block;
using ArkNet.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkNet.Service
{
    /// <summary>
    /// Provies functionality for requesting block information.
    /// </summary>
    /// 
    public class BlockService
    {
        #region Methods

        /// <summary>
        /// Gets a block by id.
        /// </summary>
        /// 
        /// <param name="id">The id of the block.</param>
        /// 
        /// <returns>Returns an <see cref="ArkBlockResponse"/> type.</returns>
        /// 
        public static ArkBlockResponse GetById(string id)
        {
            return GetByIdAsync(id).Result;
        }

        /// <summary>
        /// Asynchronously gets a block by id.
        /// </summary>
        /// 
        /// <param name="id">The id of the block.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkBlockResponse}"/> type.</returns>
        /// 
        public async static Task<ArkBlockResponse> GetByIdAsync(string id)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Block.GET_BLOCK, id)).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ArkBlockResponse>(response);
        }

        /// <summary>
        /// Gets all blocks.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="ArkBlockList"/> type.</returns>
        /// 
        public static ArkBlockList GetAll()
        {
            return GetAllAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets all blocks.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{ArkBlockList}"/> type.</returns>
        /// 
        public async static Task<ArkBlockList> GetAllAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_ALL).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ArkBlockList>(response);
        }

        /// <summary>
        /// Gets a range of blocks by range.
        /// </summary>
        /// 
        /// <param name="req">The range paramters.</param>
        /// 
        /// <returns>Returns an <see cref="ArkBlockList"/> type.</returns>
        /// 
        public static ArkBlockList GetBlocks(ArkBlockRequest req)
        {
            return GetBlocksAsync(req).Result;
        }

        /// <summary>
        /// Asynchronously gets a range of blocks by range.
        /// </summary>
        /// 
        /// <param name="req">The range paramters.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkBlockList}"/> type.</returns>
        /// 
        public async static Task<ArkBlockList> GetBlocksAsync(ArkBlockRequest req)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Block.GET_ALL + "{0}", req.ToQuery())).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ArkBlockList>(response);
        }

        /// <summary>
        /// Gets the current epoch.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="DateTime"/> type.</returns>
        /// 
        public static DateTime GetEpoch()
        {
            return GetEpochAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the current epoch.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="Task{DateTime}"/> type.</returns>
        /// 
        public async static Task<DateTime> GetEpochAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_EPOCH).ConfigureAwait(false);
            var parsed = JObject.Parse(response);

            return DateTime.Parse(parsed["epoch"].ToString());
        }

        /// <summary>
        /// Gets the current block number.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="long"/> type.</returns>
        /// 
        public static long GetHeight()
        {
            return GetHeightAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the current block number.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{int}"/> type.</returns>
        /// 
        public async static Task<long> GetHeightAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_HEIGHT).ConfigureAwait(false);
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["height"].ToString());
        }

        /// <summary>
        /// Gets the current block net hash.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="string"/> type.</returns>
        /// 
        public static string GetNetHash()
        {
            return GetNetHashAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the current block net hash.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="Task{string}"/> type.</returns>
        /// 
        public async static Task<string> GetNetHashAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_NETHASH).ConfigureAwait(false);
            var parsed = JObject.Parse(response);

            return parsed["nethash"].ToString();
        }

        /// <summary>
        /// Gets the current block fee.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="Fees"/> type.</returns>
        /// 
        public static Fees GetFees()
        {
            return GetFeesAsync().Result;
        }

        /// <summary>
        /// Asynchronously Gets the current block fee.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="Task{Fees}"/> type.</returns>
        /// 
        public async static Task<Fees> GetFeesAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_FEES).ConfigureAwait(false);
            var parsed = JObject.Parse(response);

            return JsonConvert.DeserializeObject<Fees>(parsed["fees"].ToString());
        }

        /// <summary>
        /// Gets the current block milestone.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="int"/>.</returns>
        /// 
        public static int GetMilestone()
        {
            return GetMilestoneAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the current block milestone.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{int}"/>.</returns>
        /// 
        public async static Task<int> GetMilestoneAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_MILESTONE).ConfigureAwait(false);
            var parsed = JObject.Parse(response);

            return Int32.Parse(parsed["milestone"].ToString());
        }

        /// <summary>
        /// Gets the current block reward.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="int"/>.</returns>
        /// 
        public static int GetReward()
        {
            return GetRewardAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the current block reward.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{int}"/>.</returns>
        /// 
        public async static Task<int> GetRewardAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_REWARD).ConfigureAwait(false);
            var parsed = JObject.Parse(response);

            return Int32.Parse(parsed["reward"].ToString());
        }

        /// <summary>
        /// Gets the current block supply.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="long"/>.</returns>
        /// 
        public static long GetSupply()
        {
            return GetSupplyAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the current block supply.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{int}"/>.</returns>
        /// 
        public async static Task<long> GetSupplyAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_SUPPLY).ConfigureAwait(false);
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["supply"].ToString());
        }

        /// <summary>
        /// Gets the current block status.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="ArkBlockChainStatus"/> type.</returns>
        /// 
        public static ArkBlockChainStatus GetStatus()
        {
            return GetStatusAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the current block status.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{ArkBlockChainStatus}"/> type.</returns>
        /// 
        public async static Task<ArkBlockChainStatus> GetStatusAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_STATUS).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ArkBlockChainStatus>(response);
        }

        #endregion
    }
}
