Project Title

WDT assignment1-------Appointment Scheduling and Reservation system (ASR)


Contributors:

<Wenpeng Jiang - s3674270>
<Zhuojun Li - s3514856>

Design pattern:

The design pattern used in this solution, like Singleton and Factory, they helped the code to be structured in a more eligant way.Singleton is used for the instantiation, and it ensures better coordination with database. Factory takes part in the creation of objects through the interface and overcomes the tight coupling.

Description:

Dbconnection.cs

--the class contained connectionstring, Sql execute query and data set

Factory.cs

--the abstract class of factory

Program.cs

--the main menu and the two methods of listing

Staff.cs

--the class of Staff submenu and related functions

StaffFactory.cs

--the factory class of staff, to creat staff object

Student.cs

--the class of Student submenu and related functions

StudentFactory.cs

--the factory class of student, to creat staff object

User.cs

--the abstract class of user

UtilityTool.cs

--the methods of get input and validation related



