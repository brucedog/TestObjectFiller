using System;
using System.Collections;
using System.Collections.Generic;

namespace TestObjectFillerUnitTests.TestObjects
{
    public class ComplexTestDtoClass
    {
        public TestDtoClass TestDtoClass { get; set; }
        
        public List<TestDtoClass> ListTestDtoClass { get; set; }
        
        public ArrayList ArrayListTestDtoClass { get; set; } 

        public Array[] Array { get; set; }

        public IEnumerable<TestDtoClass>  EnumerableTestDtoClasses { get; set; }

        private MyInternalClass InternalClass { get; set; }

        private class MyInternalClass
        {
            public MyPrivateClass PrivateClass { get; set; }
        }

        internal class MyPrivateClass
        {
        }
    }
}