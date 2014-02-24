using System;

namespace TestObjectFillerUnitTests.TestObjects
{
    public class TestDtoClass
    {
        public TestDtoClass()
        {
        }

        public string Name { get; set; }
        public string Title { get; private set; }
        public DateTime CreateDateTime { get; set; }
        public double Salary { get; set; }
        public int Position { get; set; }
        public bool Boolean { get; set; }
        public char Char { get; set; }
        public Decimal Decimal { get; set; }
        public object Object { get; set; }
        public long Long { get; set; }
        public SByte Sbyte { get; set; }
        public Single Single { get; set; }
    }
}