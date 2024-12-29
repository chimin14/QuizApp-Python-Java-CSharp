# Multi-Language Interactive Knowledge Quiz

## Project Overview

The Multi-Language Interactive Knowledge Quiz is a console-based application that provides an engaging, customizable quiz experience for users across various knowledge areas. Developed in Python and C#, this project demonstrates cross-language programming proficiency while offering an educational tool that helps users test their knowledge in selected categories and difficulty levels.

## Project Goals

- **Educational Tool**: To create an interactive quiz that helps users learn and test their knowledge on various topics.
- **Cross-Language Practice**: To showcase skills in Python and C#, making the quiz accessible across multiple platforms.
- **Feature-Rich Quiz Experience**: To implement features such as category selection, difficulty customization, real-time feedback, and progress saving, creating a dynamic and user-friendly quiz.

## Key Features

1. Multi-Category Selection: Users can select from multiple categories (e.g., Science, Technology, History) for a customized quiz experience.
2. Customizable Difficulty Levels: The quiz offers beginner, intermediate, and advanced levels to suit different knowledge levels.
3. Dynamic Question Types: Includes multiple-choice, true/false, and fill-in-the-blank question types to add variety.
4. Random Question Ordering: Shuffles questions at the start of each quiz for a unique experience every time.
5. Real-Time Feedback: Provides immediate feedback on whether each answer is correct or incorrect.
6. Performance Tracking: Tracks scores and provides a final report of correct and incorrect answers.
7. Multi-Language Support: Developed in Python and C# to support multiple platforms.
8. Progress Saving: Allows users to save their quiz progress and resume later.

## Technologies Used

- **Python 3.x**: Used for implementing the quiz application with features like JSON handling, file operations, and user management.
- **.NET Core SDK (C#)**: Used for creating the equivalent quiz functionality with object-oriented principles and structured data handling.
- **JSON**: Used to store and manage quiz data, including user progress and questions.

## Project Structure

```
Multi-Language_Interactive_Knowledge_Quiz
│
├── quiz_app.py
│
├── QuizApp/
│   ├── Program.cs
│
└── questions.json
```

## How to Download and Install

1. **Download**:
   - Clone this repository using:
     ```bash
     git clone https://github.com/chimin14/QuizApp-Python-Java-CSharp.git
     ```

2. **Navigate to the Project Folder**:
   - Move into the project directory:
     ```Project
     ```

3. **Ensure You Have the Required Tools**:
   - Install Python 3.x and .NET Core SDK on your machine if they aren’t already installed.

## How to Run

1. **Run the Application**:
   - **Python**: Navigate to the Python folder and execute:
     ```bash
     python quiz_app.py
     ```
   - **C#**: Navigate to the C# folder and execute:
     ```bash
     dotnet run
     ```

2. **JSON Questions File**:
   - Ensure the `quizData.json` file is placed in the project root. This file contains the quiz questions, categories, and answers.

## JSON Structure

The `quizData.json` file should follow this structure to define quiz questions:

```json

   "Questions": [
        {
            "id": 1,
            "category": "Science",
            "difficulty": "beginner",
            "question": "What is the chemical symbol for water?",
            "choices": [
                "H2O",
                "O2",
                "H2",
                "HO"
            ],
            "answer": "A || H2O || h2o || H2o",
            "type": ""
        },
        {
            "id": 2,
            "category": "Science",
            "difficulty": "beginner",
            "question": "What planet is known as the Red Planet?",
            "choices": [
                "Mars",
                "Jupiter",
                "Earth",
                "Venus"
            ],
            "answer": "A || Mars || mars || MARS",
            "type": ""
        }
```

## User Interaction

### Starting the Quiz
Upon launching the quiz application, the user is prompted to log in or sign up.

**Example**:
```
Welcome to the Quiz App!
Do you want to (1) Log in or (2) Sign up?
```

### Answering Questions
Questions are presented in various formats, such as multiple-choice or true/false. Users can answer by selecting the corresponding option number or typing their answer directly.

**Example (Multiple Choice)**:
```
What is the powerhouse of the cell?
A. Mitochondria
B. Nucleus
C. Ribosome
D. Golgi apparatus
Enter your answer (A-D):
```

**Example (True or False)**:
```
True or False: A cat is a type of bird..
Enter your answer (True/False):
```

### Feedback and Scoring
After each answer, the application provides immediate feedback.

**Example**:
```
Correct! 
```

### Saving and Resuming Progress
Users can save their progress and resume later.

**Example**:
```
Would you like to save your progress? (yes/no):
```

### Ending the Quiz and Report
At the end of the quiz, a summary report is generated.

**Example**:
```
Quiz Completed!
Correct Answers: 3
Incorrect Answers: 2
```

## Function Details

### Python Implementation Highlights
- **load_questions_from_file**: Reads questions from `quizData.json` and validates their structure.
- **save_quiz_data**: Saves user data and progress to `quizData.json`.
- **main_menu**: Handles login and sign-up functionality.
- **login**: Authenticates users and shows quiz history or saved progress.
- **signup**: Creates new user accounts and initializes their data.
- **select_difficulty**: Prompts users to choose a difficulty level.
- **ask_questions**: Presents quiz questions, manages user responses, and tracks scores.
- **save_progress/resume_quiz**: Supports progress saving and resuming.

### C# Implementation Highlights
- **PreprocessJsonFile**: Prepares the JSON file by fixing potential format issues.
- **LoadQuestionsFromFile**: Reads and validates `quizData.json` data.
- **SaveQuizData**: Writes user data and quiz progress to `quizData.json`.
- **MainMenu**: Handles user login and sign-up.
  - **Login**: Authenticates users and retrieves their progress or history.
  - **Signup**: Creates new user accounts and initializes their data.
  - **SelectDifficulty**: Allows users to choose the quiz difficulty.
- **AskQuestions**: Presents questions, collects answers, and provides feedback.
- **SaveProgress/ResumeQuiz**: Saves and resumes user progress seamlessly.

## Future Enhancements

- **Timed Mode**: Introduce a timed mode to add a challenge.
- **Adaptive Difficulty**: Adjust question difficulty dynamically based on user performance.
- **New Question Types**: Include matching or ordering questions for variety.
- **Improved Reporting**: Enhance quiz summaries with detailed feedback and analytics.

