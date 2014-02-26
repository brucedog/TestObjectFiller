## Test Object Filler ## 

## Project Description ##
Library designed to auto fill DTOs (data transfer objects) properties. THIS IS AN ALPHA VERSION.

## Overview ##
Test Object Filler was intended to be used with a mocking framework such a [Rhino Mocks](http://hibernatingrhinos.com/oss/rhino-mocks) so that a mocked method would return a object filled with "data". 


## Example ##

Basic test that checks the properties for data.
 
```csharp
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
```

## Plan Features ##
* Ability to add rules on how properties are populated.