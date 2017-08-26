using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using Xamarin.Forms;

namespace skylineapp.Models
{
    [ContentProperty("Source")]
    public class ImageResourceExtension: IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;
            var imageSource = ImageSource.FromResource(Source);
            return imageSource;
        }
    }
}
