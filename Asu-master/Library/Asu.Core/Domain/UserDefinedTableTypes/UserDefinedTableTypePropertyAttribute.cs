namespace Asu.Core.Domain.UserDefinedTableTypes
{
    using System;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class UserDefinedTableTypePropertyAttribute : Attribute
    {
        public UserDefinedTableTypePropertyAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
