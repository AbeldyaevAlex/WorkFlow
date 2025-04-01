namespace Asu.Services.Customization
{
    using System;
    using System.Xml.Serialization;
    using System.IO;

    using Model = Core.Domain.Catalog;
    using Newtonsoft.Json;

    public static class ShopperApprovedReviewExtensions
    {
        public static T XmlDeserializeFromString<T>(this string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        public static object XmlDeserializeFromString(this string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }

        //public static T JsonDeserializeFromString<T>(this string objectData)
        //{
        //    return JsonConvert.DeserializeObject<T>(objectData);
        //}

        public static Model.ShopperApprovedReview ToReviewModel(this ShopperApprovedReview source)
        {
            return new Model.ShopperApprovedReview()
            {
                Id = int.Parse(source.Id),
                CustomerName = source.Name,
                DisplayDate = DateTime.Parse(source.DisplayDate),
                Comments = source.TextComments,
                Url = source.FullUrl,
                Overall = decimal.Parse(source.Overall),
                OrderId = source.OrderId
            };
        }
    }
}
