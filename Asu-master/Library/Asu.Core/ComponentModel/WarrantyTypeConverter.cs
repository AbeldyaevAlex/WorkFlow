namespace Asu.Core.ComponentModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    using Asu.Core.Domain.Warranty;

    public class WarrantyTypeConverter : TypeConverter 
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                Dictionary<int, int> result = null;
                var valueStr = value as string;
                if (!string.IsNullOrEmpty(valueStr))
                {
                    try
                    {
                        List<WarrantyProductAssociation> output;
                        using (var tr = new StringReader(valueStr))
                        {
                            var xmlS = new XmlSerializer(typeof(List<WarrantyProductAssociation>));
                            output = (List<WarrantyProductAssociation>)xmlS.Deserialize(tr);
                        }

                        result = output.ToDictionary(m => m.ProductId, m => m.WarrantyProductId);
                    }
                    catch
                    {
                        //xml error
                    }
                }

                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var output = value as Dictionary<int, int>;
                if (output != null)
                {
                    var items = output.Select(m => new WarrantyProductAssociation { ProductId = m.Key, WarrantyProductId = m.Value }).ToList();
                    var sb = new StringBuilder();
                    using (var tw = new StringWriter(sb))
                    {
                         var xmlS = new XmlSerializer(typeof(List<WarrantyProductAssociation>));
                        xmlS.Serialize(tw, items);

                        return sb.ToString();
                    }
                }

                return string.Empty;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
