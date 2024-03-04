using System.Xml.Serialization;

namespace FinalProject
{
    public class MovieManager
    {
        //XML ფაილის ადგილმდებარეობა სადაც ფილმები ინახება
        private static string path = "MoviesBase.xml";
        //მეთოდი რათა დავამატოთ ახალი ფილმი სიაში და დავიმახსოვროთ XML ფაილში
        public static void AddMovie(string movieName, string director, int year)
        {
            //არსებული ფილმების დესერიალიზაცია XML-იდან
            List<Movie> movies = Deserialization();

            //ახალი ფილმის ჩამატება სიაში
            movies.Add(new Movie
            {
                MovieName = movieName,
                Director = director,
                ReleaseYear = year
            });
            //სერიალიზაცია განახლებული სიის უკან XML-ში
            Serialization(movies);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Movie added successfully");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
        //მეთოდი რათა ვნახოთ დეტალური ინფორმაცია ყველა ფილმზე
        public static void DetailInfo()
        {
            //XML-დან დესერიალიზაცია
            List<Movie> movies = Deserialization();

            //თითოეული ფილმის დეტალის ბეჭვდა
            foreach (var item in movies)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Movie name: {item.MovieName}, directed by {item.Director}, released at {item.ReleaseYear} year");
                Console.ForegroundColor = ConsoleColor.White;

            }
            Console.WriteLine();
        }
        //ფილმის მოძებნის მეთოდი  მისი სახელის მიხედვით და შემდეგ ჩვენება
        public static void SearchMovie(string title)
        {
            //XML-დან დესერიალიზაცია
            List<Movie> movies = Deserialization();
            //ყველა ფილმის მოძებნა რომელიც ემთხვევა სათაურს
            var findMovies = movies.FindAll(m => m.MovieName.Equals(title, StringComparison.OrdinalIgnoreCase));
            //კონსოლში ჩვენება თუ ნაპოვნი ფილმების რაოდენობა 0-ზე მეტია
            if (findMovies.Count > 0)
            {
                foreach (var item in findMovies)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Found movie name: {item.MovieName}, directed by {item.Director}, released at {item.ReleaseYear} year");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Movies not found with that title.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();

        }
        //ფილმის წაშლის მეთოდი სახელის მიხედვით
        public static void RemoveMovie(string title)
        {
            //XML-დან დესერიალიზაცია
            List<Movie> movies = Deserialization();
            //ფილმის მოძებნა (კონკრეტულის) რომლის წაშლაც გვსურს
            var findMovies = movies.Find(m => m.MovieName.Equals(title, StringComparison.OrdinalIgnoreCase));
            //წაშლა და სერიალიზება თუ ფილმი მოიძებნა
            if (findMovies != null)
            {
                movies.Remove(findMovies);
                Serialization(movies);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Movie '{title}' removed successfully.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Movie not found.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        // ფილმის რედაქტირების მეთოდი ფილმის სახელის მიხედვით
        public static void EditMovie(string title)
        {
            //XML-დან დესერიალიზაცია
            List<Movie> movies = Deserialization();
            //მოვძებნოთ ფილმი რომ დავარედაქტიროთ
            var movieToEdit = movies.Find(m => m.MovieName.Equals(title, StringComparison.OrdinalIgnoreCase));
            //var movieToEdit = movies.Find(m => m.MovieName == title);
            //თუ ფილმს იპოვის გამოგვიჩინოს პარამეტრები თუ რისი რედაქტირება გვსურს
            if (movieToEdit != null)
            {
                Console.WriteLine("What do you want to edit?");
                Console.WriteLine("1 - Title");
                Console.WriteLine("2 - Director");
                Console.WriteLine("3 - Release Year");

                string editOption = Console.ReadLine();

                switch (editOption)
                {
                    case "1":
                        Console.WriteLine("Enter new title: ");
                        string newTitle = Console.ReadLine();
                        movieToEdit.MovieName = newTitle;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Title updated successfully.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case "2":
                        Console.WriteLine("Enter new director: ");
                        string newDirector = Console.ReadLine();
                        movieToEdit.Director = newDirector;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Director updated successfully.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case "3":
                        Console.WriteLine("Enter new release year: ");
                        int newYear;
                        while (!int.TryParse(Console.ReadLine(), out newYear))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Enter a valid year: ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        movieToEdit.ReleaseYear = newYear;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Release year updated successfully.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                }
                //ისევ უკან XML ფაილში სერიალიზაცია 
                Serialization(movies);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Movie not found.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //სერიალიზაცია ფილმების სიის XML -ად
        private static void Serialization(List<Movie> moviesList)
        {
            XmlSerializer serializer = new (typeof(List<Movie>));
            using StreamWriter writer = new(path);
            serializer.Serialize(writer, moviesList);
        }
        //ფილმების სიის დესერიალიზაცია XML-დან, ვაბრუნებთ ცარიელ სიას თუ ფაილი არ არსებობს
        private static List<Movie> Deserialization()
        {
            if (!File.Exists(path))
            {
                return [];
            }

            XmlSerializer serializer = new (typeof(List<Movie>));
            using StreamReader reader = new(path);
            return (List<Movie>)serializer.Deserialize(reader);
        }
    }
}
