using StrongBeaver.Showroom.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(NoBorderEntrryRenderer))]

namespace StrongBeaver.Showroom.iOS.Renderers
{
    public class NoBorderEntrryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null || Control == null)
            {
                return;
            }

            Control.BorderStyle = UIKit.UITextBorderStyle.Bezel;
        }
    }
}
