using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Runtime.Serialization.Json;
using System.Text;
using Carrot.Contracts;

namespace Carrot.Parts
{
    [Serializable]
    [Export(typeof(IExport))]
    public class CurrentCiStatus : MarshalByRefObject, ICiStatus
    {
        private static readonly string BunnyUrl =
            "http://cifarm.itransition.corp/rest/build-bunny/1.0/summary/7e1004f2-3b94-4c06-b744-09c3ea3ffe5b.json?os_authType=basic";
        private static readonly string User = "planstatechecker.transvault";
        private static readonly string Password = "pB50GjhcouD";
        private static readonly IDictionary<string, CiWarningLevel> LevelMap = 
            new Dictionary<string, CiWarningLevel>
            {
                {"OK", CiWarningLevel.Ok},
                {"WARN", CiWarningLevel.Warn},
                {"FAIL", CiWarningLevel.Error},
            };

        /// <summary>
        /// Warning level raised by CI.
        /// </summary>
        public CiWarningLevel Level { get; set; } 

        /// <summary>
        /// Offender plans (failed).
        /// </summary>
        public string[] Offenders { get; set; }

        /// <summary>
        /// Retrieves new information on CI status, refreshes 'this' obkect fields.
        /// </summary>
        public void Refresh()
        {
            ProcessCifarmPlansState(Get());
        }

        private static BunnyOutputDto Get()
        {
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(new Uri(BunnyUrl));
            wr.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            wr.ContentType = "application/json";
            wr.Headers.Add("Authorization", GetAuthorizationString());
            wr.Method = WebRequestMethods.Http.Get;
            wr.Host = "cifarm.itransition.corp";
            using (var resp = wr.GetResponse() as HttpWebResponse)
            {
                if (resp == null)
                {
                    throw new InvalidOperationException("Could not retrieve latest state of CI plans.");
                }
                if (resp.IsFromCache)
                {
                    throw new ApplicationException("HTTP Client has raised Cifarm data from cache, which is unacceptable.");
                }
                if (resp.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(String.Format("HTTP Client returned error status code: {0}", resp.StatusCode));
                }
                using (Stream responseStream = resp.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(BunnyOutputDto));
                        return serializer.ReadObject(responseStream) as BunnyOutputDto;
                    }
                }
            }
            throw new Exception();
        }

        private void ProcessCifarmPlansState(BunnyOutputDto state)
        {
            string status = state.status.ToUpper();
            if (!LevelMap.ContainsKey(status))
            {
                throw new InvalidOperationException(String.Format("Unknown CI status: {0}", status));
            }
            Level = LevelMap[status];
            Offenders = state.offenders.Select(o => o.planName).ToArray();
        }

        private static string GetAuthorizationString()
        {
            string encoded = Convert.ToBase64String(
                Encoding.Default.GetBytes(String.Format("{0}:{1}", User, Password)));
            return String.Format("Basic {0}", encoded);
        }
    }
}
