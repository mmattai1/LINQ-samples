<Query Kind="Statements">
  <Connection>
    <ID>5e95376f-67d2-4ffc-a0c3-809b933667a9</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//Find the waiter with the most bills
var mostBillsWaiter = from w in Waiters
	where w.Bills.Count() == (from b in Waiters select b.Bills.Count()).Max()
	select new
	{
		Name = w.FirstName + " " + w.LastName,
		MostBillsCount = w.Bills.Count()
	};
mostBillsWaiter.Dump();

//Create a dataset that has an unknown number of records associated with a data value

//A list of all bills associated with the waiter
//List all waiters

//The inner nested query uses the associated Bill records of the currently processing Waiter -- x.Collection
var waiterBills = from w in Waiters
	orderby w.LastName, w.FirstName
	select new
	{
		Name = w.LastName + ", " + w.FirstName,
		TotalBillCount = w.Bills.Count(),
		BillInfo = (from b in w.Bills
			where b.BillItems.Count() > 0
			select new
				{
					BillID = b.BillID,
					BillDate = b.BillDate,
					TableID = b.TableID,
					Total = b.BillItems.Sum(bi => bi.SalePrice * bi.Quantity)
				})
	};
waiterBills.Dump();
