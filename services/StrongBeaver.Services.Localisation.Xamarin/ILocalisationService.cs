using System.Globalization;
using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Localisation
{
    public interface ILocalisationService : IService
    {
        CultureInfo GetCurrentCultureInfo();

        void SetCulture(CultureInfo cultureInfo);
    }
}
