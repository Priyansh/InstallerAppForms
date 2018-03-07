using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstallerAppForms.MarkupExtension
{
    [ContentProperty("Source")]
    public class EmbeddedImage : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Source))
                return null;
            return ImageSource.FromResource(Source);
        }
    }
}
