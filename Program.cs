using System;
class Program
{
    static Student[] students = new Student[100];
    static int studentCount = 0;
    static int subjectCount = 5;
    static void Main()
    {
        Console.WriteLine("Student Result Management System");
        DrawLine();
        Pause();
        MainLoop();
    }
    static void MainLoop()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            DrawLine();
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Remove Student");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. View All Students");
            Console.WriteLine("5. Search Student");
            Console.WriteLine("6. Show Topper Student");
            Console.WriteLine("7. Show Subject Averages");
            Console.WriteLine("8. Show Grade Distribution");
            Console.WriteLine("9. Exit");
            DrawLine();
            Console.Write("Select (1-9): ");
            if (!int.TryParse(Console.ReadLine(), out int choice)) continue;
            switch (choice)
            {
                case 1: AddStudent(); break;
                case 2: RemoveStudent(); break;
                case 3: UpdateStudent(); break;
                case 4: ViewStudents(); break;
                case 5: SearchStudent(); break;
                case 6: ShowTopperStudent(); break;
                case 7: ShowSubjectAverages(); break;
                case 8: ShowGradeDistribution(); break;
                case 9: return;
            }
        }
    }
    static void AddStudent()
    {
        if (studentCount >= students.Length)
        {
            Console.WriteLine("Student limit reached.");
            Pause();
            return;
        }
        Student s = new Student();
        s.RollNo = ReadInt("Enter Roll No: ");
        for (int i = 0; i < studentCount; i++)
        {
            if (students[i].RollNo == s.RollNo)
            {
                Console.WriteLine("Roll number already exists.");
                Pause();
                return;
            }
        }
        Console.Write("Enter Name: ");
        s.Name = Console.ReadLine() ?? "";
        s.Marks = new int[subjectCount];
        int total = 0;
        for (int i = 0; i < subjectCount; i++)
        {
            s.Marks[i] = ReadMarks($"Enter marks for Subject {i + 1}: ");
            total += s.Marks[i];
        }
        s.Percentage = total / (double)subjectCount;
        s.Grade = CalculateGrade(s.Percentage);
        students[studentCount++] = s;
        Console.WriteLine("Student added successfully.");
        Pause();
    }
    static void UpdateStudent()
    {
        int roll = ReadInt("Enter Roll No to update: ");

        for (int i = 0; i < studentCount; i++)
        {
            if (students[i].RollNo == roll)
            {
                Console.Write("Enter new name: ");
                students[i].Name = Console.ReadLine() ?? "";
                int total = 0;
                for (int j = 0; j < subjectCount; j++)
                {
                    students[i].Marks[j] = ReadMarks($"Enter marks for Subject {j + 1}:"); 
                    total += students[i].Marks[j];
                }
                students[i].Percentage = total / (double)subjectCount;
                students[i].Grade = CalculateGrade(students[i].Percentage);

                Console.WriteLine("Student updated.");
                Pause();
                return;
            }
        }
        Console.WriteLine("Student not found.");
        Pause();
    }
    static void RemoveStudent()
    {
        int roll = ReadInt("Enter Roll No to remove: ");

        for (int i = 0; i < studentCount; i++)
        {
            if (students[i].RollNo == roll)
            {
                for (int j = i; j < studentCount - 1; j++)
                    students[j] = students[j + 1];

                studentCount--;
                Console.WriteLine("Student removed.");
                Pause();
                return;
            }
        }
        Console.WriteLine("Student not found.");
        Pause();
    }
    static void ViewStudents()
    {
        if (studentCount == 0)
        {
            Console.WriteLine("No students available.");
            Pause();
            return;
        }
        for (int i = 0; i < studentCount; i++)
        {
            Console.WriteLine($"Roll No: {students[i].RollNo}");
            Console.WriteLine($"Name: {students[i].Name}");
            Console.WriteLine($"Percentage: {students[i].Percentage:F2}%");
            Console.WriteLine($"Grade: {students[i].Grade}");
            DrawLine();
        }
        Pause();
    }
    static void SearchStudent()
    {
        int roll = ReadInt("Enter Roll No to search: ");

        for (int i = 0; i < studentCount; i++)
        {
            if (students[i].RollNo == roll)
            {
                Console.WriteLine($"Name: {students[i].Name}");
                Console.WriteLine($"Percentage: {students[i].Percentage:F2}%");
                Console.WriteLine($"Grade: {students[i].Grade}");
                Pause();
                return;
            }
        }
        Console.WriteLine("Student not found.");
        Pause();
    }
    static void ShowTopperStudent()
    {
        if (studentCount == 0)
        {
            Console.WriteLine("No students available.");
            Pause();
            return;
        }
        Student topper = students[0];
        for (int i = 1; i < studentCount; i++)
            if (students[i].Percentage > topper.Percentage)
                topper = students[i];
        Console.WriteLine($"Topper: {topper.Name}");
        Console.WriteLine($"Percentage: {topper.Percentage:F2}%");
        Console.WriteLine($"Grade: {topper.Grade}");
        Pause();
    }
    static void ShowSubjectAverages()
    {
        for (int i = 0; i < subjectCount; i++)
        {
            double sum = 0;
            for (int j = 0; j < studentCount; j++)
                sum += students[j].Marks[i];
            Console.WriteLine($"Subject {i + 1} Avg: {sum / studentCount:F2}");
        }
        Pause();
    }
    static void ShowGradeDistribution()
    {
        int A = 0, B = 0, C = 0, D = 0, F = 0;
        for (int i = 0; i < studentCount; i++)
        {
            switch (students[i].Grade)
            {
                case "A": A++; break;
                case "B": B++; break;
                case "C": C++; break;
                case "D": D++; break;
                default: F++; break;
            }
        }
        Console.WriteLine("A:" + A + " B:" + B + " C:" + C + " D:" + D + " F:" + F);
        Pause();
    }
    static int ReadInt(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            if (int.TryParse(Console.ReadLine(), out int v)) return v;
            else Console.WriteLine("Invalid input.");
        }
    }
    static int ReadMarks(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            if (int.TryParse(Console.ReadLine(), out int m) && m >= 0 && m <= 100)
                return m;
            Console.WriteLine("Marks must be 0-100.");
        }
    }
    static string CalculateGrade(double p)
    {
        if (p >= 80) return "A";
        if (p >= 70) return "B";
        if (p >= 60) return "C";
        if (p >= 50) return "D";
        return "F";
    }
    static void DrawLine() => Console.WriteLine("===============");
    static void Pause() { Console.WriteLine("Press Enter..."); Console.ReadLine(); }
}
class Student
{
    public int RollNo;
    public string Name = "";
    public int[] Marks = [];
    public double Percentage;
    public string Grade = "";
}
;