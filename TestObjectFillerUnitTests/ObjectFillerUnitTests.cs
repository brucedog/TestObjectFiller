using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestObjectFiller;
using TestObjectFillerUnitTests.TestObjects;

namespace TestObjectFillerUnitTests
{
    [TestClass]
    public class ObjectFillerUnitTests
    {
        /// <summary>
        /// Fills object with basic properties
        /// </summary>
        [TestMethod]
        public void ObjectFiller_FillComplexTypes()
        {
            ComplexTestDtoClass objectToFill = new ComplexTestDtoClass();

            objectToFill = ObjectFiller.FillThisObject(objectToFill, objectToFill.GetType());

            Assert.IsTrue(objectToFill.Array.Length > 0);

            Assert.IsTrue(objectToFill.TestDtoClass.GetType() == typeof(TestDtoClass));


            Assert.AreNotEqual(objectToFill.TestDtoClass.Name, string.Empty);
            Assert.AreNotEqual(objectToFill.TestDtoClass.Title, string.Empty);
            Assert.AreNotEqual(objectToFill.TestDtoClass.Char, string.Empty);
            Assert.AreNotEqual(objectToFill.TestDtoClass.Object, null);

            Assert.AreEqual(objectToFill.TestDtoClass.Char, 'x');
            Assert.AreEqual(objectToFill.TestDtoClass.Sbyte, SByte.MaxValue);
            Assert.AreEqual(objectToFill.TestDtoClass.Single, Single.MaxValue);
            Assert.AreEqual(objectToFill.TestDtoClass.Long, long.MaxValue);

            Assert.IsTrue(objectToFill.TestDtoClass.Position > 0);
            Assert.IsTrue(objectToFill.TestDtoClass.Salary > 0.0);
            Assert.IsTrue(objectToFill.TestDtoClass.Long > 0.0);
            Assert.IsTrue(objectToFill.TestDtoClass.Boolean);  
        }

        /// <summary>
        /// Fills object with basic properties
        /// </summary>
        [TestMethod]
        public void ObjectFiller_FillBasicTypes()
        {
            TestDtoClass objectToFill = new TestDtoClass();
            objectToFill = ObjectFiller.FillThisObject(objectToFill, objectToFill.GetType());

            Assert.AreNotEqual(objectToFill.Name, string.Empty);
            Assert.AreNotEqual(objectToFill.Title, string.Empty);
            Assert.AreNotEqual(objectToFill.Char, string.Empty);
            Assert.AreNotEqual(objectToFill.Object, null);

            Assert.AreEqual(objectToFill.Char, 'x');
            Assert.AreEqual(objectToFill.Sbyte, SByte.MaxValue);
            Assert.AreEqual(objectToFill.Single, Single.MaxValue);
            Assert.AreEqual(objectToFill.Long, long.MaxValue);

            Assert.IsTrue(objectToFill.Position > 0);
            Assert.IsTrue(objectToFill.Salary > 0.0);
            Assert.IsTrue(objectToFill.Long > 0.0);
            Assert.IsTrue(objectToFill.Boolean);       
        }

        /// <summary>
        /// Fills list of objects with basic properties
        /// </summary>
        [TestMethod]
        public void ObjectFiller_FillListBasicTypes()
        {
            List<TestDtoClass> objectsToFill = new List<TestDtoClass>{new TestDtoClass(), new TestDtoClass(), new TestDtoClass()};
            foreach (var blankObject in objectsToFill)
            {
                ObjectFiller.FillThisObject(blankObject, blankObject.GetType());
            }

            foreach (var filledobject in objectsToFill)
            {
                Assert.AreNotEqual(filledobject.Name, string.Empty);
                Assert.AreNotEqual(filledobject.Title, string.Empty);

                Assert.IsTrue(filledobject.Position > 0);
                Assert.IsTrue(filledobject.Salary > 0.0);
            }
        }
    }
}
