using Microsoft.WindowsAzure.MobileServices;
using skylineapp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skylineapp.Helpers
{
    public class UserManager
    {
        static UserManager defaultInstance = new UserManager();
        MobileServiceClient client;

        IMobileServiceTable<User> userTable;

        private UserManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);
            this.userTable = client.GetTable<User>();
        }

        public static UserManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public async Task SaveTaskAsync(User user)
        {
            try
            {
                if (user.Id == null)
                {
                    await userTable.InsertAsync(user);
                }
                else
                {
                    await userTable.UpdateAsync(user);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
        }

        public async Task<ObservableCollection<User>> GetUserByUsernameAsync(string username)
        {
            try
            {
                IEnumerable<User> users = await userTable
                .Where(user => user.Username == username)
                .ToEnumerableAsync();

                return new ObservableCollection<User>(users);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task<ObservableCollection<User>> GetUserByEmailAsync(string email)
        {
            try
            {
                IEnumerable<User> users = await userTable
                    .Where(user => user.Email == email)
                    .ToEnumerableAsync();

                return new ObservableCollection<User>(users);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task<ObservableCollection<User>> GetUser()
        {
            try
            {
                IEnumerable<User> users = await userTable
                .ToEnumerableAsync();

                return new ObservableCollection<User>(users);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }


    }
}
