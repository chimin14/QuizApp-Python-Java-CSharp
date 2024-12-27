using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
 
class Program
{
    static string quizDataFile = ("../quizData.json");
    static QuizData quizData = new QuizData();
    static Dictionary<string, User> userData = new Dictionary<string, User>();
    static List<Question> questions = new List<Question>();
 
    static void Main(string[] args)
    {
        PreprocessJsonFile(quizDataFile);
        LoadQuestionsFromFile();
 
        if (questions == null || questions.Count == 0)
        {
            Console.WriteLine("No questions found in the JSON file. Please add questions to 'quizData.json' before running the quiz.");
            return;
        }
 
        MainMenu();
    }
 
    static void PreprocessJsonFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            json = json.Replace("\"totalScore\": null", "\"totalScore\": 0");
            File.WriteAllText(filePath, json);
        }
    }
 
    static void LoadQuestionsFromFile()
    {
        if (File.Exists(quizDataFile))
        {
            string json = File.ReadAllText(quizDataFile);
            try
            {
                quizData = JsonConvert.DeserializeObject<QuizData>(json) ?? new QuizData();
                Console.WriteLine("Quiz data successfully loaded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading quiz data: " + ex.Message);
                quizData = new QuizData();
            }
        }
        else
        {
            Console.WriteLine($"Quiz data file not found at {quizDataFile}. Please ensure it exists.");
            quizData = new QuizData();
        }
 
        userData = quizData.Users ?? new Dictionary<string, User>();
        questions = quizData.Questions ?? new List<Question>();
    }
 
    static void SaveQuizData()
    {
        string json = JsonConvert.SerializeObject(quizData, Formatting.Indented);
        File.WriteAllText(quizDataFile, json);
    }
 
    static void MainMenu()
    {
        Console.WriteLine("Welcome to the Quiz App!");
        Console.Write("Do you want to (1) Log in or (2) Sign up? ");
        string choice = Console.ReadLine();
 
        if (choice == "1")
        {
            Login();
        }
        else if (choice == "2")
        {
            SignUp();
        }
        else
        {
            Console.WriteLine("Invalid choice. Please choose 1 or 2.");
            MainMenu();
        }
    }
 
    static void Login()
    {
        Console.Write("Enter your username: ");
        string username = Console.ReadLine();
 
        if (userData.ContainsKey(username))
        {
            Console.WriteLine($"Welcome back, {username}! Here is your quiz history:");
            foreach (var history in userData[username].History ?? new List<QuizHistory>())
            {
                Console.WriteLine($"Date: {history.Date}, Difficulty: {history.Difficulty}, Score: {history.Score}");
            }
 
            if (userData[username].CurrentProgress != null)
            {
                Console.Write("You have saved progress. Do you want to resume? (yes/no): ");
                if (Console.ReadLine()?.Trim().ToLower() == "yes")
                {
                    ResumeQuiz(username);
                    return;
                }
            }
 
            SelectDifficulty(username);
        }
        else
        {
            Console.WriteLine("User not found. Please sign up.");
            MainMenu();
        }
    }
 
    static void SignUp()
    {
        Console.Write("Choose a username: ");
        string username = Console.ReadLine();
 
        if (userData.ContainsKey(username))
        {
            Console.WriteLine("Username already exists. Please choose another.");
            SignUp();
        }
        else
        {
            userData[username] = new User { History = new List<QuizHistory>() };
            SaveQuizData();
            Console.WriteLine($"Welcome, {username}! Your account has been created.");
            SelectDifficulty(username);
        }
    }
 
    static void SelectDifficulty(string username)
    {
        Console.Write("Choose difficulty: (1) Beginner, (2) Intermediate, or (3) Hard: ");
        string difficultyChoice = Console.ReadLine();
 
        if (difficultyChoice == "1" || difficultyChoice == "2" || difficultyChoice == "3")
        {
            string selectedDifficulty = difficultyChoice == "1" ? "beginner" : (difficultyChoice == "2" ? "intermediate" : "hard");
            AskQuestions(username, selectedDifficulty);
        }
        else
        {
            Console.WriteLine("Invalid choice. Please choose 1, 2, or 3.");
            SelectDifficulty(username);
        }
    }
 
    static void AskQuestions(string username, string difficulty, List<Question>? quizQuestions = null, QuizProgress? progress = null)
    {
        int score = progress?.CurrentScore ?? 0;
        int totalCorrectAnswers = progress?.TotalCorrectAnswers ?? 0;
        int totalIncorrectAnswers = progress?.TotalIncorrectAnswers ?? 0;
        int startIndex = progress?.CurrentQuestionIndex ?? 0;
        var selectedQuestions = quizQuestions ?? questions
            .Where(q => q.Difficulty == difficulty)
            .OrderBy(_ => Guid.NewGuid())
            .Take(10)
            .ToList();
 
        for (int i = startIndex; i < selectedQuestions.Count; i++)
        {
            var question = selectedQuestions[i];
            Console.WriteLine($"\nQuestion: {question.QuestionText}");
 
            if (question.Type == "true_false")
            {
                Console.WriteLine("Answer with 'true' or 'false'.");
            }
            else
            {
                for (int j = 0; j < question.Choices.Length; j++)
                {
                    Console.WriteLine($"{(char)('A' + j)}. {question.Choices[j]}");
                }
            }
 
            Console.Write("\nYour answer (or type 'stop' to save and exit): ");
            string answer = Console.ReadLine();
 
            if (answer.Trim().ToLower() == "stop")
            {
                var updatedProgress = new QuizProgress
                {
                    Difficulty = difficulty,
                    CurrentQuestionIndex = i,
                    CurrentScore = score,
                    TotalCorrectAnswers = totalCorrectAnswers,
                    TotalIncorrectAnswers = totalIncorrectAnswers,
                    RemainingQuestionIds = selectedQuestions.Skip(i).Select(q => q.Id).ToList()
                };
                SaveProgress(username, updatedProgress);
                Console.WriteLine("Progress saved. You can resume later.");
                return;
            }
 
            var correctAnswersList = question.Answer.Split("||").Select(a => a.Trim().ToUpper());
            if (correctAnswersList.Contains(answer.Trim().ToUpper()))
            {
                Console.WriteLine("Correct!");
                score++;
                totalCorrectAnswers++;
            }
            else
            {
                Console.WriteLine($"Wrong! The correct answer was {correctAnswersList.First()}.");
                totalIncorrectAnswers++;
            }
        }
 
        Console.WriteLine($"\nQuiz completed for {difficulty}! Your score: {score}/{selectedQuestions.Count}");
        Console.WriteLine($"Total correct answers: {totalCorrectAnswers}");
        Console.WriteLine($"Total incorrect answers: {totalIncorrectAnswers}");
 
        userData[username].History.Add(new QuizHistory
        {
            Date = DateTime.Now.ToString("o"),
            Difficulty = difficulty,
            Score = score
        });
 
        userData[username].TotalScore += score;
        userData[username].TotalCorrectAnswers += totalCorrectAnswers;
        userData[username].TotalIncorrectAnswers += totalIncorrectAnswers;
 
        userData[username].CurrentProgress = null;
        SaveQuizData();
 
        if (difficulty != "hard")
        {
            bool validChoice = false;
            while (!validChoice)
            {
                Console.Write("Do you want to move to the next level? (yes/no): ");
                string response = Console.ReadLine()?.Trim().ToLower();
                if (response == "yes")
                {
                    string nextDifficulty = difficulty == "beginner" ? "intermediate" : "hard";
                    AskQuestions(username, nextDifficulty);
                    validChoice = true;
                }
                else if (response == "no")
                {
                    Console.WriteLine("Thank you for playing!");
                    validChoice = true;
                }
                else
                {
                    Console.WriteLine("Invalid response. Please answer with 'yes' or 'no'.");
                }
            }
        }
    }
 
    static void SaveProgress(string username, QuizProgress progress)
    {
        userData[username].CurrentProgress = progress;
        SaveQuizData();
    }
 
    static void ResumeQuiz(string username)
    {
        var progress = userData[username].CurrentProgress;
        if (progress != null)
        {
            var remainingQuestions = progress.RemainingQuestionIds
                .Select(id => questions.FirstOrDefault(q => q.Id == id))
                .Where(q => q != null)
                .ToList();
 
            AskQuestions(username, progress.Difficulty, remainingQuestions, progress);
        }
    }
}
 
class QuizData
{
    public Dictionary<string, User> Users { get; set; } = new Dictionary<string, User>();
    public List<Question> Questions { get; set; } = new List<Question>();
}
 
class User
{
    public List<QuizHistory> History { get; set; } = new List<QuizHistory>();
    public QuizProgress? CurrentProgress { get; set; } = null;
    public int? TotalScore { get; set; } = 0;
    public int TotalCorrectAnswers { get; set; } = 0;
    public int TotalIncorrectAnswers { get; set; } = 0;
}
 
class QuizHistory
{
    public string Date { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public int Score { get; set; } = 0;
}
 
class QuizProgress
{
    public string Difficulty { get; set; } = string.Empty;
    public int CurrentQuestionIndex { get; set; } = 0;
    public int CurrentScore { get; set; } = 0;
    public int TotalCorrectAnswers { get; set; } = 0;
    public int TotalIncorrectAnswers { get; set; } = 0;
    public List<int> RemainingQuestionIds { get; set; } = new List<int>();
}
 
class Question
{
    [JsonProperty("id")]
    public int Id { get; set; }
 
    [JsonProperty("category")]
    public string Category { get; set; } = string.Empty;
 
    [JsonProperty("difficulty")]
    public string Difficulty { get; set; } = string.Empty;
 
    [JsonProperty("question")]
    public string QuestionText { get; set; } = string.Empty;
 
    [JsonProperty("choices")]
    public string[] Choices { get; set; } = Array.Empty<string>();
 
    [JsonProperty("answer")]
    public string Answer { get; set; } = string.Empty;
 
    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;
}
 
