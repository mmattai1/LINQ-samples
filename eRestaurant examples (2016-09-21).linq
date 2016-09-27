<Query Kind="Program">
  <Connection>
    <ID>a955ba73-5713-4ec5-9085-8a23bad5ca90</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
	//A list of bill counts for all waiters
	//This query will create a flat dataset
	//The columns are native datatypes (ie int, string, double, ...)
	//One is not concerned with repeated data in a column
	//Instad of using an anonymous datatype (new{...}) we wish to use a defined class definition
	var BestWaiter = from w in Waiters
		select new WaiterBillCounts
		{
			Name = w.FirstName + " " + w.LastName,
			TCount = w.Bills.Count()
		};
	
	BestWaiter.Dump();

	var paramMonth = 5;
	var paramYear = 2014;

	var waiterBills = from w in Waiters
		where w.LastName.Contains("k")
		orderby w.LastName, w.FirstName
		select new WaiterBills
		{
			Name = w.LastName + ", " + w.FirstName,
			TotalBillCount = w.Bills.Count(),
			BillInfo = (from b in w.Bills
				where b.BillItems.Count() > 0
//					&& b.BillDate.Month = paramMonth
//					&& b.BillDate.Year = paramYear
				select new BillItemSummary
				{
					BillID = b.BillID,
					BillDate = b.BillDate,
					TableID = b.TableID,
					Total = b.BillItems.Sum(bi => bi.SalePrice * bi.Quantity)
				}).ToList()
		};
	
	waiterBills.Dump();
}

// Define other methods and classes here
//An example of a POCO class (flat)
public class WaiterBillCounts
{
	//Whatever the receiving field on your query in your Select there appears a property of that name in this class
	public string Name {get; set;}
	public int TCount {get; set;}
}

public class BillItemSummary
{
	public int BillID {get; set;}
	public DateTime BillDate {get; set;}
	public int? TableID {get; set;}
	public decimal Total {get; set;}
}

//An example of a DTO class (structure/collection)
public class WaiterBills
{
	public string Name {get; set;}
	public int TotalBillCount {get; set;}
	public List<BillItemSummary> BillInfo {get; set;}
}