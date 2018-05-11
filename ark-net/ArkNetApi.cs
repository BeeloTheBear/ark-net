﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkNetApi.cs" company="Ark">
//   MIT License
// 
// Copyright (c) 2017 Kristjan Košič
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>
// <summary>
//   Defines the ArkNetApi type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Logging;
using ArkNet.Model.Loader;
using ArkNet.Model.Peer;
using ArkNet.Service;
using ArkNet.Utils;
using ArkNet.Utils.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkNet
{
    /// <summary>
    /// ARK API's entry.
    /// </summary>
    public sealed class ArkNetApi
    {
        /// <summary>
        /// Initial Seeds for the MainNet
        /// </summary>
        private List<Tuple<string, int>> _peerSeedListMainNet = 
            new List<Tuple<string, int>> {
            Tuple.Create("5.39.9.240", 4001),
            Tuple.Create("5.39.9.241", 4001),
            Tuple.Create("5.39.9.242", 4001),
            Tuple.Create("5.39.9.243", 4001),
            Tuple.Create("5.39.9.244", 4001),
            Tuple.Create("5.39.9.250", 4001),
            Tuple.Create("5.39.9.251", 4001),
            Tuple.Create("5.39.9.252", 4001),
            Tuple.Create("5.39.9.253", 4001),
            Tuple.Create("5.39.9.254", 4001),
            Tuple.Create("5.39.9.255", 4001)
            };

        /// <summary>
        /// Initial Seeds for the DevNet
        /// </summary>
        private List<Tuple<string, int>> _peerSeedListDevNet =
            new List<Tuple<string, int>> {
            Tuple.Create("167.114.43.48", 4002),
            Tuple.Create("167.114.29.49", 4002),
            Tuple.Create("167.114.43.43", 4002),
            Tuple.Create("167.114.29.54", 4002),
            Tuple.Create("167.114.29.45", 4002),
            Tuple.Create("167.114.29.40", 4002),
            Tuple.Create("167.114.29.56", 4002),
            Tuple.Create("167.114.43.35", 4002),
            Tuple.Create("167.114.29.51", 4002),
            Tuple.Create("167.114.29.59", 4002),
            Tuple.Create("167.114.43.42", 4002),
            Tuple.Create("167.114.29.34", 4002),
            Tuple.Create("167.114.29.62", 4002),
            Tuple.Create("167.114.43.49", 4002),
            Tuple.Create("167.114.29.44", 4002)
            };

        private IArkLogger _arkLogger { get; set; }

        private LoggingApi _loggingApi;
        public LoggingApi LoggingApi
        {
            get { return _loggingApi ?? (_loggingApi = new LoggingApi(_arkLogger)); }
        }

        private NetworkApi _networkApi;
        public NetworkApi NetworkApi
        {
            get { return _networkApi ?? (_networkApi = new NetworkApi(this)); }
        }

        private TransactionApi _transactionApi;
        public TransactionApi TransactionApi
        {
            get { return _transactionApi ?? (_transactionApi = new TransactionApi(NetworkApi, LoggingApi)); }
        }

        private AccountService _accountService;
        public AccountService AccountService
        {
            get
            {
                return _accountService ?? (_accountService = new AccountService(NetworkApi, LoggingApi));
            }
        }

        private BlockService _blockService;
        public BlockService BlockService
        {
            get
            {
                return _blockService ?? (_blockService = new BlockService(NetworkApi, LoggingApi));
            }
        }

        private DelegateService _delegateService;
        public DelegateService DelegateService
        {
            get
            {
                return _delegateService ?? (_delegateService = new DelegateService(NetworkApi, LoggingApi));
            }
        }

        private LoaderService _loaderService;
        public LoaderService LoaderService
        {
            get
            {
                return _loaderService ?? (_loaderService = new LoaderService(NetworkApi, LoggingApi));
            }
        }

        private PeerService _peerService;
        public PeerService PeerService
        {
            get
            {
                return _peerService ?? (_peerService = new PeerService(NetworkApi, LoggingApi));
            }
        }

        private TransactionService _transactionService;
        public TransactionService TransactionService
        {
            get
            {
                return _transactionService ?? (_transactionService = new TransactionService(NetworkApi, LoggingApi));
            }
        }

        /// <summary>
        /// Start the Network
        /// </summary>
        /// <param name="type">
        /// <inheritdoc cref="NetworkType"/> Can be :
        /// -- DevNet (test), ask Dark (testnet coins) on the slack.
        /// -- MainNet (live, beware real money, financial loss possible there).
        /// </param>
        /// <returns> The <inheritdoc cref="Task"/> starts the node.</returns>
        public async Task Start(NetworkType type, IArkLogger logger = null)
        {
            _arkLogger = logger;
            await SetNetworkSettings(await GetInitialPeer(type).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <summary>
        /// Start the Network.
        /// </summary>
        /// <param name="initialPeerIp"> The Initial peer's IP</param>
        /// <param name="initialPeerPort"> The Initial Peer's Port</param>
        /// <returns> The <inheritdoc cref="Task"/> starts the node.</returns>
        public async Task Start(string initialPeerIp, int initialPeerPort, IArkLogger logger = null)
        {
            _arkLogger = logger;
            await SetNetworkSettings(GetInitialPeer(initialPeerIp, initialPeerPort)).ConfigureAwait(false);
        }

        /// <summary>
        /// Switches the Network
        /// </summary>
        /// <param name="type">
        /// <inheritdoc cref="NetworkType"/> Can be :
        /// -- DevNet (test), ask Dark (testnet coins) on the slack.
        /// -- MainNet (live, beware real money, financial loss possible there).
        /// </param>
        /// <returns> The <inheritdoc cref="Task"/> switches the network.</returns>
        public async Task SwitchNetwork(NetworkType type)
        {
            NetworkApi.NetworkSettings = null;
            await SetNetworkSettings(await GetInitialPeer(type).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <summary>
        /// Switches the Network
        /// </summary>
        /// <param name="peerId"> The Initial peer's IP</param>
        /// <param name="peerPort"> The Initial Peer's Port</param>
        /// <returns> The <inheritdoc cref="Task"/> switches the network.</returns>
        public async Task SwitchNetwork(string peerId, int peerPort)
        {
            NetworkApi.NetworkSettings = null;
            await SetNetworkSettings(GetInitialPeer(peerId, peerPort)).ConfigureAwait(false);
        }

        /// <summary>
        /// Fetch the the NetworkSettings, and set the NetworkSettings variable,
        /// which is used for every subsequent request after the initial ones.
        /// </summary>
        /// <param name="initialPeer">Initial Peer <inheritdoc cref="PeerApi"/> from which the settings are fetched.</param>
        /// <returns>Instiate a <inheritdoc cref="PeerApi"/> based on the initial peer provided.</returns>
        private async Task SetNetworkSettings(PeerApi initialPeer)
        {
            // Request the NetworkSettings, Fees, and more peer address from the peers it connects to. 
            var responseAutoConfigure = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_AUTO_CONFIGURE).ConfigureAwait(false);
            var responseFees = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_FEES).ConfigureAwait(false);
            var responsePeer = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Peer.GET, initialPeer.Ip, initialPeer.Port)).ConfigureAwait(false);

            // Auto-configures what has been fetched previously
            var autoConfig = JsonConvert.DeserializeObject<ArkLoaderNetworkResponse>(responseAutoConfigure);
            var fees = JsonConvert.DeserializeObject<Fees>(JObject.Parse(responseFees)["fees"].ToString());
            var peer = JsonConvert.DeserializeObject<ArkPeerResponse>(responsePeer);

            // Fill the NetworkSettings with what has been fetched / auto-configured previously.
            NetworkApi.NetworkSettings = new ArkNetworkSettings()
            {
                Port = initialPeer.Port,
                BytePrefix = (byte)autoConfig.Network.Version,
                Version = peer.Peer.Version,
                NetHash = autoConfig.Network.NetHash,
                Fee = fees
            };

            await NetworkApi.WarmUp(new PeerApi(NetworkApi, initialPeer.Ip, initialPeer.Port)).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the initial peers to connect to the blockchain.
        /// </summary>
        /// <param name="initialPeerIp">Ip of the first peer.</param>
        /// <param name="initialPeerPort">Port that the first peer listen on.</param>
        /// <returns>Return the first peer found at the IP/Port given.</returns>
        private PeerApi GetInitialPeer(string initialPeerIp, int initialPeerPort)
        {
            return new PeerApi(NetworkApi, initialPeerIp, initialPeerPort);
        }

        /// <summary>
        /// Is needed to fetch the peer.
        /// </summary>
        /// <param name="type">
        /// <inheritdoc cref="NetworkType"/>Can be :
        /// -- DevNet (test), ask Dark (testnet coins) on the slack.
        /// -- MainNet (live, beware real money, financial loss possible there).
        /// </param>
        /// <param name="retryCount">Number of retry before a timeout</param>
        /// <returns>Returns the first <inheritdoc cref="PeerApi"/> that is online</returns>
        private async Task<PeerApi> GetInitialPeer(NetworkType type, int retryCount = 0)
        {
            // Pick a peer randomly in _peerSeedListMainNet //
            var peerUrl = _peerSeedListMainNet[new Random().Next(_peerSeedListMainNet.Count)];

            // If the Network is set to DevNet, change the peer picked above by a peer from _peerSeedListDevNet //
            if (type == NetworkType.DevNet)
                peerUrl = _peerSeedListDevNet[new Random().Next(_peerSeedListDevNet.Count)];

            // create a peer out of peerurl, and returns if the peer is online. //
            var peer = new PeerApi(NetworkApi, peerUrl.Item1, peerUrl.Item2);
            if (await peer.IsOnline().ConfigureAwait(false))
            {
                return peer;
            }

            // Throw an exception if all of the initial peers have been tried. //
            if ((type == NetworkType.DevNet && retryCount == _peerSeedListDevNet.Count) 
             || (type == NetworkType.MainNet && retryCount == _peerSeedListMainNet.Count))
                throw new Exception("Unable to connect to a seed peer");

            // redo the check and increment the retry count //
            return await GetInitialPeer(type, retryCount + 1).ConfigureAwait(false);
        }
    }
}
