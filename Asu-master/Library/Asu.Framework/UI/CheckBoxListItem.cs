namespace Asu.Framework.UI
{
    /// <summary>
    /// WC. Item of custom CheckBoxList
    /// </summary>
    public class CheckBoxListItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string RouteName { get; set; }
        public string SeName { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsTop { get; set; }
    }
}