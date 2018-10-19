using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UwpXamFormsApp.Models;

namespace UwpXamFormsApp.Services
{
    public interface IRealmService
    {
        void SaveSampleRecord(SampleRecord sampleRecord);

        SampleRecord FindSampleRecord(string giud);

        IQueryable<SampleRecord> AllSampleRecords();

    }
}
