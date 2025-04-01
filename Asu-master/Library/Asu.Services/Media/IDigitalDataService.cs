namespace Asu.Services.Media
{
    using Asu.Core.Domain.ProductGroups;

    public interface IDigitalDataService
    {
        DigitalData GetById(int id);

        string GetUrl(int id);

        string GetUrl(DigitalData digitalData);

        string GetThumbUrl(DigitalData digitalData, int maxWidth = 0, int maxHeight = 0);

        string GetDefaultPictureUrl();
    }
}
