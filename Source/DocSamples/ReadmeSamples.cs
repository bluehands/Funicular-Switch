using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using FunicularSwitch;

namespace DocSamples
{
    class ResultCreation
    {
        [Runner("resultCreation")]
        public static void CreateResult()
        {
            #region resultCreation
            //Ok result:
            var fortyTwo = Result.Ok(42);
            //or using implicit cast operator
            Result<string> ok = "Ok";

            //Error result:
            var error = Result.Error<int>("Could not find the answer");
            #endregion
        }

        [Runner("map")]
        public static void Map()
        {
            #region map
            static Result<int> Ask() => 42;

            Result<int> answerTransformed = Ask()
                .Map(answer => answer * 2);

            Console.WriteLine(answerTransformed);
            #endregion
        }

        [Runner("bind")]
        public static void Bind()
        {
            #region bind
            static Result<int> Ask() => 42;

            Result<int> answerTransformed = Ask()
                .Bind(answer => answer == 0 ? Result.Error<int>("Division by zero") : 42 / answer);

            Console.WriteLine(answerTransformed);
            #endregion
        }

        [Runner("errorPropagation")]
        public static void ErrorPropagation()
        {
            #region errorPropagation

            static Result<int> Transform(Result<int> result) =>
                result
                    .Bind(answer => answer == 0 ? Result.Error<int>("Division by zero") : 42 / answer)
                    .Map(transformed => transformed * 2);

            Result<int> firstLevelError = Transform(Result.Error<int>("I don't know"));
            Console.WriteLine($"First level: {firstLevelError}");

            Result<int> secondLevelError = Transform(Result.Ok(0));
            Console.WriteLine($"Second level: {secondLevelError}");
            #endregion
        }

        [Runner("match")]
        public static void Match()
        {
            #region match
            static Result<int> Ask() => 42;

            string whatIsIt =
                Ask().Match(
                    answer => $"The answer is: {answer}",
                    error => $"Ups: {error}"
                );

            Console.WriteLine(whatIsIt);
            #endregion
        }
    }

    class ReadmeSamples
    {
        [Runner("fruitSalad")]
        #region fruitSalad
        public static async Task FruitSalad()
        {
            var stock = ImmutableList.Create(
                new Fruit("Orange", 155),
                new Fruit("Orange", 12),
                new Fruit("Apple", 132),
                new Fruit("Stink fruit", 1));

            var ingredients = ImmutableList.Create("Apple", "Banana", "Pear", "Stink fruit");

            const int cookSkillLevel = 3;

            static IEnumerable<string> CheckFruit(Fruit fruit)
            {
                if (fruit.AgeInDays > 20)
                    yield return $"{fruit.Name} is not fresh";

                if (fruit.Name == "Stink fruit")
                    yield return "Stink fruit, I do not serve that";
            }

            var salad =
                await ingredients.Select(ingredient =>
                        stock
                            .Where(fruit => fruit.Name == ingredient)
                            .FirstOk(CheckFruit, onEmpty: () => $"No {ingredient} in stock")
                    )
                    .Aggregate()
                    .Bind(fruits => CutIntoPieces(fruits, cookSkillLevel))
                    .Map(Serve);

            Console.WriteLine(salad.Match(ok => "Salad served successfully!", error => $"No salad today:{Environment.NewLine}{error}"));
        }

        static Result<Salad> CutIntoPieces(IEnumerable<Fruit> fruits, int skillLevel = 5)
        {
            try
            {
                return CutFruits(fruits, skillLevel);
            }
            catch (Exception e)
            {
                return Result.Error<Salad>($"Ouch: {e.Message}");
            }
        }

        static Salad CutFruits(IEnumerable<Fruit> fruits, int skillLevel) => skillLevel > 5 ? new Salad(fruits) : throw new Exception("Cut my fingers");
        static Task<Salad> Serve(Salad salad) => Task.FromResult(new Salad(salad.Fruits, true));

        class Salad
        {
            public IReadOnlyCollection<Fruit> Fruits { get; }
            public bool Served { get; }

            public Salad(IEnumerable<Fruit> fruits, bool served = false)
            {
                Fruits = fruits.ToList();
                Served = served;
            }
        }

        class Fruit
        {
            public string Name { get; }
            public int AgeInDays { get; }

            public Fruit(string name, int ageInDays)
            {
                Name = name;
                AgeInDays = ageInDays;
            }
        }
        #endregion
    }
}