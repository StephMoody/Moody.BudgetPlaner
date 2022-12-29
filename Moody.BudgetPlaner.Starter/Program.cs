// See https://aka.ms/new-console-template for more information

using Moody.BudgetPlaner.Starter;
using Moody.WpfSandBox;

// await new Sample().SampleMethod().ConfigureAwait(true);
// Console.ReadKey();

// await new Sample().SampleMethod();

Thread.CurrentThread.Name = "MainThread";
BudgetPlanerStarter budgetPlanerStarter = new BudgetPlanerStarter();
await budgetPlanerStarter.Start();
await Task.Delay(10000);
budgetPlanerStarter.Stop();


