using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;
using System.Threading;

namespace StrongBeaver.Core.Services.Dialog
{
    /// <summary>
    /// Based on Dropdown message bar for iOS;
    /// https://github.com/prashantvc/Xamarin.iOS-MessageBar;
    /// Nuget MessageBarLib 1.2.1.1;
    /// Commits on Sep 16, 2016 (c30d84c4098c3b3f292127f7088014953113f8cf);
    /// </summary>
    internal class ToastManager : NSObject
    {
        private const float DismissAnimationDuration = 0.25f;

        private readonly Queue<ToastView> messageBarQueue;

        private nfloat initialPosition = 0;
        private ToastView lastMessage;
        private nfloat showPosition = 0;

        internal ToastManager()
        {
            messageBarQueue = new Queue<ToastView>();
            MessageVisible = false;

            ShowAtTheBottom = true;
            MessageBarOffset = 20;
            DiscardRepeated = true;
        }

        public bool DiscardRepeated { get; set; }

        public bool ShowAtTheBottom { get; set; }

        private nfloat MessageBarOffset { get; set; }

        public bool MessageVisible { get; private set; }

        private UIViewController ViewController => UIApplication.SharedApplication.KeyWindow.RootViewController;

        private UIView MessageWindowView => ViewController.View;

        public void HideAll()
        {
            var subviews = MessageWindowView.Subviews;

            foreach (UIView subview in subviews)
            {
                ToastView view = subview as ToastView;
                view?.RemoveFromSuperview();
            }

            MessageVisible = false;
            messageBarQueue.Clear();
            CancelPreviousPerformRequest(this);
        }

        public void ShowMessage(
            string message,
            TimeSpan? duration = null,
            Action onTapped = null,
            Action onDismiss = null)
        {
            var messageView = new ToastView(message)
            {
                OnTapped = onTapped,
                OnDismiss = onDismiss,
                Hidden = true,
                Duration = duration ?? TimeSpan.FromSeconds(2.5)
            };

            MessageWindowView.AddSubview(messageView);
            MessageWindowView.BringSubviewToFront(messageView);

            messageBarQueue.Enqueue(messageView);

            if (!MessageVisible)
            {
                ShowNextMessage();
            }
        }

        private static void InvokeDismissEvent(ToastView messageView, bool dismissedByTap)
        {
            if (!dismissedByTap)
            {
                messageView.OnDismiss?.Invoke();
            }
            else
            {
                messageView.OnTapped?.Invoke();
            }
        }

        private void DismissMessage(object messageView)
        {
            ToastView view = messageView as ToastView;

            if (view == null)
            {
                return;
            }

            InvokeOnMainThread(() => DismissMessage(view, false));
        }

        private void DismissMessage(ToastView messageView, bool dismissedByTap)
        {
            if (messageView == null || messageView.Hit)
            {
                return;
            }

            messageView.Hit = true;

            UIView.Animate(
                DismissAnimationDuration,
                () => {
                    messageView.Frame = new CGRect(
                        messageView.Frame.X,
                        initialPosition,
                        messageView.Frame.Width,
                        messageView.Frame.Height);
                },
                () => {
                    MessageVisible = false;
                    messageView.RemoveFromSuperview();

                    InvokeDismissEvent(messageView, dismissedByTap);

                    if (messageBarQueue.Count > 0)
                    {
                        ShowNextMessage();
                    }
                    else
                    {
                        lastMessage = null;
                    }
                });
        }

        private ToastView GetNextMessage()
        {
            ToastView message = null;

            if (!DiscardRepeated)
            {
                return messageBarQueue.Dequeue();
            }

            // Removes all except the last message.
            while (messageBarQueue.Count > 0)
            {
                message = messageBarQueue.Dequeue();

                if (!IsEqualLastMessage(message))
                {
                    break;
                }

                message = null;
            }

            lastMessage = message;
            return message;
        }

        private bool IsEqualLastMessage(ToastView message)
        {
            return message.Equals(lastMessage);
        }

        private void MessageTapped(UIGestureRecognizer recognizer)
        {
            ToastView view = recognizer.View as ToastView;

            if (view != null)
            {
                DismissMessage(view, true);
            }
        }

        private void ShowNextMessage()
        {
            ToastView messageView = GetNextMessage();

            if (messageView != null)
            {
                MessageVisible = true;

                if (ShowAtTheBottom)
                {
                    initialPosition = MessageWindowView.Bounds.Height + messageView.Height;
                    showPosition = MessageWindowView.Bounds.Height - messageView.Height;
                }
                else
                {
                    initialPosition = MessageWindowView.Bounds.Y - messageView.Height;
                    showPosition = MessageWindowView.Bounds.Y + MessageBarOffset;
                }

                messageView.Frame = new CGRect(0, initialPosition, messageView.Width, messageView.Height);
                messageView.Hidden = false;
                messageView.SetNeedsDisplay();

                var gest = new UITapGestureRecognizer(MessageTapped);
                messageView.AddGestureRecognizer(gest);

                UIView.Animate(
                    DismissAnimationDuration,
                    () => messageView.Frame = new CGRect(
                                                  messageView.Frame.X,
                                                  showPosition,
                                                  messageView.Width,
                                                  messageView.Height));

                // Need a better way of dissmissing the method
                var dismiss = new Timer(
                    DismissMessage,
                    messageView,
                    messageView.Duration,
                    TimeSpan.FromMilliseconds(-1));
            }
        }
    }
}