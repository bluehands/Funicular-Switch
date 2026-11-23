namespace FunicularSwitch.Generators.Consumer.Net11.NonGenericResult;

//attribute on the non-generic type: Outcome<T> no longer derives from Outcome, so closing it is valid
[ResultType(ErrorType = typeof(string))]
public closed partial class Outcome;
