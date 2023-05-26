using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LegyenOnIsMilliomos
{
    public partial class MainWindow : Window
    {
        //A kérdések listája: Sós Róbert
        private List<Question> questions;
        //Az aktuális kérdés: Sós Róbert
        private Question currentQuestion;
        //Az aktuális kérdés indexe a listában: Sós Róbert
        private int currentQuestionIndex;
        //A nyeremény összege: Sós Róbert
        private int money;
        //A segítségek listája, amelyeket már felhasználtak: Sós Róbert
        private List<string> usedHelps;

        public MainWindow()
        {
            InitializeComponent();
            InitializeQuestions(); //Kérdések inicializálása: Sós Róbert
            StartNewGame(); //Új játék indítása: Sós Róbert
        }

        //Kérdések inicializálása: Sós Róbert
        private void InitializeQuestions()
        {
            //Kérdések létrehozása és hozzáadása a listához: Ősz Krisztián
            questions = new List<Question>
            {
                new Question("Mi a fővárosa Magyarországnak?", new List<string> { "A. Budapest", "B. Bécs", "C. Prága", "D. Varsó" }, 0, 100),
                new Question("Mi a világ legmagasabb hegye?", new List<string> { "A. Mount Everest", "B. K2", "C. Kilimandzsáró", "D. Makalu" }, 0, 200),
                new Question("Ki festette a Mona Lisát?", new List<string> { "A. Leonardo da Vinci", "B. Pablo Picasso", "C. Vincent van Gogh", "D. Michelangelo" }, 0, 300),
                new Question("Melyik évben történt az első Holdra szállás?", new List<string> { "A. 1969", "B. 1965", "C. 1972", "D. 1975" }, 0, 500),
                new Question("Mi a világ legnagyobb óceánja?", new List<string> { "A. Atlanti-óceán", "B. Csendes-óceán", "C. Indiai-óceán", "D. Északi-sarki-óceán" }, 1, 750),
                new Question("Mi a leghosszabb folyó a világon?", new List<string> { "A. Nílus", "B. Amazonas", "C. Mississippi", "D. Jangce" }, 1, 1000),
                new Question("Hol rendezték a nyári olimpiai játékokat 2000-ben?", new List<string> { "A. Atlanta", "B. Athén", "C. Sydney", "D. London" }, 2, 250),
                new Question("A 95. Oscar gálán hány kategóriában jelölték a Fekete Párduc 2. c. filmet?", new List<string> { "A. 2", "B. 4", "C. 3", "D. 5" }, 3, 800),
                new Question("Milyen színek találhatóak meg a Jamaicai zászlóban?", new List<string> { "A. sárga, piros, fekete", "B. piros, sárga, zöld", "C. zöld, piros, fekete", "D. zöld, fekete, sárga" }, 3, 650),
                new Question("Melyik évben jelent meg a Microsoft Windows XP?", new List<string> { "A. 2000", "B. 2001", "C. 1999", "D. 2002" }, 1, 450)
            };
        }
        //Új játék indítása: Sós Róbert
        private void StartNewGame()
        {
            currentQuestionIndex = 0;
            money = 0;
            usedHelps = new List<string>();
            questions = questions.OrderBy(q => Guid.NewGuid()).ToList(); //A kérdések véletlenszerű sorrendbe rendezése: Sós Róbert
            LoadQuestion(); //Kérdés betöltése: Sós Róbert
        }

        //Kérdés betöltése: Ősz Krisztián
        private void LoadQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                currentQuestion = questions[currentQuestionIndex];
                questionTextBlock.Text = currentQuestion.Text; //Kérdés szövegének beállítása: Ősz Krisztián
                optionsListBox.ItemsSource = currentQuestion.Options; //Válaszlehetőségek beállítása: Ősz Krisztián
                moneyTextBlock.Text = $"Nyeremény: {money} Ft"; //Nyeremény szövegének beállítása: Ősz Krisztián
                UpdateHelpButtons(); //Segítség gombok állapotának frissítése: Ősz Krisztián
            }
            else
            {
                EndGame(); //Játék vége: Ősz Krisztián
            }
        }

        //Válasz ellenőrzése: Sós Róbert
        private void CheckAnswer(string selectedOption)
        {
            int selectedOptionIndex = optionsListBox.Items.IndexOf(selectedOption);
            if (currentQuestion.IsCorrect(selectedOptionIndex)) //Ellenőrzés, hogy helyes-e a válasz: Sós Róbert
            {
                money += currentQuestion.Points; //Nyeremény növelése: Sós Róbert
                resultTextBlock.Text = "Helyes válasz!"; //Eredmény szövegének beállítása: Sós Róbert
                resultTextBlock.Foreground = Brushes.Green; //Eredmény színének beállítása zöldre: Sós Róbert
            }
            else
            {
                EndGame(); //Játék vége: Sós Róbert
            }

            nextButton.IsEnabled = true; //Következő gomb engedélyezése: Ősz Krisztián
            optionsListBox.IsEnabled = false; //Válaszlehetőségek tiltása: Ősz Krisztián
            moneyTextBlock.Text = $"Nyeremény: {money} Ft"; //Nyeremény szövegének frissítése: Ősz Krisztián
            UpdateHelpButtons(); //Segítség gombok állapotának frissítése: Ősz Krisztián
        }

        //Következő kérdés: Ősz Krisztián
        private void NextQuestion()
        {
            currentQuestionIndex++;
            resultTextBlock.Text = ""; //Eredmény szövegének törlése: Ősz Krisztián
            nextButton.IsEnabled = false; //Következő gomb tiltása: Ősz Krisztián
            optionsListBox.IsEnabled = true; //Válaszlehetőségek engedélyezése: Ősz Krisztián
            LoadQuestion(); //Kérdés betöltése: Ősz Krisztián
        }

        //Játék vége: Sós Róbert
        private void EndGame()
        {
            if (currentQuestionIndex >= 10)
            {
                MessageBox.Show($"Gratulálok ön nyert {money} Ft-ot, a játék ezzel véget ért!");
                StartNewGame();
            }
            else
            {
                MessageBox.Show($"Sajnos vesztettél! Nyertél {money} Ft-ot!"); //Eredmény kiírása üzenetablakban: Sós Róbert
                StartNewGame(); //Új játék indítása: Sós Róbert
            } 
        }

        //Segítség gombok állapotának frissítése: Ősz Krisztián
        private void UpdateHelpButtons()
        {
            fiftyFiftyButton.IsEnabled = !usedHelps.Contains("FiftyFifty"); //"Fifty-Fifty" segítség engedélyezése vagy tiltása: Ősz Krisztián
            audienceButton.IsEnabled = !usedHelps.Contains("Audience"); //"Közönség" segítség engedélyezése vagy tiltása: Ősz Krisztián
            phoneButton.IsEnabled = !usedHelps.Contains("Phone"); //"Telefonos segítség" engedélyezése vagy tiltása: Ősz Krisztián
        }

        //"Fifty-Fifty" segítség gomb eseménykezelője: Ősz Krisztián
        private void fiftyFiftyButton_Click(object sender, RoutedEventArgs e)
        {
            usedHelps.Add("FiftyFifty"); //Segítség felhasználásának rögzítése: Ősz Krisztián
            var randomOptions = currentQuestion.GetRandomOptions(2); //Véletlenszerűen kiválasztott válaszlehetőségek: Ősz Krisztián
            optionsListBox.ItemsSource = randomOptions; //Válaszlehetőségek frissítése: Ősz Krisztián
            fiftyFiftyButton.IsEnabled = false; //"Fifty-Fifty" segítség gomb tiltása: Ősz Krisztián
        }

        //"Közönség" segítség gomb eseménykezelője: Sós Róbert
        private void audienceButton_Click(object sender, RoutedEventArgs e)
        {
            usedHelps.Add("Audience"); //Segítség felhasználásának rögzítése: Sós Róbert
            var audienceVotes = currentQuestion.GetAudienceVotes(); //Közönség szavazatai: Sós Róbert
            MessageBox.Show($"A közönség szavazatai:\n\nA: {audienceVotes[0]}%\nB: {audienceVotes[1]}%\nC: {audienceVotes[2]}%\nD: {audienceVotes[3]}%"); //Közönség szavazatainak megjelenítése üzenetablakban: Sós Róbert
            audienceButton.IsEnabled = false; //"Közönség" segítség gomb tiltása: Sós Róbert
        }

        //"Telefonos segítség" gomb eseménykezelője: Ősz Krisztián
        private void phoneButton_Click(object sender, RoutedEventArgs e)
        {
            usedHelps.Add("Phone"); //Segítség felhasználásának rögzítése: Ősz Krisztián
            var friendOpinion = currentQuestion.GetFriendOpinion(); //Barát véleménye: Ősz Krisztián
            MessageBox.Show($"A barátod véleménye: \"{friendOpinion}\""); //Barát véleményének megjelenítése üzenetablakban: Ősz Krisztián
            phoneButton.IsEnabled = false; //"Telefonos segítség" gomb tiltása: Ősz Krisztián
        }

        //Válaszlehetőség kiválasztása eseménykezelője: Sós Róbert
        private void optionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (optionsListBox.SelectedItem != null)
            {
                string selectedOption = optionsListBox.SelectedItem.ToString(); //Kiválasztott válaszlehetőség: Sós Róbert
                CheckAnswer(selectedOption); //Válasz ellenőrzése: Sós Róbert
            }
        }

        //Következő gomb eseménykezelője: Sós Róbert
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            NextQuestion(); //Következő kérdés betöltése: Sós Róbert
        }
    }
}