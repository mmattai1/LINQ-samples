<Query Kind="Expression">
  <Connection>
    <ID>36e897cc-cb07-416c-a8f7-9980167fcacb</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Syntax style for .Union() is (query).Union(query2).Union(queryn).OrderBy(firstsortfield).ThenBy(othersortfield)
//To get both albums with tracks and without tracks, --> use .Union()

//Rules
//Number of columns the same
//Column datatype is the same
//For ordering the Unioned queries use the name of the anonymous data fields
(from a in Albums
	where a.Tracks.Count() > 0
	select new
	{
		a.Title,
		Tracks = a.Tracks.Count(),
		AverageLengthA = a.Tracks.Average(t => t.Milliseconds) / 1000.0,
		AverageLengthB = a.Tracks.Sum(t => t.Milliseconds) / a.Tracks.Count() / 1000.0,
		AverageLengthC = (int)a.Tracks.Average(t => t.Milliseconds / 1000)
	}
).Union(
from a in Albums
	where a.Tracks.Count() == 0
	select new
	{
		a.Title,
		Tracks = 0,
		AverageLengthA = 0.0,
		AverageLengthB = 0.0,
		AverageLengthC = 0
	}
).OrderBy(o => o.Tracks).ThenBy(o => o.Title)
