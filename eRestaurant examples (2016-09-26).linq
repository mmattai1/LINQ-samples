<Query Kind="Expression">
  <Connection>
    <ID>a955ba73-5713-4ec5-9085-8a23bad5ca90</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//Multiple Column Groups

from food in Items
	group food by food.MenuCategoryID
;

//Grouping data placed in a local temp data set for further processing
//.Key allows you to have access to the value(s) in your group key(s)
//If you have multiple group columns they MUST be in an anonymous datatype
//To create a DTO-type collection you can use .ToList() on the temp data set
//You can have a custom anonymous data collection by using a nested query

//Step A: See what the grouping looks like
from food in Items
	group food by new {food.MenuCategoryID, food.CurrentPrice}
;

//Step B: DTO-style dataset using ToList()
from food in Items
	group food by new {food.MenuCategoryID, food.CurrentPrice} into tempDataset
	select new
	{
		MenuCategoryID = tempDataset.Key.MenuCategoryID,
		CurrentPrice = tempDataset.Key.CurrentPrice,
		FoodItems = tempDataset.ToList()
	}
;

//Step C: DTO-style custom dataset
//Use a nested query for the FoodItems
from food in Items
	group food by new {food.MenuCategoryID, food.CurrentPrice} into tempDataset
	select new
	{
		MenuCategoryID = tempDataset.Key.MenuCategoryID,
		CurrentPrice = tempDataset.Key.CurrentPrice,
		FoodItems = from x in tempDataset
			select new
			{
				ItemID = x.ItemID,
				FoodDescription = x.Description,
				TimesServed = x.BillItems.Count()
			}
	}
