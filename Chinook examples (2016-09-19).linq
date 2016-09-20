<Query Kind="Statements">
  <Connection>
    <ID>36e897cc-cb07-416c-a8f7-9980167fcacb</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//The most popular Media Type for the Tracks (which MediaType has the most tracks)

//A statement has a receiving variable which is set by the results of a query
//When you need multiple steps to solve a problem, switch your language choice to either Statement(s) or Program

var maxCount = (from m in MediaTypes
	select m.Tracks.Count()).Max();

//To display the contents of a variable in LinqPad you use the method .Dump()
maxCount.Dump();

//To filter data you can use the Where clause
var mediaTypeCounts = from m in MediaTypes
	where m.Tracks.Count() == maxCount
	select new
	{
		Name = m.Name,
		TrackCount = m.Tracks.Count()
	};
mediaTypeCounts.Dump();

//Can this set of statements be written as one complete query?
//The answer: possibly; and in this case yes
//In this example maxCount could be exchanged for the query that actually created the value in the first place
//This substitution query is a nested query (subquery)
//The nested query needs its own instance identifier
var mediaTypeCountsNested = from m in MediaTypes
	where m.Tracks.Count() == (from t in MediaTypes select t.Tracks.Count()).Max()
	select new
	{
		Name = m.Name,
		TrackCount = m.Tracks.Count()
	};
mediaTypeCountsNested.Dump();

//Using a method syntax to determine the count value for the where expression
//This demonstrates that queries can be constructed using both query syntax and method syntax
var mediaTypeCountsMethod = from m in MediaTypes
	where m.Tracks.Count() == MediaTypes.Select(t => t.Tracks.Count()).Max()
	select new
	{
		Name = m.Name,
		TrackCount = m.Tracks.Count()
	};
mediaTypeCountsMethod.Dump();
