using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UwpXamFormsApp.Models
{
	[JsonObject("recordMeasurement")]
	public class RecordMeasurement
	{
		// GUID（一意のキー）
		[JsonProperty("guid")]
		public Guid Guid { get; set; }

		// CardNo（会員番号）
		[JsonProperty("cardNo")]
		public string CardNo { get; set; }

		// RecordNo（対応履歴番号：連番）
		[JsonProperty("recordNo")]
		public string RecordNo { get; set; }

		// MeasuredAt（測定日時）
		[JsonProperty("measuredAt")]
		public DateTimeOffset MeasuredAt { get; set; }

		// result1:結果文字列
		[JsonProperty("result1")]
		public string Result1 { get; set; }

		// result2:結果文字列
		[JsonProperty("result2")]
		public string Result2 { get; set; }

		// result3:結果文字列
		[JsonProperty("result3")]
		public string Result3 { get; set; }

		// result4:結果文字列
		[JsonProperty("result4")]
		public string Result4 { get; set; }

		// result5:結果文字列
		[JsonProperty("result5")]
		public string Result5 { get; set; }
	}
}
