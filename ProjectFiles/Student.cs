using System.Xml.Serialization;

namespace FinalProject
{
    public class Student
    {
        
        //XML ფაილის ადგილმდებარეობა სადაც სტუდენტების სია ინახება
        private static string path = "StudentsBase.xml";

        public string FullName { get; set; }
        public int RollNumber { get; set; }
        public char Grade { get; set; } 

        public static void AddStudent(string fullName, int rollNumber, char grade)
        {
            //არსებული სტუდენტების დესერიალიზაცია XML-იდან
            List<Student> students = Deserialization();

            students.Add(new Student { FullName = fullName, RollNumber = rollNumber, Grade = grade });
            //სერიალიზაცია განახლებული სიის უკან XML-ში
            Serialization(students);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Student added successfully");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static bool RollNumberExists(int rollNumber)
        {
            List<Student> students = Deserialization();
            return students.Any(student => student.RollNumber == rollNumber);
        }


        public static void DetailInfo()
        {
            //XML-დან დესერიალიზაცია
            List<Student> students = Deserialization();

            //თითოეული სტუდენტის დეტალის ბეჭვდა
            foreach (var item in students)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Student name: {item.FullName}, roll number: {item.RollNumber}, grade - {item.Grade} ");
                Console.ForegroundColor = ConsoleColor.White;

            }
            Console.WriteLine();
        }

        public static bool SearchStudent(int rollNumber)
        {
            List<Student> students = Deserialization();

            var findStudent = students.Find(s => s.RollNumber.Equals(rollNumber));
            if (findStudent != null)
            {
                // დეტალების ბეჭვდა სტუდენტის პოვნის შემთხვევაში
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Student name: {findStudent.FullName}, roll number: {findStudent.RollNumber}, grade - {findStudent.Grade} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                return true; // //დააბრუნეთ true, რაც მიუთითებს რომ სტუდენტი მოიძებნა.
            }
            else
            {
                // სტუდენტის ვერ პოვნის შემთხვევაში
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Student not found.");
                Console.ForegroundColor = ConsoleColor.White;
                return false; //დააბრუნეთ false, რაც მიუთითებს რომ სტუდენტი ვერ მოიძებნა.
            }
        }


        public static bool UpdateGrade(int rollNumber)
        {
            List<Student> students = Deserialization();
            var studentToEdit = students.Find(f => f.RollNumber.Equals(rollNumber));

            if (studentToEdit != null)
            {
                Console.WriteLine($"Current grade of {studentToEdit.FullName} is {studentToEdit.Grade}.");
                Console.WriteLine("Enter new grade (A, B, C, D, E, F): ");
                char newGrade = ' ';
                bool isValidGrade = false;
                while (!isValidGrade)
                {
                    string input = Console.ReadLine().ToUpper();
                    if (!string.IsNullOrEmpty(input) && input.Length == 1 && "ABCDEF".Contains(input[0]))
                    {
                        newGrade = input[0]; //  ვალიდური შეფასების მინიჭება
                        isValidGrade = true; // ციკლიდან გამოსვლა

                        // მიანიჭეთ ახალი შეფასება სტუდენტის Grade property-ს
                        studentToEdit.Grade = newGrade;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Grade updated successfully!");
                        Console.ForegroundColor = ConsoleColor.White;

                        //სერიალიზირება 
                        Serialization(students);
                        return true;
                    }
                    else
                    {
                        // წარუმატებლობის დროს 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter a valid grade (A, B, C, D, E, F): ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Student not found.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            return false; //  წარუმატებლობა (სტუდენტი ვერ მოიძებნა ან არავალიდურია შეფასების input-ი)
        }

        public static void RemoveStudent(int rollNum)
        {
            //XML-დან დესერიალიზაცია
            List<Student> students = Deserialization();
            var studentToRemove = students.Find(r => r.RollNumber.Equals(rollNum));
            //წაშლა
            if (studentToRemove != null)
            {
                students.Remove(studentToRemove);
                Serialization(students);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Student '{studentToRemove.FullName}' removed successfully.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Student not found.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        //სერიალიზაცია სტუდენტების სიის XML -ად
        private static void Serialization(List<Student> moviesList)
        {
            XmlSerializer serializer = new (typeof(List<Student>));
            using StreamWriter writer = new(path);
            serializer.Serialize(writer, moviesList);
        }
        //სტუდენტების სიის დესერიალიზაცია XML-დან, ვაბრუნებთ ცარიელ სიას თუ ფაილი არ არსებობს
        private static List<Student> Deserialization()
        {
            if (!File.Exists(path))
            {
                return [];
            }

            XmlSerializer serializer = new (typeof(List<Student>));
            using StreamReader reader = new(path);
            return (List<Student>)serializer.Deserialize(reader);
        }
    }
}
