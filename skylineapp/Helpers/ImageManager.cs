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
    public class ImageManager
    { 
     static ImageManager defaultInstance = new ImageManager();
    MobileServiceClient client;

    IMobileServiceTable<ImageTable> imageTable;

    private ImageManager()
    {
        this.client = new MobileServiceClient(Constants.ApplicationURL);
        this.imageTable = client.GetTable<ImageTable>();
    }

    public static ImageManager DefaultManager
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

    public async Task SaveTaskAsync(ImageTable image)
    {
        try
        {
            if (image.Id == null)
            {
                await imageTable.InsertAsync(image);
            }
            else
            {
                await imageTable.UpdateAsync(image);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(@"Sync error: {0}", e.Message);
        }
    }

    public async Task<ObservableCollection<ImageTable>> GetPictureByCategoryh(string category)
    {
        try
        {
            IEnumerable<ImageTable> images = await imageTable
            .Where(image => image.Category == category)
            .ToEnumerableAsync();

            return new ObservableCollection<ImageTable>(images);
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
        
    public async Task<ObservableCollection<ImageTable>> GetPictureByUser(string username)
        {
            try
            {
                IEnumerable<ImageTable> images = await imageTable
                .Where(image => image.User == username)
                .ToEnumerableAsync();

                return new ObservableCollection<ImageTable>(images);
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
