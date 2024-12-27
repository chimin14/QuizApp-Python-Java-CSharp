import json
import os
from random import shuffle
from datetime import datetime
 
 
quiz_data_file = "quizData.json"
quiz_data = {"Users": {}, "Questions": []}
user_data = {}
questions = []
 
 
def load_questions_from_file():
    global quiz_data, user_data, questions
    if os.path.exists(quiz_data_file):
        try:
            with open(quiz_data_file, 'r') as file:
                quiz_data = json.load(file)
            print("Quiz data successfully loaded.")
        except Exception as ex:
            print(f"Error loading quiz data: {ex}")
            quiz_data = {"Users": {}, "Questions": []}
    else:
        print("Quiz data file not found. Please create 'quizData.json' with your questions.")
        quiz_data = {"Users": {}, "Questions": []}
 
    user_data = quiz_data["Users"]
    questions = quiz_data["Questions"]
 
 
def save_quiz_data():
    with open(quiz_data_file, 'w') as file:
        json.dump(quiz_data, file, indent=4)
 
 
def main_menu():
    print("Welcome to the Quiz App!")
    choice = input("Do you want to (1) Log in or (2) Sign up? ")
 
    if choice == "1":
        login()
    elif choice == "2":
        sign_up()
    else:
        print("Invalid choice. Please choose 1 or 2.")
        main_menu()
 
 
def login():
    username = input("Enter your username: ")
 
    if username in user_data:
        print(f"Welcome back, {username}! Here is your quiz history:")
        for history in user_data[username].get('History', []):
            print(f"Date: {history['Date']}, Difficulty: {history['Difficulty']}, Score: {history['Score']}")
 
        if user_data[username].get('CurrentProgress') is not None:
            choice = input("You have saved progress. Do you want to resume? (yes/no): ")
            if choice.lower() == "yes":
                resume_quiz(username)
                return
 
        select_difficulty(username)
    else:
        print("User not found. Please sign up.")
        main_menu()
 
 
def sign_up():
    username = input("Choose a username: ")
 
    if username in user_data:
        print("Username already exists. Please choose another.")
        sign_up()
    else:
        user_data[username] = {"History": []}
        save_quiz_data()
        print(f"Welcome, {username}! Your account has been created.")
        select_difficulty(username)
 
 
def select_difficulty(username):
    difficulty_choice = input("Choose difficulty: (1) Beginner, (2) Intermediate, or (3) Hard: ")
 
    if difficulty_choice == "1" or difficulty_choice == "2" or difficulty_choice == "3":
        selected_difficulty = "beginner" if difficulty_choice == "1" else "intermediate" if difficulty_choice == "2" else "hard"
        ask_questions(username, selected_difficulty)
    else:
        print("Invalid choice. Please choose 1, 2, or 3.")
        select_difficulty(username)
 
 
def ask_questions(username, difficulty, quiz_questions=None, progress=None):
    score = progress['CurrentScore'] if progress else 0
    total_correct_answers = progress['TotalCorrectAnswers'] if progress else 0
    total_incorrect_answers = progress['TotalIncorrectAnswers'] if progress else 0
    start_index = progress['CurrentQuestionIndex'] if progress else 0
 
    selected_questions = quiz_questions if quiz_questions else [q for q in questions if q['difficulty'] == difficulty]
    shuffle(selected_questions)
    selected_questions = selected_questions[:10]
 
    for i in range(start_index, len(selected_questions)):
        question = selected_questions[i]
        print(f"\nQuestion: {question['question']}")
 
        if question['type'] == "true_false":
            print("Answer with 'true' or 'false'.")
        else:
            for j, choice in enumerate(question['choices']):
                print(f"{chr(65 + j)}. {choice}")
 
        answer = input("\nYour answer (or type 'stop' to save and exit): ")
 
        if answer.strip().lower() == "stop":
            updated_progress = {
                "Difficulty": difficulty,
                "CurrentQuestionIndex": i,
                "CurrentScore": score,
                "TotalCorrectAnswers": total_correct_answers,
                "TotalIncorrectAnswers": total_incorrect_answers,
                "RemainingQuestionIds": [q['id'] for q in selected_questions[i:]]
            }
            save_progress(username, updated_progress)
            print("Progress saved. You can resume later.")
            return
 
        correct_answers = set([a.strip().upper() for a in question['answer'].split("||")])
        if answer.strip().upper() in correct_answers:
            print("Correct!")
            score += 1
            total_correct_answers += 1
        else:
            print(f"Wrong! The correct answer was {next(iter(correct_answers))}.")
            total_incorrect_answers += 1
 
    print(f"\nQuiz completed for {difficulty}! Your score: {score}/{len(selected_questions)}")
    print(f"Total correct answers: {total_correct_answers}")
    print(f"Total incorrect answers: {total_incorrect_answers}")
 
    if username not in user_data:
        user_data[username] = {}
 
    user_data[username]['History'].append({
        "Date": datetime.now().isoformat(),
        "Difficulty": difficulty,
        "Score": score
    })
 
    user_data[username]['TotalScore'] = user_data[username].get('TotalScore', 0) + score
    user_data[username]['TotalCorrectAnswers'] = user_data[username].get('TotalCorrectAnswers', 0) + total_correct_answers
    user_data[username]['TotalIncorrectAnswers'] = user_data[username].get('TotalIncorrectAnswers', 0) + total_incorrect_answers
 
    user_data[username]['CurrentProgress'] = None
    save_quiz_data()
 
    if difficulty != "hard":
        valid_choice = False
        while not valid_choice:
            response = input("Do you want to move to the next level? (yes/no): ").strip().lower()
            if response == "yes":
                next_difficulty = "intermediate" if difficulty == "beginner" else "hard"
                ask_questions(username, next_difficulty)
                valid_choice = True
            elif response == "no":
                print("Thank you for playing!")
                valid_choice = True
            else:
                print("Invalid response. Please answer with 'yes' or 'no'.")
 
 
def save_progress(username, progress):
    user_data[username]['CurrentProgress'] = progress
    save_quiz_data()
 
 
def resume_quiz(username):
    progress = user_data[username].get('CurrentProgress')
    if progress:
        remaining_questions = [q for q in questions if q['id'] in progress['RemainingQuestionIds']]
        ask_questions(username, progress['Difficulty'], remaining_questions, progress)
 
 
if __name__ == "__main__":
    load_questions_from_file()
    if not questions:
        print("No questions found in the JSON file. Please add questions to 'quizData.json' before running the quiz.")
    else:
        main_menu()
