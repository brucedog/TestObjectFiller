Object Filler
================

Project Description
Library is used to fill object's properties with test data.

Overview 

Example:

TestDtoClass objectToFill = new TestDtoClass();

objectToFill = ObjectFiller.FillThisObject(objectToFill, objectToFill.GetType());
objectToFill now has all its properties filled with data 