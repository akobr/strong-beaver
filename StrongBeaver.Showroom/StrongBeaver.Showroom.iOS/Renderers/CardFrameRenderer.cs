using StrongBeaver.Showroom.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(CardFrameRenderer))]

namespace StrongBeaver.Showroom.iOS.Renderers
{
    public class CardFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                SetupCardLayer();
            }
        }

        private void SetupCardLayer()
        {
            if (Element.HasShadow)
            {
                Layer.ShadowRadius = 2f;
            }
        }
    }
}
