using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstallerAppForms.MarkupExtension
{
    [ContentProperty("ResourceId")]
    public class EmbeddedImage : IMarkupExtension
    {
        public string ResourceId { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(ResourceId))
                return null;
            return ImageSource.FromResource(ResourceId);
        }
    }
}
