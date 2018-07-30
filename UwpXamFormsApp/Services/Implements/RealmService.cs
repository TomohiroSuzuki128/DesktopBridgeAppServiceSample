using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpXamFormsApp.Services;
using UwpXamFormsApp.Models;
using Realms;

namespace UwpXamFormsApp.Services
{
	public class RealmService : IRealmService
	{
		public void SaveRecordMeasurement(RecordMeasurement recordMeasurement)
		{
			var context = Realm.GetInstance();

			using (var transaction = context.BeginWrite())
			{
				context.Add(recordMeasurement);
				transaction.Commit();
			}
		}

		public RecordMeasurement ReadRecordMeasurement(string giud)
		{
			var context = Realm.GetInstance();
			var recordMeasurement = context.Find<RecordMeasurement>(giud);

			return recordMeasurement;
		}

	}
}
