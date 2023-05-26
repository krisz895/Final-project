using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LegyenOnIsMilliomos
{
    public class Question
    {
        public string Text { get; set; } // A kérdés szövege: Sós Róbert
        public List<string> Options { get; set; } // A válaszlehetőségek listája: Sós Róbert
        public int CorrectOptionIndex { get; set; } // A helyes válasz indexe a válaszlehetőségek között: Sós Róbert
        public int Points { get; set; } // A kérdéshez tartozó pontszám: Sós Róbert

        public Question(string text, List<string> options, int correctOptionIndex, int points)
        {
            Text = text;
            Options = options;
            CorrectOptionIndex = correctOptionIndex;
            Points = points;
        }

        public bool IsCorrect(int selectedOptionIndex)
        {
            return selectedOptionIndex == CorrectOptionIndex; // Ellenőrzi, hogy a választott válasz helyes-e: Ősz Krisztián
        }

        public List<string> GetRandomOptions(int count)
        {
            var randomOptions = new List<string>();
            randomOptions.Add(Options[CorrectOptionIndex]); // Helyes válasz hozzáadása az opciók közé: Ősz Krisztián

            var random = new Random();
            while (randomOptions.Count < count)
            {
                var randomIndex = random.Next(Options.Count);
                var randomOption = Options[randomIndex];
                if (!randomOptions.Contains(randomOption))
                {
                    randomOptions.Add(randomOption); // Véletlenszerűen választott opciók hozzáadása, amíg elérik a kívánt számot: Sós Róbert
                }
            }

            return randomOptions.OrderBy(o => Guid.NewGuid()).ToList(); // Véletlenszerűen keveri az opciókat és visszaadja a listát: Ősz Krisztián, Sós Róbert
        }

        public List<int> GetAudienceVotes()
        {
            var audienceVotes = new List<int>();
            var random = new Random();

            for (int i = 0; i < Options.Count; i++)
            {
                if (i == CorrectOptionIndex)
                {
                    audienceVotes.Add(random.Next(40, 60)); // Közönség véleményének szimulálása, nagyobb valószínűséggel választják a helyes választ: Ősz Krisztián, Sós Róbert
                }
                else
                {
                    audienceVotes.Add(random.Next(0, 20)); // Közönség véleményének szimulálása, kisebb valószínűséggel választják a helytelen válaszokat: Ősz Krisztián, Sós Róbert
                }
            }

            return audienceVotes;
        }

        public string GetFriendOpinion()
        {
            var opinions = new List<string>
            {
                "Szerintem a válasz A.",
                "Biztos vagyok benne, hogy a válasz B.",
                "A válasz C-re tippelek.",
                "Nagyon úgy néz ki, hogy a válasz D a helyes."
            };

            return opinions[CorrectOptionIndex]; // Barát véleményének visszaadása a helyes válasz alapján: Ősz Krisztián
        }
    }
}
