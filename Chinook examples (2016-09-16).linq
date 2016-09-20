<Query Kind="Expression">
  <Connection>
    <ID>36e897cc-cb07-416c-a8f7-9980167fcacb</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//List of all customers supported by the employee Jane Peacock
//	list LastName, FirstName, City, State, Phone, and Email
//	list alphabetically

//This sample requires a subset of the entity record
//The data needs to be filtered for aspecific select thus a wher is needed
//Using the navigation name on Customer, one can access the associated Employee record
//REMINDER: This is C# syntax and thus appropriate methods can be used .Equals()
from c in Customers
	where c.SupportRepIdEmployee.FirstName.Equals("Jane")
		&& c.SupportRepIdEmployee.LastName.Equals("Peacock")
	orderby c.LastName, c.LastName
	select new
	{
		Name = c.LastName + ", " + c.FirstName,
		c.City,
		c.State,
		c.Phone,
		c.Email
	}
;

//List all the Albums and the total number of tracks for that Album.
//	list albums alphabetically

//For aggregates it is best to consider doing parent -> child direction
//Aggregates are used against collections (multiple records)
//Count() count the number of instances of the collection referenced
from a in Albums
	orderby a.Title
	select new
	{
		a.Title,
		Tracks = a.Tracks.Count()
	}
;

//Find the total price for each set of Album Tracks

//Null error could occur if a collection is empty for specific aggregates such as Sum() thus you may need to filter (Where) certain records from your query
//Sum() totals a specific field/expression, thus you will likely need to use a delegate to indicate the collection instance attribute to be used
from a in Albums
	orderby a.Title
	where a.Tracks.Count() > 0
	select new
	{
		a.Title,
		Tracks = a.Tracks.Count(),
		Cost = a.Tracks.Sum(t => t.UnitPrice)
	}
;

//Find the average length of the Album tracks in seconds

//Average() averages a specific field/expression, thus you will likely need to use a delegate to indicate the collection instance attribute to be used
from a in Albums
	orderby a.Title
	where a.Tracks.Count() > 0
	select new
	{
		a.Title,
		AverageLengthA = a.Tracks.Average(t => t.Milliseconds) / 1000,
		AverageLengthB = a.Tracks.Sum(t => t.Milliseconds) / a.Tracks.Count() / 1000,
		AverageLengthC = a.Tracks.Average(t => t.Milliseconds / 1000)
	}
;

//The most popular Media Type for the Tracks (which MediaType has the most tracks)
from m in MediaTypes
	where m.Tracks.Count() == (from t in MediaTypes select t.Tracks.Count()).Max()
	select new
	{
		m.Name,
		Max = m.Tracks.Count()
	}
