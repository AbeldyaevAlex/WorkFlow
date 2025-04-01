namespace Asu.Core.Domain.UserDefinedTableTypes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class UserDefinedTableTypeAttribute : Attribute
    {
        public UserDefinedTableTypeAttribute(string type)
        {
            this.Type = type;
        }

        public string Type { get; private set; }
    }
}
