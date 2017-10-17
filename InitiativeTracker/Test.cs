using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiativeTracker
{
    public class Test : IComparable<Test>
    {
        public int MyProperty { get; set; }

        public int CompareTo(Test other)
        {
            throw new NotImplementedException();
        }
    }
}
