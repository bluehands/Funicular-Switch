#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	internal static partial class CardTypeMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.CardType cardType, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.FemaleCardType, T> female, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.MaleCardType, T> male) =>
		cardType switch
		{
			FunicularSwitch.Generators.Consumer.CardType.FemaleCardType female1 => female(female1),
			FunicularSwitch.Generators.Consumer.CardType.MaleCardType male2 => male(male2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.CardType: {cardType.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.CardType cardType, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.FemaleCardType, global::System.Threading.Tasks.Task<T>> female, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.MaleCardType, global::System.Threading.Tasks.Task<T>> male) =>
		cardType switch
		{
			FunicularSwitch.Generators.Consumer.CardType.FemaleCardType female1 => female(female1),
			FunicularSwitch.Generators.Consumer.CardType.MaleCardType male2 => male(male2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.CardType: {cardType.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.CardType> cardType, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.FemaleCardType, T> female, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.MaleCardType, T> male) =>
		(await cardType.ConfigureAwait(false)).Match(female, male);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.CardType> cardType, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.FemaleCardType, global::System.Threading.Tasks.Task<T>> female, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.MaleCardType, global::System.Threading.Tasks.Task<T>> male) =>
		await (await cardType.ConfigureAwait(false)).Match(female, male).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.CardType cardType, global::System.Action<FunicularSwitch.Generators.Consumer.CardType.FemaleCardType> female, global::System.Action<FunicularSwitch.Generators.Consumer.CardType.MaleCardType> male)
		{
			switch (cardType)
			{
				case FunicularSwitch.Generators.Consumer.CardType.FemaleCardType female1:
					female(female1);
					break;
				case FunicularSwitch.Generators.Consumer.CardType.MaleCardType male2:
					male(male2);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.CardType: {cardType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.CardType cardType, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.FemaleCardType, global::System.Threading.Tasks.Task> female, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.MaleCardType, global::System.Threading.Tasks.Task> male)
		{
			switch (cardType)
			{
				case FunicularSwitch.Generators.Consumer.CardType.FemaleCardType female1:
					await female(female1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.CardType.MaleCardType male2:
					await male(male2).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.CardType: {cardType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.CardType> cardType, global::System.Action<FunicularSwitch.Generators.Consumer.CardType.FemaleCardType> female, global::System.Action<FunicularSwitch.Generators.Consumer.CardType.MaleCardType> male) =>
		(await cardType.ConfigureAwait(false)).Switch(female, male);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.CardType> cardType, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.FemaleCardType, global::System.Threading.Tasks.Task> female, global::System.Func<FunicularSwitch.Generators.Consumer.CardType.MaleCardType, global::System.Threading.Tasks.Task> male) =>
		await (await cardType.ConfigureAwait(false)).Switch(female, male).ConfigureAwait(false);
	}
	
	abstract partial record CardType
	{
		public static FunicularSwitch.Generators.Consumer.CardType Female() => new FunicularSwitch.Generators.Consumer.CardType.FemaleCardType();
		public static FunicularSwitch.Generators.Consumer.CardType Male(int Age) => new FunicularSwitch.Generators.Consumer.CardType.MaleCardType(Age);
	}
}
#pragma warning restore 1591
