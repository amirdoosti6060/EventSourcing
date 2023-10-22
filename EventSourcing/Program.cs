// Create a new account
using EventSourcing;

var accountId = Guid.NewGuid();
var account = new BankAccount(accountId);

Console.WriteLine("Doing some transactions ...");
// Deposit and withdraw funds
account.Deposit(100);
account.Withdraw(50);
account.Deposit(20);
account.Withdraw(10);
account.Withdraw(5);
account.Withdraw(3);
account.Withdraw(2);

// The main account now balance
Console.WriteLine("Main Account Balance: " + account.Balance); // Should be 50

// Save the events to the event store
var eventStore = new EventStore();
eventStore.SaveEvents(accountId, account.GetChanges());

Console.WriteLine("\nEvents available in EventStore ...");
var events = eventStore.GetEvents(accountId);
foreach (object item in events)
{
    Console.WriteLine(item.ToString());
}


Console.WriteLine("\nApplying events on a new account ...");
// Load the account's history from the event store
var history = eventStore.GetEvents(accountId);
var loadedAccount = BankAccount.LoadFromHistory(accountId, history);

// The loaded account now reflects the state after replaying events
Console.WriteLine("New Account Balance: " + loadedAccount.Balance); // Should be 50
