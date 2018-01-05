using System.Globalization;

namespace StrongBeaver.Core.Services.Localisation
{
    public interface ILocalisationService : IService
    {
        CultureInfo GetCurrentCultureInfo();

        void SetCulture(CultureInfo cultureInfo);
    }
}
