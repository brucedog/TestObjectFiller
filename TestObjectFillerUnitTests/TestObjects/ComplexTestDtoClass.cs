using System;
using System.Collections;
using System.Collections.Generic;

namespace TestObjectFillerUnitTests.TestObjects
{
    public class ComplexTestDtoClass
    {
        public TestDtoClass TestDtoClass { get; set; }

        public List<String> StringList { get; set; }
        
        public Dictionary<string, TestDtoClass> DictionaryTestDtoClasses { get; set; }
        
        public IList<TestDtoClass> ListTestDtoClass { get; set; }

        public IEnumerable<TestDtoClass> EnumerableTestDtoClasses { get; set; }

        public ArrayList ArrayListTestDtoClass { get; set; } 

        public Array[] Array { get; set; }
        
        // TODO cannot be done just yet
        //public List<List<TestDtoClass>> DoubleListTestDtoClass { get; set; }

        //public HashSet<TestDtoClass> HashSetTestDtoClasses { get; set; }

        //public Hashtable HashtableTestDtoClasses { get; set; }

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