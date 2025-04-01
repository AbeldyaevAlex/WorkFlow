using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Data
{
    public class test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<test> GetTestList { get; set; }
    }
    public class Test2ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}