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
    /// <summary>
    /// RealmへのRead, Writeなどを行う
    /// </summary>
    /// <remarks>
    /// ・RealmはFodyというAOP（アスペクト指向）ライブラリを利用しているので
    /// 　FodyWeavers.xmlの配置が必要、足りない場合ビルド時にエラーとなるので注意
    /// 　コンパイル時に RealmObject のサブクラスへ自動的にコードが注入されているはずである。
    /// ・データベースの実体は、Realm.GetInstance().Config.DatabasePath で取得可能
    /// （例）C:\Users\Tomohiro Suzuki\AppData\Local\Packages\0753ead7-15a3-4399-a0fe-7b57db3072db_w4wzfqr0x77c8\LocalState\default.realm
    /// テーブルは初回アクセス時にModelの定義から自動的に作られる
    /// ・データベースの実体は https://realm.io/products/realm-studio/ で管理可能
    /// </remarks>
    public class RealmService : IRealmService
    {
        public void SaveSampleRecord(SampleRecord recordMeasurement)
        {
            var context = Realm.GetInstance();

            // 下記のようにトランザクションを利用可能
            using (var transaction = context.BeginWrite())
            {
                context.Add(recordMeasurement);
                transaction.Commit();
            }
        }

        public SampleRecord FindSampleRecord(string giud)
        {
            var context = Realm.GetInstance();
            return context.Find<SampleRecord>(giud);
        }

        public IQueryable<SampleRecord> AllSampleRecords()
        {
            var context = Realm.GetInstance();
            return context.All<SampleRecord>();
        }

    }
}
