namespace StrongBeaver.Core.Services.Permissions
{
    public enum PermissionTypeEnum
    {
        /// <summary>
        /// The unknown permission only used for return type, never requested.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Android: Calendar.
        /// iOS: Calendar (Events).
        /// UWP: Nothing.
        /// </summary>
        Calendar,
        /// <summary>
        /// Android: Camera.
        /// iOS: Photos (Camera Roll and Camera).
        /// UWP: Nothing.
        /// </summary>
        Camera,
        /// <summary>
        /// Android: Contacts.
        /// iOS: AddressBook.
        /// UWP: ContactManager.
        /// </summary>
        Contacts,
        /// <summary>
        /// Android: Fine and Coarse Location.
        /// iOS: CoreLocation (Always and WhenInUse).
        /// UWP: Geolocator.
        /// </summary>
        Location,
        /// <summary>
        /// Android: Microphone.
        /// iOS: Microphone.
        /// UWP: Nothing.
        /// </summary>
        Microphone,
        /// <summary>
        /// Android: Phone.
        /// iOS: Nothing.
        /// UWP: Nothing.
        /// </summary>
        Phone,
        /// <summary>
        /// Android: Nothing
        /// iOS: Photos
        /// UWP: Nothing
        /// </summary>
        Photos,
        /// <summary>
        /// Android: Nothing.
        /// iOS: Reminders.
        /// UWP: Nothing.
        /// </summary>
        Reminders,
        /// <summary>
        /// Android: Body Sensors.
        /// iOS: CoreMotion.
        /// UWP: DeviceAccessInformation.
        /// </summary>
        Sensors,
        /// <summary>
        /// Android: Sms.
        /// iOS: Nothing.
        /// UWP: Nothing.
        /// </summary>
        Sms,
        /// <summary>
        /// Android: External Storage.
        /// iOS: Nothing.
        /// </summary>
        Storage,
        /// <summary>
        /// Android: Microphone.
        /// iOS: Speech.
        /// UWP: Nothing.
        /// </summary>
        Speech,
        /// <summary>
        /// Android: Fine and Coarse Location.
        /// iOS: CoreLocation - Always.
        /// UWP: Geolocator.
        /// </summary>
        LocationAlways,
        /// <summary>
        /// Android: Fine and Coarse Location.
        /// iOS: CoreLocation - WhenInUse.
        /// UWP: Geolocator.
        /// </summary>
        LocationWhenInUse,
        /// <summary>
        ///  Android: None.
        ///  iOS: MPMediaLibrary.
        ///  UWP: None.
        /// </summary>
        MediaLibrary,
    }
}