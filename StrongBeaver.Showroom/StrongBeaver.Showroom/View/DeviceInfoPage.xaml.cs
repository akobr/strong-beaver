using StrongBeaver.Core.Constants;
using StrongBeaver.Core.Platform;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StrongBeaver.Showroom.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceInfoPage : ContentPage
    {
        public DeviceInfoPage()
        {
            InitializeComponent();
            InitialiseInfo();
        }

        private void InitialiseInfo()
        {
            TableRoot tableRoot = tableInfo.Root;
            tableRoot.Clear();

            IEnvironmentInfo environmentInfo = App.EnvironmentInfo;

            tableRoot.Add(CreateDeviceInfoSection(environmentInfo.Device));
            tableRoot.Add(CreatePlatformInfoSection(environmentInfo.Platform));
            tableRoot.Add(CreateApplicationInfoSection(environmentInfo.Application));
        }

        private TableSection CreateDeviceInfoSection(IDeviceInfo info)
        {
            TableSection section = new TableSection("Device info");

            section.Add(new TextCell() { Text = "Manufacturer", Detail = info.Manufacturer });
            section.Add(new TextCell() { Text = "Model", Detail = info.Model });
            section.Add(new TextCell() { Text = "Family", Detail = info.Family });
            section.Add(new TextCell() { Text = "Identifier", Detail = info.Identifier.ToString(GlobalConstatns.DEFAULT_GUID_STRING_FORMAT) });

            return section;
        }

        private TableSection CreatePlatformInfoSection(IPlatformInfo info)
        {
            TableSection section = new TableSection("Platform info");

            section.Add(new TextCell() { Text = "Platform name", Detail = info.PlatformName });
            section.Add(new TextCell() { Text = "OS manufacturer", Detail = info.OsManufacturer });
            section.Add(new TextCell() { Text = "OS name", Detail = info.OsName });
            section.Add(new TextCell() { Text = "OS version", Detail = info.OsVersion.ToString() });

            return section;
        }

        private TableSection CreateApplicationInfoSection(IApplicationInfo info)
        {
            TableSection section = new TableSection("Application info");

            section.Add(new TextCell() { Text = "Application name", Detail = info.Name });
            section.Add(new TextCell() { Text = "Application identifier", Detail = info.Identifier.ToString(GlobalConstatns.DEFAULT_GUID_STRING_FORMAT) });

            return section;
        }
    }
}