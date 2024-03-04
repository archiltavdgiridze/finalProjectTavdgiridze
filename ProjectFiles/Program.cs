using FinalProject;
using System.Xml.Serialization;

#region Calculator
void Calculator()
{
    Console.WriteLine("Enter first number:");
    double x;
    while (!double.TryParse(Console.ReadLine(), out x))
    {
        Console.WriteLine("Invalid input. Please enter a number:");
    }

    Console.WriteLine("Enter second number:");
    double y;
    while (!double.TryParse(Console.ReadLine(), out y))
    {
        Console.WriteLine("Invalid input. Please enter a number:");
    }

    Console.WriteLine("Choose operation: '+, -, * or /': ");
    char z;
    while (true)
    {
        string input = Console.ReadLine();
        if (input.Length == 1 && "+-*/".Contains(input[0]))
        {
            z = input[0];
            break;
        }
        else
        {
            Console.WriteLine("Invalid operation. Please enter one of the following: +, -, *, /");
        }
    }

    double result;

    switch (z)
    {
        case '+':
            result = x + y;
            Console.WriteLine($"Result: {result}");
            break;
        case '-':
            result = x - y;
            Console.WriteLine($"Result: {result}");
            break;
        case '*':
            result = x * y;
            Console.WriteLine($"Result: {result}");
            break;
        case '/':
            if (y != 0)
            {
                result = x / y;
                Console.WriteLine($"Result: {result}");
            }
            else
            {
                Console.WriteLine("Cannot divide by zero");
            }
            break;
    }
}

#endregion

#region GuessingNumber
void GuessingNumber()
{
    Random rand = new();
    int number = rand.Next(1,101);
    int count = 1;
    Console.WriteLine("Guess the number between 1 and 100: ");

    while (true)
    {
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int tryNumber))
        {
            Console.WriteLine("Please enter a valid number.");
            continue;
        }
        else if (tryNumber < number)
        {
            Console.WriteLine("Your number is low, try again.");
            ++count;
            continue;
        }
        else if (tryNumber > number)
        {
            Console.WriteLine("Your number is high, try again.");
            ++count;
            continue;
        }
        else
        {
            Console.WriteLine($"Congrats you gussed the number. The number was {number}");
            Console.WriteLine($"It tooks {count} try to guess the number");

            Console.WriteLine("Do you want to play again? (yes/no): ");

            string playAgain = Console.ReadLine().Trim().ToLower();

            if (playAgain == "yes" || playAgain == "y")
            {
                // Reset the game
                number = rand.Next(1, 101);
                count = 1;
                Console.WriteLine("\nGuess the number between 1 and 100: ");
                continue;
            }
            else
            {
                Console.WriteLine("Thanks for playing!");
                break; 
            }
        }
    }
}
#endregion

#region Hangman
void Hangman()
{
    List<string> myColors = ["yellow", "red", "black", "magenta", "maroon", "golden", "silver", "aqua", "purple", "gray"];
    List<string> myCars = ["Toyota", "Honda", "BMW", "Ford", "Chevrolet", "Mercedes", "Audi", "Lexus", "Volkswagen", "Hyundai"];
    List<string> myCities = ["Tokyo", "New York", "London", "Paris", "Beijing", "Moscow", "Dubai", "Tbilisi", "Chicago", "Hong Kong"];
    List<string> myCountries = ["USA", "China", "India", "Brazil", "Russia", "Japan", "Germany", "Georgia", "France", "Italy"];


    string category = ShowMenu();
    string word = GetWord(category);
    Play(word);

    do
    {
        Console.WriteLine("Would you like to play again? (Y or N)");
        string answer = Console.ReadLine().ToUpper();
        if (answer == "Y" || answer == "YES")
        {
            category = ShowMenu();
            word = GetWord(category);
            Play(word);
        }
        else if (answer == "N" || answer == "NO")
        {
            return; 
        }
        else
        {
            Console.WriteLine("Please enter a correct answer.");
        }
    } while (true);


    static string ShowMenu()
    {
        Console.WriteLine("Choose a category:");
        Console.WriteLine("1. Colors");
        Console.WriteLine("2. Cars");
        Console.WriteLine("3. Cities");
        Console.WriteLine("4. Countries");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                return "colors";
            case "2":
                return "cars";
            case "3":
                return "cities";
            case "4":
                return "countries";
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                return ShowMenu();
        }
    }

    string GetWord(string category)
    {
        Random myRandom = new();
        return category switch
        {
            "colors" => myColors[myRandom.Next(myColors.Count)].ToUpper(),
            "cars" => myCars[myRandom.Next(myCars.Count)].ToUpper(),
            "cities" => myCities[myRandom.Next(myCities.Count)].ToUpper(),
            "countries" => myCountries[myRandom.Next(myCountries.Count)].ToUpper(),
            _ => throw new ArgumentException("Invalid category"),
        };
    }

    static void Play(string myWord)
    {
        string completeWord = new('-', myWord.Length);
        HashSet<char> guessedLetters = [];
        List<string> guessedWords = [];
        int numberOfTries = 6;

        Console.WriteLine("\t\t\t Welcome To The Hangman Game");
        Console.WriteLine($"You should guess the {myWord.Length}-letter word. Let's start!");
        DrawHangman(numberOfTries);
        Console.WriteLine(completeWord);
        Console.WriteLine();

        while (numberOfTries > 0)
        {
            Console.WriteLine($"You have {numberOfTries} attempts left.");
            Console.Write("Enter your guess: ");
            string guess = Console.ReadLine().ToUpper();

            if (guess.Length == 1 && char.IsLetter(guess[0]))
            {
                if (guessedLetters.Contains(guess[0]))
                {
                    Console.WriteLine($"You already guessed the letter '{guess}'. Please try another one.");
                    continue;
                }

                guessedLetters.Add(guess[0]);

                if (myWord.Contains(guess))
                {
                    Console.WriteLine($"Good job! The letter '{guess}' is in the word.");
                    for (int i = 0; i < myWord.Length; i++)
                    {
                        if (myWord[i] == guess[0])
                        {
                            completeWord = completeWord.Remove(i, 1).Insert(i, guess);
                        }
                    }

                    if (completeWord == myWord)
                    {
                        Console.WriteLine($"Congratulations! You guessed the word: {myWord}");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine($"Sorry, but the letter '{guess}' is not in the word.");
                    numberOfTries--;
                    if (numberOfTries == 0)
                    {
                        Console.WriteLine($"You ran out of attempts. The word was: {myWord}");
                        return;
                    }
                }
            }
            else if (guess.Length == myWord.Length && guess.All(char.IsLetter))
            {
                if (guessedWords.Contains(guess))
                {
                    Console.WriteLine($"You already guessed the word '{guess}'. Please try another one.");
                    continue;
                }

                guessedWords.Add(guess);

                if (guess != myWord)
                {
                    Console.WriteLine($"Sorry, but the word '{guess}' is not correct.");
                    numberOfTries--;
                    if (numberOfTries == 0)
                    {
                        Console.WriteLine($"You ran out of attempts. The word was: {myWord}");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine($"Congratulations! You guessed the word: {myWord}");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter either a single letter or a word with the same length as the hidden word.");
            }

            DrawHangman(numberOfTries);
            Console.WriteLine(completeWord);
            Console.WriteLine();
        }
    }

    static void DrawHangman(int tries)
    {
        string[] attempts =
        {
            @"
           
               ════════
               ▐       ▐ 
               ▐       ▐
               ▐       0
               ▐      \|/
               ▐       |
               ▐      / \
               ▐
            ",

            @"
                      OMG, Be careful, you have a last attempt !!!
        
                ════════
               ▐       ▐ 
               ▐       ▐
               ▐       O
               ▐      \|/
               ▐       |
               ▐      /
               ▐
            ",

            @"
                ════════
               ▐       ▐ 
               ▐       ▐
               ▐       O
               ▐      \|/
               ▐       |
               ▐
               ▐
            ",

            @"
                ════════
               ▐       ▐ 
               ▐       ▐
               ▐       O
               ▐      \|
               ▐       |
               ▐
               ▐
            ",

            @"
                ════════
               ▐       ▐ 
               ▐       ▐
               ▐       O
               ▐       |
               ▐       |
               ▐
               ▐
            ",

            @"
                ════════
               ▐       ▐ 
               ▐       ▐
               ▐       O
               ▐      
               ▐     
               ▐
               ▐
            ",

            @"
                ════════
               ▐       ▐ 
               ▐       ▐
               ▐    
               ▐      
               ▐     
               ▐
               ▐
            ",
        };

        Console.WriteLine(attempts[tries - 1]);
    }
}
#endregion

#region Translator
void Translator()
{
    //ვახდენთ XmlSerializer-ის ინიციალიზაციას Translator ობიექტის სიის დესერიალიზაციისთვის
    XmlSerializer serializer = new(typeof(List<Translator>));
    // გვანახებს ადგილმდებარეობას xml ფაილისას რომელიც შეადგენს ლექსიკონს
    string path = "translatedWords.xml";
    //ვაცხადებთ სიას, რომ შევინახოთ ნათარგმნი სიტყვები დესერიალიზაციის შემდგომ.
    List<Translator> translatedWords;

    // XML ფაილის დესერიალიზება Translator ობიექტების სიაში 
    using (StreamReader reader = new(path))
    {
        //ვიყენებთ serializer-ს რომ გადავიყვანოთ XML-ი Translator ობიექტების სიში
        translatedWords = (List<Translator>)serializer.Deserialize(reader);
    }

    Console.WriteLine("\nWelcome to Dictionary!");
    Console.WriteLine("Please choose language couple:\n");

    // განვსაზღვრავთ მეთოდს რომელიც მომხმარებელს ეკითხება აირჩიოს ლექსიკონის ენის წყვილი
    string ChooseLanguage()
    {
        // მივმართოთ მომხმარებელს განუწყვეტლივ სანამ არ გააკეთებს ვალიდურ არჩევანს
        while (true)
        {
            // ენის წყვილების პარამეტრების ჩვენება მომხმარებლისთვის
            Console.WriteLine("From Georgian to English please insert 1.\nFrom English to Georgian: 2.\nFrom Georgian to Russian: 3.\nFrom Russian to Georgian: 4.\nFrom English to Russian: 5.\nFrom Russian to English: 6.\n");
            string? num = Console.ReadLine();
            //ვამოწმებთ მომხმარებლის არჩევანს და ვაბრუნებთ შედეგს თუ ის ვალიდურია
            if (num == "1" || num == "2" || num == "3" || num == "4" || num == "5" || num == "6")
            {
                return num;
            }
            //თუ არჩევანი არავალიდურია ვატყობინებთ მომხარებელს და მეთოდი ახლიდან სთავაზობს არჩევანს
            Console.WriteLine("Invalid choice, please try again.");
        }
    }


    #region translator methods
    // ქართულიდან ინგლისურად თარგმნის მეთოდი
    string GeoToEng()
    {
        // prompt უზერისთვის
        Console.WriteLine("Enter a Georgian word to translate:");
        // იუზერის inputi (ასოების ზომის განურჩევლად)
        string inputWord = Console.ReadLine()!.ToLower();

        // მოვძებნოთ ნათარგმნი სიტყვების(translatedWords) სიაში ელემენტი, სადაც ქართული სიტყვა შეესაბამება შეყვანილ სიტყვას
        // ვიყენებთ FirstOrDefault პირველი შესატყვისი ელემენტის მისაღებად 
        Translator foundWord = translatedWords.FirstOrDefault(item => item.Georgian.Equals(inputWord));

        // ვამოწმებთ ნაპოვნია თუ არა შესაბამისი ობიექტი და აქვს თუ არა მას ინგლისური თარგმანი
        if (foundWord != null && !string.IsNullOrEmpty(foundWord.English))
        {
            // თუ თარგმანს იპოვის დააბრუნოს string
            return $"{inputWord} in English is '{foundWord.English}'\n";
        }
        else
        {
            // თუ შესატყვისი სიტყვა არ მოიძებნა ან მას არ აქვს ინგლისური თარგმანი, ვთხოვოთ მომხმარებელს ჩაამატოს თვითონ.
            Console.WriteLine("Word not found in the translation list or lacks an English translation. Would you like to add a translation? Press 'Y' for yes.");

            var response = Console.ReadKey(true);

            // ვამოწმებთ დააჭირა თუ არა მომხმარებელმა Y ღილაკს
            if (response.Key == ConsoleKey.Y)
            {
                // ვუთხრათ იუზერს რომ შეიყვანოს ინგლისური თარგმანი
                Console.WriteLine("Enter the English translation:");
                // წავიკითხოთ და დავიმახსოვროთ მომხმარებლის თარგმანი
                string englishTranslation = Console.ReadLine()!.ToLower();

                // შევამოწმოთ იყო თუ არა ქართული სიტყვა სიაში
                if (foundWord != null)
                {
                    // თუ სიტყვა არსებობს, უბრალოდ განვაახლოთ მისი ინგლისური თარგმანი
                    foundWord.English = englishTranslation;
                }
                else
                {
                    // თუ ქართული სიტყვა არ მოიძებნა სიაში, შევქმნათ ახალი Translator ობიექტი ქართული და ინგლისური განმარტებებით
                    Translator newWord = new() { Georgian = inputWord, English = englishTranslation };
                    // დავამატოთ ახალი Translator ობიექტი  translatedWords-ის სიაში
                    translatedWords.Add(newWord);
                }

                // გავხსნათ XML ფაილი  'path' ცვლადში მითითებული გზით რომ განვაახლოთ ახალი თარგმანით
                using (StreamWriter writer = new(path))
                {
                    //  Translator objects-ის განახლებული სიის სერიალიზაცია XML ფაილში, ვინახავთ ცვლილებებს
                    serializer.Serialize(writer, translatedWords);
                }

                // ვაცნობოთ მომხმარებელს რომ თარგმანი წარმატებით დაემატა
                Console.WriteLine("Translation successfully added.");
                // დავაბრუნოთ ცარიელი სტრიქონი რათა დავასრულოთ პროგრამა კონკრეტული თარგმნის შედეგის გარეშე
                return "";
            }
        }
        // დავაბრუნოთ ცარიელი სტრიქონი თუ თარგმანი არ შესრულებულა ან დამატებულა.
        return "";
    }



    string EngToGeo()
    {
        Console.WriteLine("Sheiyvanet inglisuri sityva satargmnelad:");
        string inputWord = Console.ReadLine()!.ToLower();

        Translator foundWord = translatedWords.FirstOrDefault(item => item.English.Equals(inputWord));

        if (foundWord != null && !string.IsNullOrEmpty(foundWord.Georgian))
        {
            return $"{inputWord} qartulad aris '{foundWord.Georgian}'\n";
        }
        else
        {
            Console.WriteLine("sityva ar moidzebna leqsikonshi. gsurt targmanis damateba? daachire 'Y' ghilaks tanxmobistvis.");
            var response = Console.ReadKey(true);

            if (response.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Sheiyvanet qartuli targmani:");
                string georgianTranslation = Console.ReadLine()!.ToLower();

                if (foundWord != null)
                {
                    foundWord.Georgian = georgianTranslation;
                }
                else
                {
                    Translator newWord = new() { English = inputWord, Georgian = georgianTranslation };
                    translatedWords.Add(newWord);
                }

                using (StreamWriter writer = new(path))
                {
                    serializer.Serialize(writer, translatedWords);
                }

                Console.WriteLine("Targmani warmatebit daemata.");
                return "";
            }
        }
        return "";
    }


    string GeoToRuss()
    {
        Console.WriteLine("Vvedite gruzinskoe slovo dlya perevoda:");
        string inputWord = Console.ReadLine()!.ToLower();

        Translator foundWord = translatedWords.FirstOrDefault(item => item.Georgian.Equals(inputWord));

        if (foundWord != null && !string.IsNullOrEmpty(foundWord.Russian))
        {
            return $"{inputWord} na russkom yazyke '{foundWord.Russian}'\n";
        }
        else
        {
            Console.WriteLine("Slovo ne naydeno v spiske perevodov ili dlya nego net russkogo perevoda. Khotite dobavit' perevod?");
            Console.WriteLine("Najmite 'Y' dlya podtverzhdeniya.");
            var response = Console.ReadKey(true);

            if (response.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Vvedite russkiy perevod:");
                string russianTranslation = Console.ReadLine()!.ToLower();

                if (foundWord != null)
                {
                    foundWord.Russian = russianTranslation;
                }
                else
                {
                    Translator newWord = new() { Georgian = inputWord, Russian = russianTranslation };
                    translatedWords.Add(newWord);
                }

                using (StreamWriter writer = new(path))
                {
                    serializer.Serialize(writer, translatedWords);
                }

                Console.WriteLine("Perevod uspeshno dobavlen.");
                return "";
            }
        }
        return "";
    }

    string RussToGeo()
    {
        Console.WriteLine("Sheiyvanet rusuli sityva satargmenlad:");
        string inputWord = Console.ReadLine()!.ToLower();

        Translator foundWord = translatedWords.FirstOrDefault(item => item.Russian.Equals(inputWord));

        if (foundWord != null && !string.IsNullOrEmpty(foundWord.Georgian))
        {
            return $"{inputWord} qartulad aris '{foundWord.Georgian}'\n";
        }
        else
        {
            Console.WriteLine("sityva ar moidzebna leqsikonshi. gsurt targmanis damateba? daachire 'Y' ghilaks tanxmobistvis.");
            var response = Console.ReadKey(true);

            if (response.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Sheiyvanet qartuli targmani: ");
                string georgianTranslation = Console.ReadLine()!.ToLower();

                if (foundWord != null)
                {
                    foundWord.Georgian = georgianTranslation;
                }
                else
                {
                    Translator newWord = new() { Russian = inputWord, Georgian = georgianTranslation };
                    translatedWords.Add(newWord);
                }

                using (StreamWriter writer = new(path))
                {
                    serializer.Serialize(writer, translatedWords);
                }

                Console.WriteLine("targmani warmatebit daemata.");
                return "";
            }
        }
        return "";
    }


    string EngToRuss()
    {
        Console.WriteLine("Vvedite angliskoe slova dlia perevoda:");
        string inputWord = Console.ReadLine()!.ToLower();

        Translator foundWord = translatedWords.FirstOrDefault(item => item.English.Equals(inputWord));

        if (foundWord != null && !string.IsNullOrEmpty(foundWord.Russian))
        {
            return $"{inputWord} na ruskom iazike '{foundWord.Russian}'\n";
        }
        else
        {
            Console.WriteLine("Slovo ne naydeno v spiske perevodov ili dlya nego net russkogo perevoda. Khotite dobavit' perevod?.");
            Console.WriteLine("Najmite 'Y' dlya podtverzhdeniya.");
            var response = Console.ReadKey(true);

            if (response.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Vvedite russkiy perevod:");
                string russianTranslation = Console.ReadLine()!.ToLower();

                if (foundWord != null)
                {
                    foundWord.Russian = russianTranslation;
                }
                else
                {
                    Translator newWord = new() { English = inputWord, Russian = russianTranslation };
                    translatedWords.Add(newWord);
                }

                using (StreamWriter writer = new(path))
                {
                    serializer.Serialize(writer, translatedWords);
                }

                Console.WriteLine("Perevod uspeshno dobavlen.");
                return "";
            }
        }
        return "";
    }



    string RussToEng()
    {
        Console.WriteLine("Enter a Russian word to translate:");
        string inputWord = Console.ReadLine()!.ToLower();

        Translator foundWord = translatedWords.FirstOrDefault(item => item.Russian.Equals(inputWord));

        if (foundWord != null && !string.IsNullOrEmpty(foundWord.English))
        {
            return $"{inputWord} in English is '{foundWord.English}'\n";
        }
        else
        {
            Console.WriteLine("Word not found in the translation list or lacks an English translation. Would you like to add a translation? Press 'Y' for yes.");
            var response = Console.ReadKey(true);

            if (response.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Enter the English translation:");
                string englishTranslation = Console.ReadLine()!.ToLower();

                if (foundWord != null)
                {
                    foundWord.English = englishTranslation;
                }
                else
                {
                    Translator newWord = new() { Russian = inputWord, English = englishTranslation };
                    translatedWords.Add(newWord);
                }

                using (StreamWriter writer = new(path))
                {
                    serializer.Serialize(writer, translatedWords);
                }

                Console.WriteLine("Translation successfully added.");
                return "";
            }
        }
        return "";
    }

    #endregion

    #region ContinueMethods
    // განსაზღვრავს მეთოდს რომ უწყვეტად შესთავაზოს მომხმარებელს ახალი სიტყვის თარგმანი.
    void ContinueGeoToEngTranslating()
    {
        //ვკითხოთ მომხარებელს სურს თუ არა ახალი სიტყვის თარგმნა
        Console.WriteLine("Want to translate another word?\n" +
            "press 'Y' for acceptance.\n");
        //კითხულობს მომხარებლის პასუხს
        var answer = Console.ReadKey(true);
        //ამოწმებს დააჭირა თუ არა Y ღილაკზე
        if (answer.Key == ConsoleKey.Y)
        {
            //ვიძახებთ მეთოდს რომ ვთარგმნოთ სიტყვა და შევინახოთ შედეგი ცვლადში რათა დავბეჭდოთ
            var word = GeoToEng();
            Console.WriteLine(word);
            //რეკურსია რათა ვკითხოთ მომხმარებელს სურშ თუ არა ახალი სიტყვის თარგმნა ისევ
            ContinueGeoToEngTranslating();
        }
        // თუ Y ღილაკზე არ დააჭერს მომხმარებელი შესაბამისად მეთოდის ციკლი ამით დასრულდება
        return;

    }

    void ContinueEngToGeoTranslating()
    {
        Console.WriteLine("gsurt sxva sityvis targmna?\n"
        + "daachiret ghilaks 'Y' tanxmobisatvis.\n");

        var answer = Console.ReadKey(true);
        if (answer.Key == ConsoleKey.Y)
        {
            var word = EngToGeo();
            Console.WriteLine(word);
            ContinueEngToGeoTranslating();
        }
        return;
    }


    void ContinueGeoToRussTranslating()
    {
        Console.WriteLine("xotite perevodit drugoe slova?\n"
        + "najmite klavishu 'Y'.\n");

        var answer = Console.ReadKey(true);
        if (answer.Key == ConsoleKey.Y)
        {
            var word = GeoToRuss();
            Console.WriteLine(word);
            ContinueGeoToRussTranslating();
        }
        return;
    }

    void ContinueRussToGeoTranslating()
    {
        Console.WriteLine("gsurt sxva sityvis targmna?\n"
        + "daachiret ghilaks 'Y' tanxmobisatvis.\n");

        var answer = Console.ReadKey(true);
        if (answer.Key == ConsoleKey.Y)
        {
            var word = RussToGeo();
            Console.WriteLine(word);
            ContinueRussToGeoTranslating();
        }
        return;
    }

    void ContinueEngToRussTranslating()
    {

        Console.WriteLine("xotite perevodit drugoe slova?\n"
        + "najmite klavishu 'Y'.\n");

        var answer = Console.ReadKey(true);
        if (answer.Key == ConsoleKey.Y)
        {
            var word = EngToRuss();
            Console.WriteLine(word);
            ContinueEngToRussTranslating();
        }
        return;
    }

    void ContinueRussToEngTranslating()
    {
        Console.WriteLine("Want to translate another word?\n" +
            "press 'Y' for acceptance.\n");

        var answer = Console.ReadKey(true);
        if (answer.Key == ConsoleKey.Y)
        {
            var word = RussToEng();
            Console.WriteLine(word);
            ContinueRussToEngTranslating();
        }
        return;
    }

    #endregion

    //უშუალოდ თარგმნის პროცესის დაწყება
    string ? choosedLanguage = ChooseLanguage();

    if (choosedLanguage == "1")
    {
        string ? word = GeoToEng();
        Console.WriteLine(word);
        ContinueGeoToEngTranslating();
    }
    else if (choosedLanguage == "2")
    {
        string ? word = EngToGeo();
        Console.WriteLine(word);
        ContinueEngToGeoTranslating();
    }
    else if (choosedLanguage == "3")
    {
        string ? word = GeoToRuss();
        Console.WriteLine(word);
        ContinueGeoToRussTranslating();
    }
    else if (choosedLanguage == "4")
    {
        string ? word = RussToGeo();
        Console.WriteLine(word);
        ContinueRussToGeoTranslating();
    }
    else if (choosedLanguage == "5")
    {
        string ? word = EngToRuss();
        Console.WriteLine(word);
        ContinueEngToRussTranslating();
    }
    else if (choosedLanguage == "6")
    {
        string ? word = RussToEng();
        Console.WriteLine(word);
        ContinueRussToEngTranslating();
    }

}
#endregion

#region Bankomati

void Bankomati()
{
    //XML ფაილის გზა
    string path = "customersData.xml";

    // სერიალიზაცია 
    void serialization(List<Customer> customers)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
        using (StreamWriter writer = new StreamWriter(path))
        {
            serializer.Serialize(writer, customers);

        }
    }

    // დესერიალიზაცია
    List<Customer> deserialization()
    {
        List<Customer> poorCustomers = new List<Customer>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
        using (StreamReader reader = new StreamReader(path))
        {
            poorCustomers = (List<Customer>)serializer.Deserialize(reader);
        }
        return poorCustomers;
    }
    //მთავარი მენიუ
    void userWelcome()
    {
        Console.WriteLine("Welcome ATM C# Program");
        Console.WriteLine("\n  Gtxovt Airchiot Varianti ");
        Console.WriteLine("1. Arsebuli Momxmarebeli");
        Console.WriteLine("2. Axali Momxmarebeli");
        int mainOption = int.Parse(Console.ReadLine());
        if (mainOption == 1)
        {
            login();
        }
        else if (mainOption == 2)
        {
            newUser();
        }
    }

    userWelcome();
    //ახალი იუზერის დამატება ბაზაში ამ შემთხვევაში XML ფაილში
    void newUser()
    {
        Console.WriteLine("Gtxovt Shemoikvanot Tkveni Saxeli: ");
        string newName = Console.ReadLine();
        Console.WriteLine("Gtxovt Shemoikvanot Username: ");
        string newuserAcc = Console.ReadLine();

        List<Customer> customer = deserialization();
        foreach (Customer cust in customer)
        {
            if (newuserAcc == cust.userAccount)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Eseti UserName Ukve Arsebobs, Gtxovt Sxva Scadet.");
                Console.WriteLine("Gtxovt Shemoikvanet Axali UserName: ");
                newuserAcc = Console.ReadLine();

            }
        }

        Console.WriteLine("Gtxovt Shemoikvanot Paroli: ");
        string newPass = Console.ReadLine();
        Console.WriteLine("Gtxovt Shemoikvanet Tanxa Romlis Shemotanac Gsurt: ");
        double newBalance = double.Parse(Console.ReadLine());

        Customer newCustomer = new Customer();
        newCustomer.userAccount = newuserAcc;
        newCustomer.password = newPass;
        newCustomer.name = newName;
        newCustomer.balance = newBalance;

        customer.Add(newCustomer);
        serialization(customer);

        Thread.Sleep(2000);
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Tkveni Accounti Carmatebit Sheikmna!\n\n");
        login();

    }
    // ბანკომატის იუზერში შესვლა
    void login()
    {
        Console.WriteLine("\t\tShesvla");
        Console.WriteLine("Gtxovt Shemoikvanot Tkveni UserName: ");
        String userAcc = Console.ReadLine();

        bool isAccount = false;
        List<Customer> desCustomer = deserialization();
        foreach (Customer loginArgument in desCustomer)
        {
            if (userAcc == loginArgument.userAccount)
            {
                isAccount = true;
                Console.WriteLine("Gtxovt Shemoikvanot Tkveni Paroli: ");
                string pass = Console.ReadLine();
                if (pass == loginArgument.password)
                {
                    Console.WriteLine("\t \t \t Mogesalmebit : " + loginArgument.name);
                    int option = 0;
                    do
                    {
                        selectOptions();

                        option = int.Parse(Console.ReadLine());

                        if (option == 1)
                        {
                            deposit(loginArgument);
                        }
                        else if (option == 2)
                        {
                            withdraw(loginArgument);
                        }
                        else if (option == 3)
                        {
                            balance(loginArgument);
                        }
                        else if (option == 4)
                        {
                            break;
                        }
                        else
                        {
                            option = 0;
                        }
                    }
                    while (option != 4);
                    Console.WriteLine("Gmadlobt Gisurvebt Mshvidobian Dghes!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    userWelcome();
                }
                else
                {
                    Console.WriteLine("");

                }

            }
            serialization(desCustomer);
        }
        if (!isAccount)
        {
            Console.WriteLine("\nTkveni Accounti Ver Moidzebna\n");
            Console.WriteLine("Amoirchiet Qvemot Mocemu Variantebi: ");
            Console.WriteLine("1. Xelaxla Scadet");
            Console.WriteLine("2. Axali Accountis Shekvana");
            Console.WriteLine("3. Gamosvla");
            int select = int.Parse(Console.ReadLine());
            if (select == 1)
            {
                login();
            }
            else if (select == 2)
            {
                newUser();
            }
            else
            {
                return;
            }
        }
    }

    //ვარიანტების ამორჩევა 
    void selectOptions()
    {
        Console.WriteLine("\n Amoirchiet Qvemot Mocemuli Variantebi");
        Console.WriteLine("1. Depozitze Fulis Shetana");
        Console.WriteLine("2. Fulis Gamotana");
        Console.WriteLine("3. Balansis Naxva");
        Console.WriteLine("4. Gamosvla");
    }
    //დეპოზიტის შემოტანა
    void deposit(Customer currentUsers)
    {
        Console.WriteLine("Gtxovt Shemoikvanot Tanxis Raodenoba: ");
        double deposited = double.Parse(Console.ReadLine());
        currentUsers.balance += deposited;
        Console.WriteLine("Tanxa Carmatebit Daido Angarishze!");
        Console.WriteLine("Tkveni Balansi " + currentUsers.balance);

        // განვაახლოთ მომხმარებლის ბალანსი
        List<Customer> customers = deserialization();
        int index = customers.FindIndex(c => c.userAccount == currentUsers.userAccount);
        if (index != -1)
        {
            customers[index] = currentUsers; // მომხმარებლის განახლება სიაში
        }
        serialization(customers); // განახლებული სიის სერიალიზაცია XML ფაილში
    }
    //თანხის გატანა
    void withdraw(Customer currentUsers)
    {
        Console.WriteLine("Shemoikvanet Gasatani Tanxis Raodenoba: ");
        double withdrawal = double.Parse(Console.ReadLine());

        if (currentUsers.balance < withdrawal)
        {
            Console.WriteLine("Bodishit Tkveni Motxovnili Tanxa Aghemateba Tkvens Balans");
        }
        else
        {
            currentUsers.balance -= withdrawal;
            Console.WriteLine("Fulis Gatana Moxda Carmatebit!");
            Console.WriteLine("Tkveni Arsebuli Tanxa " + currentUsers.balance);

            // განვაახლოთ მომხმარებლის ბალანსი
            List<Customer> customers = deserialization();
            int index = customers.FindIndex(c => c.userAccount == currentUsers.userAccount);
            if (index != -1)
            {
                customers[index] = currentUsers; // მომხმარებლის განახლება სიაში
            }
            serialization(customers); // განახლებული სიის სერიალიზაცია XML ფაილში
        }
    }
    //ბალანსი
    void balance(Customer currentUsers)
    {
        Console.WriteLine("Arsebuli Tanxa: " + currentUsers.balance);
    }
}

#endregion

#region MovieList
void MovieList()
{
    // მისალმების პრომპტი მოხმარებლისთვის
    Console.WriteLine("Welcome To Movies Program: ");
    Console.WriteLine("Choose an option: ");

    //პროგრამის გაგრძელების კონტროლი
    bool continueProgram = true;

    do
    {
        //მენიუს ოფციები
        Console.WriteLine("1 - Add new movie." + "\n2 - See detail informations of all movies." + "\n3 - Search the movie by it's title." + "\n4 - Remove the movie." + "\n5 - Edit the movie." + "\n6 - Exit the program.");

        //მომხმარებლის არჩევანი
        string option = Console.ReadLine();
        // მომხმარებლის არჩევნის ვალიდურობის მონიშვნა
        bool validOption = true;


        switch (option)
        {
            case "1":
                Console.WriteLine("Enter movie name: ");
                string movieName = Console.ReadLine();
                Console.WriteLine("Enter movie director: ");
                string movieDirector = Console.ReadLine();
                Console.WriteLine("Enter release date: ");
                int releaseYear;
                while (!int.TryParse(Console.ReadLine(), out releaseYear))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter valid year: ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                //ფილმის ჩამატების მეთოდი ზემოთ მონიშნული ცვლადების საშუალებით
                MovieManager.AddMovie(movieName, movieDirector, releaseYear);

                break;
            case "2":
                Console.WriteLine("Detailed information of all movies: ");
                Console.WriteLine();
                //ყველა ფილმის დეტალური ინფო სიის სახით
                MovieManager.DetailInfo();
                break;
            case "3":
                Console.WriteLine();
                Console.WriteLine("Enter the title of the movie you want to find: ");
                string title = Console.ReadLine()!.ToLower();
                //ფილმის მოძებნა ფილმის სახელით
                MovieManager.SearchMovie(title);
                break;
            case "4":
                Console.WriteLine();
                Console.WriteLine("Enter the title of the movie you want to remove: ");
                string removedMovie = Console.ReadLine()!.ToLower();
                //ფილმის წაშლა ფილმის სახელით
                MovieManager.RemoveMovie(removedMovie);
                break;
            case "5":
                Console.WriteLine();
                Console.WriteLine("Enter the title of the movie you want to edit: ");
                string editMovieTitle = Console.ReadLine()!.ToLower();
                //ფილმის რედაქტირება ფილმის სახელით
                MovieManager.EditMovie(editMovieTitle);
                break;
            case "6":
                //პროგრამიდან გამოსვლა
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option. Please enter a number between 1 and 6.");
                Console.ForegroundColor = ConsoleColor.White;
                validOption = false;
                break;
        }
        //სურს თუ არა მომხარებელს გაგრძელება
        if (validOption) // მხოლოდ იმ შეთხვევაში თუ არჩეული ვარიანტი ვალიდური იყო
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Do you want to continue program? Insert 'yes' to continue.");
            Console.ForegroundColor = ConsoleColor.White;
            string optionForContinue = Console.ReadLine()!.ToLower();
            if (optionForContinue != "yes")
            {
                continueProgram = false;
            }
        }
    } while (continueProgram); //ვატრიალოთ სანამ მომხმარებელი არ აირჩევს გასვლას
}
#endregion

#region StudentSystem
void StudentSys()
{
    Console.WriteLine("Welcome to the students app!");

    bool continueProgram = true;

    do
    {
        Console.WriteLine("Please choos an option: ");
        //მენიუს არჩევანი
        Console.WriteLine("1 - Add a new student." + "\n2 - See all students." + "\n3 - Search students by roll number." + "\n4 - Update the grade of student." + "\n5 - Remove Student." + "\n6 - Exit the program.");

        //მომხმარებლის არჩევანი
        string option = Console.ReadLine();
        bool validOption = true;
        bool skipContinuationPrompt = false;

        switch (option)
        {
            // სტუდენტის დამატება
            case "1":
                Console.WriteLine("Enter student's full name: ");
                string studentName = Console.ReadLine();

                Console.WriteLine("Enter student's roll number: ");

                int rollNumber;
                do
                {
                    while (!int.TryParse(Console.ReadLine(), out rollNumber))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter valid number: ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (Student.RollNumberExists(rollNumber))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("A student with this roll number already exists.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Please enter a different roll number:");
                    }
                } while (Student.RollNumberExists(rollNumber));

                Console.WriteLine("Enter student's grade (A, B, C, D, E, F): ");
                char grade = ' ';
                bool isValidGrade = false;
                while (!isValidGrade)
                {
                    string input = Console.ReadLine().ToUpper();
                    if (!string.IsNullOrEmpty(input) && input.Length == 1 && "ABCDEF".Contains(input[0]))
                    {
                        grade = input[0];
                        isValidGrade = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter a valid grade (A, B, C, D, E, F): ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Student.AddStudent(studentName, rollNumber, grade);
                break;
            case "2":
                Console.WriteLine();
                Console.WriteLine("Detailed information of all students: ");
                Console.WriteLine();
                //ყველა სტუდენტის დეტალური ინფო სიის სახით
                Student.DetailInfo();
                break;
            case "3":
                bool endSearch = false; // მონიშვნა დასრულდა თუ არა ძებნა რაც გვაბრუნებს მთავარ მენიუში
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter the roll number of the student: ");
                    int rollNum;
                    while (!int.TryParse(Console.ReadLine(), out rollNum))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter a valid roll number: ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    bool found = Student.SearchStudent(rollNum);

                    if (!found) // თუ სტუდენს ვერ იპოვის
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Would you like to try again? (yes/no): ");
                        Console.ForegroundColor = ConsoleColor.White;
                        string userChoice = Console.ReadLine().ToLower();
                        if (userChoice != "yes")
                        {
                            endSearch = true; // დავაყენოთ მართებულზე რომ მორჩეს ძებნას და დაბრუნდეს მთავარ მენიუში
                        }
                    }
                    else
                    {
                        endSearch = true; // მორჩეს ძებნას თუ სტუდენტს მოძებნის
                    }
                } while (!endSearch); // გააგრძელოს ცილკი თუ endSearch არის false
                skipContinuationPrompt = true;
                break;


            case "4":
                bool endUpdate = false; // თუ დასრულდა განახლების პროცესი გვაბრუნებს მთავარ მენიუში
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter the roll number of student to edit his/her grade: ");
                    int rollNumForEdit;
                    while (!int.TryParse(Console.ReadLine(), out rollNumForEdit))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter valid roll number: ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    bool updated = Student.UpdateGrade(rollNumForEdit);

                    if (!updated) // სტუდენტის ვერ აღმოჩენის შემთხვევაში
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Would you like to try again? (yes/no): ");
                        Console.ForegroundColor = ConsoleColor.White;
                        string userChoice = Console.ReadLine().ToLower();
                        if (userChoice != "yes")
                        {
                            endUpdate = true; // გვაბრუნებს მთავარ მენიუში თუ userChoice  არ არის yes
                        }
                    }
                    else
                    {
                        endUpdate = true; // ასრულებს განახლების პროცესს თუ შეფასება წარმატებით განახლდება
                    }
                } while (!endUpdate); // გააგრძელოს ციკლი თუ endUpdate არის false
                skipContinuationPrompt = true;
                break;
            case "5":
                Console.WriteLine();
                // არსებული სტუდენტების ჩვენება რათა აირჩიოს რომელი წაშალოს
                Student.DetailInfo();
                Console.WriteLine("Enter roll number to remove student: ");
                int removeStudentNum;
                while (!int.TryParse(Console.ReadLine(), out removeStudentNum))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter valid roll number: ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Student.RemoveStudent(removeStudentNum);
                break;
            case "6":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option. Please enter a number between 1 and 6.");
                Console.ForegroundColor = ConsoleColor.White;
                validOption = false;
                break;
        }
        // ამოწმებს სურს თუ არა იუსერ გაგრძელება მხოლოდ იმ შემთხვევაში თუ ეს ვალიდური ვარიანტია და გაგრძელების მოთხოვნა არ არის გამოტოვებული
        if (validOption && !skipContinuationPrompt)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Do you want to continue program? Insert 'yes' to continue.");
            Console.ForegroundColor = ConsoleColor.White;
            string optionForContinue = Console.ReadLine()!.ToLower();
            if (optionForContinue != "yes")
            {
                continueProgram = false;
            }
        }

    } while (continueProgram);
}
#endregion

while (true)
{
    Console.WriteLine("\n\t\t\tSelect the program to run:");
    Console.WriteLine("1. Calculator");
    Console.WriteLine("2. Guessing Number");
    Console.WriteLine("3. Hangman");
    Console.WriteLine("4. Translator");
    Console.WriteLine("5. Bankomati");
    Console.WriteLine("6. MoviesList");
    Console.WriteLine("7. StudentsSystem");
    Console.WriteLine("0. Exit");

    string ? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Calculator();
            break;
        case "2":
            GuessingNumber(); break;
        case "3":
            Hangman(); break;
        case "4":
            Translator(); break;
        case "5":
            Bankomati(); break;
        case "6":
            MovieList(); break;
        case "7": StudentSys(); break;
        case "0":
            Console.WriteLine("Exiting the program.");
            return; 
        default:
            Console.WriteLine("Invalid choice. Please select a valid option.");
            break;
    }
}