# Multi-Language Interactive Knowledge Quiz

## Project Overview

The Multi-Language Interactive Knowledge Quiz is a console-based application that provides an engaging, customizable quiz experience for users across various knowledge areas. Developed in Python, JavaScript, and C#, this project demonstrates cross-language programming proficiency while offering an educational tool that helps users test their knowledge in selected categories and difficulty levels.

## Project Goals

- Educational Tool: To create an interactive quiz that helps users learn and test their knowledge on various topics.
- Cross-Language Practice: To showcase skills in Python, JavaScript, C#, making the quiz accessible across multiple platforms.
- Feature-Rich Quiz Experience: To implement features such as category selection, difficulty customization, real-time feedback, and progress saving, creating a dynamic and user-friendly quiz.

## Key Features

1. Multi-Category Selection: Users can select from multiple categories (e.g., Science, Technology, History) for a customized quiz experience.
2. Customizable Difficulty Levels: The quiz offers beginner, intermediate, and advanced levels to suit different knowledge levels.
3. Dynamic Question Types: Includes multiple-choice, true/false, and fill-in-the-blank question types to add variety.
4. Random Question Ordering: Shuffles questions at the start of each quiz for a unique experience every time.
5. Real-Time Feedback: Provides immediate feedback on whether each answer is correct or incorrect.
6. Performance Tracking: Tracks scores and provides a final report of correct and incorrect answers.
7. Multi-Language Support: Developed in Python, JavaScript, and C# to support multiple platforms.
8. Progress Saving: Allows users to save their quiz progress and resume later.

## Technologies Used

- Python 3.x
- JavaScript (Node.js)
- .NET Core SDK (for C#)

## Project Structure

Multi-Language_Interactive_Knowledge_Quiz
│
├── Python/
│   ├── knowledge_quiz.py
│
├── JavaScript/
│   ├── knowledgeQuiz.js
│
├── C#/
│   ├── KnowledgeQuiz.cs
│
├── VB.NET/
│   ├── KnowledgeQuiz.vb
│
└── questions.json

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
   - Install Python 3.x, Node.js (for JavaScript), and .NET Core SDK on your machine if they aren’t already installed.

## How to Run

1. Run the Application:
   - Python: Navigate to the Python folder and execute:
     ```bash
     python knowledge_quiz.py
     ```
   - JavaScript: Navigate to the JavaScript folder and execute:
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


Function Details

 1. LoadQuestionsFromFile
   - Purpose: Reads quiz questions from the JSON file and returns a list of questions.
   - How It Works: Opens `questions.json`, parses content, and validates each question format.
   - Output: Returns a list of question objects for the quiz.

 2. ValidateQuestions
   - Purpose: Ensures that all questions loaded from the JSON file follow the correct format.
   - How It Works: Checks each question for required fields such as `category`, `difficulty`, `question`, `type`, and `answer`.
   - Output: Returns `true` if valid or throws an error if the format is incorrect.

 3. AskQuestions
   - Purpose: Handles the quiz flow, presenting each question and collecting answers.
   - How It Works: Randomly shuffles questions, displays each question with choices, and validates answers. Provides immediate feedback on correctness.
   - Output: Tracks the number of correct and incorrect answers.

 4. TrackScore
   - Purpose: Keeps track of user scores by counting correct and incorrect responses.
   - How It Works: Compares user responses to correct answers and updates score counters.
   - Output: Updates score data during the quiz.

 5. GenerateReport
   - Purpose: Provides a summary of the user’s performance at the end of the quiz.
   - How It Works: After all questions are answered, calculates the total score and displays a report of correct and incorrect answers.
   - Output: Prints a final performance report to the console.

 6. DisplayCountdown
   - Purpose: Adds a countdown timer for each question to create a timed quiz challenge.
   - How It Works: Starts a timer for each question and submits the answer automatically if time runs out.
   - Output: Displays a countdown for each question.

 7. SaveProgress
   - Purpose: Allows users to save their quiz progress.
   - How It Works: Writes current progress (current question, score, etc.) to a file, allowing users to resume later.
   - Output: A saved file in JSON format for future continuation.

8. LoadProgress
   - Purpose: Loads a previously saved quiz session so users can resume from where they left off.
   - How It Works: Reads saved progress from a file and restores quiz state.
   - Output: Restores saved quiz progress, including score and current question.

