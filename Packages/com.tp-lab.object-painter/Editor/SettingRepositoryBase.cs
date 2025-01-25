using System.IO;
using UnityEditor;
using UnityEngine;

namespace TpLab.ObjectPainter.Editor
{
    public abstract class SettingRepositoryBase<TRepository, TSetting>
        where TRepository : SettingRepositoryBase<TRepository, TSetting>, new()
        where TSetting : ScriptableObject
    {
        protected abstract string SettingPath { get; }

        static TRepository _instance;

        public static TRepository Instance => _instance ??= new TRepository();

        /// <summary>
        /// 設定を読み込む。
        /// </summary>
        /// <returns>設定</returns>
        public TSetting LoadSetting() => AssetDatabase.LoadAssetAtPath<TSetting>(SettingPath)
                                         ?? ScriptableObject.CreateInstance<TSetting>();

        /// <summary>
        /// 設定を保存する。
        /// </summary>
        /// <param name="setting">設定</param>
        public void SaveSetting(TSetting setting)
        {
            var dir = Path.GetDirectoryName(SettingPath);
            if (!string.IsNullOrEmpty(dir) && !AssetDatabase.IsValidFolder(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (!AssetDatabase.Contains(setting))
            {
                AssetDatabase.CreateAsset(setting, SettingPath);
            }

            EditorUtility.SetDirty(setting);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
