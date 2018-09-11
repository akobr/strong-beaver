﻿using StrongBeaver.Core.Platform;
using StrongBeaver.Core.ViewModel;

namespace StrongBeaver.Showroom.ViewModel
{
    public class ViewModelLocator : BaseViewModelLocator, IViewModelLocator
    {
        public ViewModelLocator(IPlatformInfo platformInfo, IMainViewModel mainViewModel, ExemplaryViewModel exemplaryViewModel)
            : base(platformInfo)
        {
            MainViewModel = mainViewModel;
            RegisterViewModel(mainViewModel);

            Exemplary = exemplaryViewModel;
            RegisterViewModel(exemplaryViewModel);
        }

        public static IViewModelLocator Current { get; private set; }

        public IMainViewModel MainViewModel { get; }

        public ExemplaryViewModel Exemplary { get; }

        public static void SetCurrentLocator(IViewModelLocator viewModelLocator)
        {
            Current = viewModelLocator;
        }
    }
}
