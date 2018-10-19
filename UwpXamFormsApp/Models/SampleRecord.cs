using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Realms;

namespace UwpXamFormsApp.Models
{
    [JsonObject("sampleRecord")]
    public class SampleRecord : RealmObject
    {
        [JsonProperty("guid")]
        [PrimaryKey]
        public string Guid { get; set; }

        [JsonProperty("data1")]
        public string Data1 { get; set; }

        [JsonProperty("data2")]
        public string Datat2 { get; set; }

        [JsonProperty("data3")]
        public string Data3 { get; set; }

        [JsonProperty("data4")]
        public string Data4 { get; set; }

        [JsonProperty("data5")]
        public string Data5 { get; set; }
    }
}
