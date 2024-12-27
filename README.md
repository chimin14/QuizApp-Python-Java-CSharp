# Multi-Language Interactive Knowledge Quiz

## Project Overview

The Multi-Language Interactive Knowledge Quiz is a console-based application that provides an engaging, customizable quiz experience for users across various knowledge areas. Developed in Python, and C#, this project demonstrates cross-language programming proficiency while offering an educational tool that helps users test their knowledge in selected categories and difficulty levels.

## Project Goals

- Educational Tool: To create an interactive quiz that helps users learn and test their knowledge on various topics.
- Cross-Language Practice: To showcase skills in Python, C#, making the quiz accessible across multiple platforms.
- Feature-Rich Quiz Experience: To implement features such as category selection, difficulty customization, real-time feedback, and progress saving, creating a dynamic and user-friendly quiz.

## Key Features

1. Multi-Category Selection: Users can select from multiple categories (e.g., Science, Technology, History) for a customized quiz experience.
2. Customizable Difficulty Levels: The quiz offers beginner, intermediate, and advanced levels to suit different knowledge levels.
3. Dynamic Question Types: Includes multiple-choice, true/false, and fill-in-the-blank question types to add variety.
4. Random Question Ordering: Shuffles questions at the start of each quiz for a unique experience every time.
5. Real-Time Feedback: Provides immediate feedback on whether each answer is correct or incorrect.
6. Performance Tracking: Tracks scores and provides a final report of correct and incorrect answers.
7. Multi-Language Support: Developed in Python, and C# to support multiple platforms.
8. Progress Saving: Allows users to save their quiz progress and resume later.

## Technologies Used

- Python 3.x
- .NET Core SDK (for C#)

## Project Structure
```

Multi-Language_Interactive_Knowledge_Quiz
│
├── Python/
│   ├── quiz_app.py
│
├── C#/
│   ├── QuizApp.cs
│
└── questions.json
```

## How to Download and Install

1. Download:
   - Clone this repository using:
     ```bash
     git clone https://github.com/your-username/Multi-Language_Interactive_Knowledge_Quiz.git
     ```

2. Navigate to the Project Folder:
   - Move into the project directory:
     ```bash
     cd Multi-Language_Interactive_Knowledge_Quiz
     ```

3. Ensure You Have the Required Tools:
   - Install Python 3.x, Node.js (for ), and .NET Core SDK on your machine if they aren’t already installed.

## How to Run

1. Run the Application:
   - Python: Navigate to the Python folder and execute:
     ```bash
     python knowledge_quiz.py
     ```
   - : Navigate to the  folder and execute:
     ```bash
     node knowledgeQuiz.js

     ```
   - C#: Navigate to the C# folder and execute:
     ```bash
     dotnet run KnowledgeQuiz.cs
     ```


2. JSON Questions File:
   - The `questions.json` file should be placed in the root of each language folder. This file contains the quiz questions, categories, and answers.

## JSON Structure

The `questions.json` file should follow this structure to define quiz questions:

```json
[
    {
        "category": "Science",
        "difficulty": "beginner",
        "question": "What is the boiling point of water?",
        "type": "multiple-choice",
        "choices": ["90°C", "100°C", "110°C", "120°C"],
        "answer": "100°C"
    },
    {
        "category": "Technology",
        "difficulty": "advanced",
        "question": "True or False: The internet was originally called ARPANET.",
        "type": "true-false",
        "answer": "True"
    }
]
 ```

## User Interaction
Starting the Quiz
Upon launching the quiz application, the user is prompted to select a category and difficulty level. 

### Example:
 ```
Welcome to the Interactive Knowledge Quiz!
Please choose a category:
1. Science
2. Technology
3. History
 ```

## Answering Questions
Each question is presented with multiple answer choices, or as a true/false or fill-in-the-blank format.
The user inputs their answer by selecting the corresponding option number or typing their answer directly.

### Example (Multiple Choice):
```
What is the boiling point of water?
1. 90°C
2. 100°C
3. 110°C
4. 120°C
Enter your answer (1-4):
```
### Example (True or False)

```True or False: The internet was originally called ARPANET.
Enter your answer (True/False):
```

## Feedback and Scoring

After each answer, the application provides immediate feedback on whether the answer was correct.
### Example:
```
Correct! The boiling point of water is 100°C.
```

## Saving and Resuming Progress
Users can save their progress mid-quiz and resume it later.

### Example:
```
Would you like to save your progress? (yes/no):
```
# Ending the Quiz and Report

At the end of the quiz, a summary report is generated, displaying the number of correct and incorrect answers.
Example:

```
Quiz Completed!
Correct Answers: 3
Incorrect Answers: 2
Final Score: 60%
```

# Function Details

## 1. LoadQuestionsFromFile
   - Purpose: Reads quiz questions from the JSON file and returns a list of questions.
   - How It Works: Opens `questions.json`, parses content, and validates each question format.
   - Output: Returns a list of question objects for the quiz.

 ## 2. ValidateQuestions
   - Purpose: Ensures that all questions loaded from the JSON file follow the correct format.
   - How It Works: Checks each question for required fields such as `category`, `difficulty`, `question`, `type`, and `answer`.
   - Output: Returns `true` if valid or throws an error if the format is incorrect.

## 3. AskQuestions
   - Purpose: Handles the quiz flow, presenting each question and collecting answers.
   - How It Works: Randomly shuffles questions, displays each question with choices, and validates answers. Provides immediate feedback on correctness.
   - Output: Tracks the number of correct and incorrect answers.

## 4. TrackScore
   - Purpose: Keeps track of user scores by counting correct and incorrect responses.
   - How It Works: Compares user responses to correct answers and updates score counters.
   - Output: Updates score data during the quiz.

## 5. GenerateReport
   - Purpose: Provides a summary of the user’s performance at the end of the quiz.
   - How It Works: After all questions are answered, calculates the total score and displays a report of correct and incorrect answers.
   - Output: Prints a final performance report to the console.

## 6. DisplayCountdown
   - Purpose: Adds a countdown timer for each question to create a timed quiz challenge.
   - How It Works: Starts a timer for each question and submits the answer automatically if time runs out.
   - Output: Displays a countdown for each question.

## 7. SaveProgress
   - Purpose: Allows users to save their quiz progress.
   - How It Works: Writes current progress (current question, score, etc.) to a file, allowing users to resume later.
   - Output: A saved file in JSON format for future continuation.

## 8. LoadProgress
   - Purpose: Loads a previously saved quiz session so users can resume from where they left off.
   - How It Works: Reads saved progress from a file and restores quiz state.
   - Output: Restores saved quiz progress, including score and current question.
## 9. ReviewAnswers
   - Purpose: Allows users to review their answers at the end of the quiz, showing which questions were answered correctly or incorrectly.
   - How it works: After completing the quiz, display each question with the user's answers, the correct answer.
   - Output: A detailed review screen summarizing performance.

 ## Future Enhancements
- Timed Mode: Introduce a timed mode to add a level of challenge.
- Additional Question Types: Expand to new question types, such as matching or ordering.
- Question Difficulty Adjustment: Develop a feature where the difficulty level adjusts based on the user’s performance. For instance, if a user answers several questions correctly in a row, the difficulty level increases.


