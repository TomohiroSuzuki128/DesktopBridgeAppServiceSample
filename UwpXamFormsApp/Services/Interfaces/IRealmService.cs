﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UwpXamFormsApp.Models;

namespace UwpXamFormsApp.Services
{
	public interface IRealmService
	{
		void SaveRecordMeasurement(RecordMeasurement recordMeasurement);

		RecordMeasurement FindRecordMeasurement(string giud);

		IQueryable<RecordMeasurement> AllRecordMeasurements();

	}
}