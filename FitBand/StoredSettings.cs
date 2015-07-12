using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FitBand
{
    public static class StoredSettings
    {
        public static ApplicationDataContainer _AppSettings;

        public enum StoredStringValues
        { 
            AppSettings,
            RefreshToken,
            ImplicitAccessToken
        }

        public static async Task<string> TryLoad(StoredStringValues desiredKey)
        {
            string key = Enum.GetName(typeof(StoredStringValues), desiredKey);

            try
            {
                return await LoadString(key);
            }
            catch(FileNotFoundException)
            {
                return null;
            }
        }

        public static async Task Save(StoredStringValues desiredKey, string value)
        {
            string key = Enum.GetName(typeof(StoredStringValues), desiredKey);
            await SaveString(key, value);
        }

        public static async Task Delete(StoredStringValues desiredKey)
        {
            string key = Enum.GetName(typeof(StoredStringValues), desiredKey);
            await DeleteString(key);
        }

        private static async Task SaveString(string Key, string content)
        {
            byte[] savedata = Encoding.UTF8.GetBytes(content);
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(Key, CreationCollisionOption.ReplaceExisting);
            using (Stream s = await file.OpenStreamForWriteAsync())
            {
                await s.WriteAsync(savedata, 0, savedata.Length);
            }
        }

        private static async Task<string> LoadString(string Key)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(Key);
            byte[] loaddata;
            using (Stream s = await file.OpenStreamForReadAsync())
            {
                loaddata = new byte[s.Length];
                await s.ReadAsync(loaddata, 0, loaddata.Length);
            }

            return Encoding.UTF8.GetString(loaddata, 0, loaddata.Length);
        }

        private static async Task DeleteString(string Key)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile file = await folder.GetFileAsync(Key);
                await file.DeleteAsync();
            }
            catch (FileNotFoundException)
            { }
        }
    }
}
