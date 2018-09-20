using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace StrongBeaver.Services.Toast
{
    internal class ToastView : UIView
    {
        private const float PADDING = 10.0f;
        private const float ALPHA = 0.96f;

        private static readonly UIColor MessageTextColor;
        private static readonly UIFont MessageFont;
        private static readonly UIColor DefaultBackgroundColor;
        private static readonly UIColor DefaultStrokeColor;

        private float height;
        private nfloat width;

        static ToastView()
        {
            MessageFont = UIFont.SystemFontOfSize(14.0f);
            MessageTextColor = UIColor.FromWhiteAlpha(1.0f, 1.0f);
            DefaultBackgroundColor = UIColor.FromRGBA(0.0f, 0.482f, 1.0f, ALPHA);
            DefaultStrokeColor = UIColor.FromRGBA(0.0f, 0.415f, 0.803f, 1.0f);
        }

        internal ToastView(string message)
            : this((NSString)message)
        {
            // No operation
        }
        internal ToastView(NSString message)
            : base(CGRect.Empty)
        {
            BackgroundColor = DefaultBackgroundColor;
            ClipsToBounds = false;
            UserInteractionEnabled = true;
            Message = message;
            Height = 0.0f;
            Width = 0.0f;
            Hit = false;

            NSNotificationCenter.DefaultCenter.AddObserver(
                UIDevice.OrientationDidChangeNotification,
                OrientationChanged);

            AccessibilityIdentifier = "Toast";
        }

        public TimeSpan Duration { get; set; }

        [Export("Message")]
        public NSString Message { get; set; }

        public bool HasMessage
        {
            get
            {
                return !string.IsNullOrEmpty(Message);
            }
        }

        public float Height
        {
            get
            {
                if (height == 0)
                {
                    CGSize messageLabelSize = MessageSize();
                    height = (float)((PADDING * 2)+ messageLabelSize.Height);
                }

                return height;
            }

            private set
            {
                height = value;
            }
        }

        public nfloat Width
        {
            get
            {
                if (width == 0)
                {
                    width = GetStatusBarFrame().Width;
                }

                return width;
            }

            private set
            {
                width = value;
            }
        }

        private nfloat AvailableWidth
        {
            get
            {
                nfloat maxWidth = Width - (PADDING * 2);
                return maxWidth;
            }
        }

        public bool Hit { get; set; }

        public Action OnDismiss { get; set; }

        public Action OnTapped { get; set; }

        public override void Draw(CGRect rect)
        {
            var context = UIGraphics.GetCurrentContext();
            context.SaveState();

            BackgroundColor.SetColor();
            context.FillRect(rect);
            context.RestoreState();
            context.SaveState();

            context.BeginPath();
            context.MoveTo(0, rect.Size.Height);
            context.SetStrokeColor(DefaultStrokeColor.CGColor);
            context.SetLineWidth(1);
            context.AddLineToPoint(rect.Size.Width, rect.Size.Height);
            context.StrokePath();
            context.RestoreState();
            context.SaveState();

            CGSize descriptionLabelSize = MessageSize();

            if (HasMessage)
            {
                MessageTextColor.SetColor();

                var descriptionRectangle = new CGRect(
                    PADDING,
                    PADDING,
                    descriptionLabelSize.Width,
                    descriptionLabelSize.Height);

                Message.DrawString(
                    descriptionRectangle,
                    MessageFont,
                    UILineBreakMode.TailTruncation,
                    UITextAlignment.Left);
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ToastView))
            {
                return false;
            }

            ToastView messageView = (ToastView)obj;
            return Message == messageView.Description;
        }

        public override int GetHashCode()
        {
            return HasMessage ? Message.GetHashCode() : 0;
        }

        private static bool IsRunningiOS7OrLater()
        {
            return UIDevice.CurrentDevice.CheckSystemVersion(7, 0);
        }

        private static bool IsRunningiOS8OrLater()
        {
            return UIDevice.CurrentDevice.CheckSystemVersion(8, 0);
        }

        private CGSize MessageSize()
        {
            if (!HasMessage)
            {
                return CGSize.Empty;
            }

            var boundedSize = new CGSize(AvailableWidth, float.MaxValue);
            CGSize descriptionLabelSize;

            if (!IsRunningiOS7OrLater())
            {
                var attr = new UIStringAttributes(NSDictionary.FromObjectAndKey(MessageFont, (NSString)MessageFont.Name));
                descriptionLabelSize = Message.GetBoundingRect(
                    boundedSize,
                    NSStringDrawingOptions.TruncatesLastVisibleLine,
                    attr,
                    null).Size;
            }
            else
            {
                descriptionLabelSize = Message.StringSize(
                    MessageFont,
                    boundedSize,
                    UILineBreakMode.TailTruncation);
            }

            return descriptionLabelSize;
        }

        private CGRect GetStatusBarFrame()
        {
            var windowFrame = OrientFrame(UIApplication.SharedApplication.KeyWindow.Frame);
            var statusFrame = OrientFrame(UIApplication.SharedApplication.StatusBarFrame);

            return new CGRect(windowFrame.X, windowFrame.Y, windowFrame.Width, statusFrame.Height);
        }

        private bool IsDeviceLandscape(UIDeviceOrientation orientation)
        {
            return orientation == UIDeviceOrientation.LandscapeLeft || orientation == UIDeviceOrientation.LandscapeRight;
        }

        private bool IsStatusBarLandscape(UIInterfaceOrientation orientation)
        {
            return orientation == UIInterfaceOrientation.LandscapeLeft
                   || orientation == UIInterfaceOrientation.LandscapeRight;
        }

        private CGRect OrientFrame(CGRect frame)
        {
            // This size has already inverted in iOS8, but not on simulator, seems odd
            if (!IsRunningiOS8OrLater()
                && (IsDeviceLandscape(UIDevice.CurrentDevice.Orientation)
                    || IsStatusBarLandscape(UIApplication.SharedApplication.StatusBarOrientation)))
            {
                frame = new CGRect(frame.X, frame.Y, frame.Height, frame.Width);
            }

            return frame;
        }

        private void OrientationChanged(NSNotification notification)
        {
            Frame = new CGRect(Frame.X, Frame.Y, GetStatusBarFrame().Width, Frame.Height);
            SetNeedsDisplay();
        }
    }
}