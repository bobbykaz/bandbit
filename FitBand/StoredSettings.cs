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

        private const string AppSettingsKey = "AppSettings";
        private const string RefreshTokenKey = "RefreshToken";

        public static async Task<string> TryLoadRefreshToken()
        {
            try
            {
                return await LoadString(RefreshTokenKey);
            }
            catch(FileNotFoundException)
            {
                return null;
            }
        }

        public static async Task SaveRefreshToken(string token)
        {
            await SaveString(RefreshTokenKey, token);
        }

        public static async Task DeleteRefreshToken()
        {
            await DeleteString(RefreshTokenKey);
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
