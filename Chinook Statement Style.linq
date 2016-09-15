<Query Kind="Statements">
  <Connection>
    <ID>36e897cc-cb07-416c-a8f7-9980167fcacb</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//use of the statement environment allows for C# type commands
//you can have local variables
//you can have multiple statements in your execution
//to display the contents of a variable you will use the LINQPad method .Dump()
var theResults = from x in Albums
	where x.ReleaseYear == 2008
	orderby x.Artist.Name, x.Title
	select new
	{
		x.Artist.Name, x.Title
	};
theResults.Dump();

//list all albums which contains the string "son"
//consider using the method .Contains()
var theResults = from x in Albums
	where x.Title.Contains("son")
	select x;
theResults.Dump();
