using System;
using System.Collections.Generic;
using System.Text;

namespace UwpXamFormsApp.Services
{
	public enum OpeningPage
	{
		PageA,
		PageB,
		PageC,
	}

	// MeasurementQuestion EventArgs
	public class MeasurementQuestionEventArgs : EventArgs
	{
		// GUID（一位のキー）
		public Guid Guid { get; set; }

		// CardNo（会員番号）
		public string CardNo { get; set; }

		// MeasurementCourseId（肌測定コース:1〜8）
		public int MeasurementCourseId { get; set; }

		// QuestionNo（1〜）
		public int QuestionNo { get; set; }

		// AnswerNo（1〜）
		public int AnswerNo { get; set; }
	}

	// 肌測定結果 EventArgs
	public class RecordMeasurementEventArgs : EventArgs
	{
		public OpeningPage OpeningPage { get; set; }

		// GUID（一意のキー）
		public Guid Guid { get; set; }

		// CardNo（会員番号）
		public string CardNo { get; set; }

		// RecordNo（対応履歴番号：連番）
		public Guid RecordNo { get; set; }

		// MeasuredAt（測定日時）
		public DateTimeOffset MeasuredAt { get; set; }

		// result1:結果文字列
		public string Result1 { get; set; }

		// result2:結果文字列
		public string Result2 { get; set; }

		// result3:結果文字列
		public string Result3 { get; set; }

		// result4:結果文字列
		public string Result4 { get; set; }

		// result5:結果文字列
		public string Result5 { get; set; }
	}

	// 肌測定結果受信
	public delegate void RecordMeasurementEventHandler(object sender, RecordMeasurementEventArgs args);


	public interface IDataShareService
	{
		event RecordMeasurementEventHandler RecordMeasurementReceived;
	}

}
