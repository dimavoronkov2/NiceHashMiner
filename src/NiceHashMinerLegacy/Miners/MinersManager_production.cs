﻿// PRODUCTION
#if !(TESTNET || TESTNETDEV)
using NiceHashMiner.Devices;
using NiceHashMiner.Interfaces;
using NiceHashMiner.Stats;
using NiceHashMiner.Switching;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NiceHashMiner.Miners
{
    public static class MinersManager
    {
        private static MiningSession _curMiningSession;

        public static void StopAllMiners()
        {
            _curMiningSession?.StopAllMiners();
            EthlargementOld.Stop();
            _curMiningSession = null;
        }

        public static void StopAllMinersNonProfitable()
        {
            _curMiningSession?.StopAllMinersNonProfitable();
        }

        public static List<int> GetActiveMinersIndexes()
        {
            return _curMiningSession != null ? _curMiningSession.ActiveDeviceIndexes : new List<int>();
        }

        public static double GetTotalRate()
        {
            var rate01 = _curMiningSession?.GetTotalRate() ?? 0;
            //var rate02 = MiningStats.GetTotalRate(NHSmaData.CurrentProfitsSnapshot());

            return rate01;
        }

        public static bool StartInitialize(IMainFormRatesComunication mainFormRatesComunication,
            string miningLocation, string worker, string btcAdress)
        {
            _curMiningSession = new MiningSession(AvailableDevices.Devices,
                mainFormRatesComunication, miningLocation, worker, btcAdress);

            return _curMiningSession.IsMiningEnabled;
        }

        public static bool IsMiningEnabled()
        {
            return _curMiningSession != null && _curMiningSession.IsMiningEnabled;
        }


        /// <summary>
        /// SwichMostProfitable should check the best combination for most profit.
        /// Calculate profit for each supported algorithm per device and group.
        /// </summary>
        /// <param name="niceHashData"></param>
        //[Obsolete("Deprecated in favour of AlgorithmSwitchingManager timer")]
        //public static async Task SwichMostProfitableGroupUpMethod()
        //{
        //    if (_curMiningSession != null) await _curMiningSession.SwichMostProfitableGroupUpMethod();
        //}

        public static async Task MinerStatsCheck()
        {
            if (_curMiningSession != null) await _curMiningSession.MinerStatsCheck();
        }
    }
}
#endif
