using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Game.Core.Extensions
{
    public static class LocalizationExtensions
    {
        public static async UniTask<Queue<string>> GetAllEntries(this StringTable source)
        {
            var initializeOperation = LocalizationSettings.InitializationOperation;
            await UniTask.WaitUntil(() => initializeOperation.IsDone);

            var tableLoadOperation = LocalizationSettings.StringDatabase.GetTableAsync(source.TableCollectionName);
            await UniTask.WaitUntil(() => tableLoadOperation.IsDone);

            var stringTable = tableLoadOperation.Result;
            if (stringTable == null) return null;

            List<SharedTableData.SharedTableEntry> allEntries = stringTable.SharedData.Entries;
            Queue<string> result = new();

            foreach (string key in allEntries
                .Where(entry => entry != null)
                .Select(entry => entry.Key)
                .ToList())
            {
                string translation = stringTable.GetEntry(key)?.Value;
                result.Enqueue(translation);
            }

            return result;
        }
    }
}
