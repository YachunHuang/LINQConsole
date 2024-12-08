namespace LINQConsole.service
{
    internal class ListService : IListService
    {
        public void Execute()
        {
            var evenNumbers = FilterEvenNumbers();
            Console.WriteLine("篩選出的偶數:");
            Console.WriteLine(string.Join(", ", evenNumbers)); 

            Console.WriteLine();

            var filteredNames = FilterAndSortNames();
            Console.WriteLine("以 'A' 開頭並排序的字串列表:");
            Console.WriteLine(string.Join(", ", filteredNames));

            Console.WriteLine();

            var sum = CalculateSum();
            Console.WriteLine($"整數列表的總和是: {sum}");

            var names = GetNames();
            Console.WriteLine();

            Console.WriteLine("取得的名字列表:");
            Console.WriteLine(string.Join(", ", names));

            Console.WriteLine();

            var ageGroups = GroupAndCountByAge();
            Console.WriteLine("年齡分組與人數統計:");

            foreach (var group in ageGroups)
            {
                Console.WriteLine($"年齡: {group.Age}, 人數: {group.Count}");
            }
            Console.WriteLine();

            var topThreeNumbers = GetTopThreeNumbers();
            Console.WriteLine("最大的 3 個值（降序排列）:");
            Console.WriteLine(string.Join(", ", topThreeNumbers));

            Console.WriteLine();
            var sortedPeople = SortPeopleByAgeAndName();

            Console.WriteLine("按年齡升序排序，年齡相同則按姓名字母順序排序:");
            foreach (var person in sortedPeople)
            {
                Console.WriteLine($"{person.Name}, {person.Age}");
            }

            Console.WriteLine();
            var studentGrades = JoinStudentsWithGrades();

            Console.WriteLine("學生與成績配對結果:");
            foreach (var studentGrade in studentGrades)
            {
                Console.WriteLine($"Name: {studentGrade.Name}, Score: {studentGrade.Score}");
            }
            Console.WriteLine();
            var studentAverages = CalculateAverageScores();

            Console.WriteLine("學生的平均分數:");
            foreach (var studentAverage in studentAverages)
            {
                Console.WriteLine($"Name: {studentAverage.Name}, Average Score: {studentAverage.AverageScore:F2}");
            }
        }

        /// <summary>
        /// 篩選整數列表中的偶數
        /// </summary>
        /// <returns>偶數列表</returns>
        private List<int> FilterEvenNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            return numbers.Where(num => num % 2 == 0).ToList();
        }

        /// <summary>
        /// 篩選以 "A" 開頭並按字母順序排序的字串
        /// </summary>
        /// <returns>篩選並排序後的字串列表</returns>
        private List<string> FilterAndSortNames()
        {
            List<string> names = new List<string> { "Apple", "banana", "Apricot", "avocado", "Blueberry" };
            // 不分大小寫 StringComparison.OrdinalIgnoreCase
            return names
                .Where(name => name.StartsWith("A", StringComparison.OrdinalIgnoreCase))
                .OrderBy(name => name)
                .ToList();
        }

        /// <summary>
        /// 計算整數列表的總和
        /// </summary>
        /// <returns></returns>
        private int CalculateSum()
        {
            List<int> numbers = new List<int> { 10, 20, 30, 40, 50 };
            return numbers.Sum();
        }

        /// <summary>
        /// 取得人員列表中的名字
        /// </summary>
        /// <returns>名字列表</returns>
        private List<string> GetNames()
        {
            var people = new List<Person>
        {
            new Person { Name = "Alice", Age = 25 },
            new Person { Name = "Bob", Age = 30 },
            new Person { Name = "Charlie", Age = 35 },
        };
            return people.Select(person => person.Name).ToList();
        }

        /// <summary>
        /// 將人員按年齡分組並統計人數
        /// </summary>
        /// <returns>年齡與人數的對應</returns>
        private List<AgeGroup> GroupAndCountByAge()
        {
            var people = new List<Person>
        {
            new Person { Name = "Alice", Age = 25 },
            new Person { Name = "Bob", Age = 30 },
            new Person { Name = "Charlie", Age = 25 },
            new Person { Name = "Dave", Age = 30 },
            new Person { Name = "Eve", Age = 35 },
        };
            return people
                   .GroupBy(person => person.Age) 
                   .Select(group => new AgeGroup
                   {
                       Age = group.Key,
                       Count = group.Count()
                   })
                   .ToList();
        }

        /// <summary>
        /// 找出整數列表中最大的 3 個值並按降序排列
        /// </summary>
        /// <returns>最大的 3 個值（降序排列）</returns>
        private List<int> GetTopThreeNumbers()
        {
            List<int> numbers = new List<int> { 15, 42, 89, 33, 5, 76, 12, 98, 54 };
            return numbers
                .OrderByDescending(num => num) 
                .Take(3) 
                .ToList(); 
        }

        /// <summary>
        /// 按年齡升序排序，若年齡相同則按姓名字母順序排序
        /// </summary>
        /// <returns>排序後的列表</returns>
        static List<Person> SortPeopleByAgeAndName()
        {
            var people = new List<Person>
        {
            new Person { Name = "Charlie", Age = 30 },
            new Person { Name = "Alice", Age = 25 },
            new Person { Name = "Bob", Age = 25 },
            new Person { Name = "Dave", Age = 30 },
        };
            return people
                .OrderBy(person => person.Age) 
                .ThenBy(person => person.Name)
                .ToList(); 
        }

        /// <summary>
        /// 將學生與成績進行關聯
        /// </summary>
        /// <returns>學生與成績的關聯</returns>
        static List<StudentGrade> JoinStudentsWithGrades()
        {
            var students = new List<Student>
        {
            new Student { Id = 1, Name = "Alice" },
            new Student { Id = 2, Name = "Bob" },
            new Student { Id = 3, Name = "Charlie" },
            new Student { Id = 4, Name = "Dave" } // Dave 沒有成績
        };

            var grades = new List<Grade>
        {
            new Grade { StudentId = 1, Score = 85 },
            new Grade { StudentId = 2, Score = 92 },
            new Grade { StudentId = 3, Score = 78 },
        };

            return students
                .GroupJoin(
                    grades,
                    student => student.Id,
                    grade => grade.StudentId,
                    (student, studentGrades) => new StudentGrade
                    {
                        Name = student.Name,
                        Score = studentGrades.FirstOrDefault()?.Score ?? 0
                    })
                .ToList();
        }

        /// <summary>
        /// 計算每位學生的平均分數。
        /// </summary>
        /// <returns>學生與分數</returns>
        static List<StudentAverage> CalculateAverageScores()
        {
            var students = new List<Student>
        {
            new Student { Id = 1, Name = "Alice" },
            new Student { Id = 2, Name = "Bob" },
        };

            var scores = new List<StudentScore>
        {
            new StudentScore  { StudentId = 1, Subject = "Math", Score = 85 },
            new StudentScore  { StudentId = 1, Subject = "English", Score = 90 },
            new StudentScore  { StudentId = 2, Subject = "Math", Score = 78 },
            new StudentScore  { StudentId = 2, Subject = "English", Score = 88 },
        };

            return students
                .GroupJoin(
                    scores,
                    student => student.Id,
                    score => score.StudentId,
                    (student, studentScores) => new StudentAverage
                    {
                        Name = student.Name,
                        AverageScore = studentScores.Any()
                            ? studentScores.Average(s => s.Score)
                            : 0
                    })
                .ToList();
        }
  
        class AgeGroup
        {
            public int Age { get; set; }
            public int Count { get; set; }
        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class Grade
        {
            public int StudentId { get; set; }
            public int Score { get; set; }
        }

        class StudentGrade
        {
            public string Name { get; set; }
            public int Score { get; set; }
        }


        class StudentScore
        {
            public int StudentId { get; set; }
            public string Subject { get; set; }
            public int Score { get; set; }
        }

        class StudentAverage
        {
            public string Name { get; set; }
            public double AverageScore { get; set; }
        }
    }
}
