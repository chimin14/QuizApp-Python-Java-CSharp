const fs = require('fs');
const readline = require('readline');

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

let quizDataFile = 'quizData.json';

// Load combined quiz data
let quizData = fs.existsSync(quizDataFile) ? JSON.parse(fs.readFileSync(quizDataFile)) : { users: {}, questions: [] };
let userData = quizData.users;
let questions = quizData.questions;

if (!questions.length) {
    // Generate default questions by reading levels from JSON file
    questions = Array.from({ length: 100 }, (_, i) => { // Adjust length to 100 for all questions
        const difficultyLevels = ['beginner', 'intermediate', 'hard'];
        const difficulty = difficultyLevels[Math.floor(i / 33.33)]; // Evenly distribute difficulties

        return {
            id: i + 1,
            category: ['Science', 'Technology', 'History', 'Math'][Math.floor(i / 25)], // Adjust category distribution
            difficulty,
            question: `Question ${i + 1} - Sample`,
            type: 'multiple-choice',
            choices: ['A', 'B', 'C', 'D'],
            answer: 'A || H2O' // Example: Accept both letter and full answer text
        };
    });
    quizData.questions = questions;
    fs.writeFileSync(quizDataFile, JSON.stringify(quizData, null, 2));
}


function shuffleArray(array) {
    for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
}

function saveQuizData() {
    fs.writeFileSync(quizDataFile, JSON.stringify(quizData, null, 2));
}

function mainMenu() {
    console.log("Welcome to the Quiz App! Here are the rules:\n");
    console.log("1. Answer the questions by typing either the letter (A, B, C, D) or the full answer text.\n");
    console.log("2. You have 15 seconds to answer each question.\n");
    console.log("3. Your score will be recorded in your history.\n");

    rl.question('Do you want to (1) Log in or (2) Sign up? ', (choice) => {
        if (choice === '1') {
            login();
        } else if (choice === '2') {
            signUp();
        } else {
            console.log('Invalid choice. Please choose 1 or 2.');
            mainMenu();
        }
    });
}

function login() {
    rl.question('Enter your username: ', (username) => {
        if (userData[username]) {
            console.log(`Welcome back, ${username}! Here is your quiz history:`);
            console.log(userData[username].history);
            selectDifficulty(username);
        } else {
            console.log('User not found. Please sign up.');
            mainMenu();
        }
    });
}

function signUp() {
    rl.question('Choose a username: ', (username) => {
        if (userData[username]) {
            console.log('Username already exists. Please choose another.');
            signUp();
        } else {
            userData[username] = { history: [] };
            saveQuizData();
            console.log(`Welcome, ${username}! Your account has been created.`);
            selectDifficulty(username);
        }
    });
}

function selectDifficulty(username) {
    rl.question('Choose difficulty: (1) Beginner, (2) Intermediate, or (3) Hard: ', (difficultyChoice) => {
        if (['1', '2', '3'].includes(difficultyChoice)) {
            const selectedDifficulty = difficultyChoice === '1' ? 'beginner' : (difficultyChoice === '2' ? 'intermediate' : 'hard');
            startQuiz(username, selectedDifficulty);
        } else {
            console.log('Invalid choice. Please choose 1, 2, or 3.');
            selectDifficulty(username);
        }
    });
}


function startQuiz(username, difficulty) {
    let score = 0;
    let questionIndex = 0;
    let userHistory = userData[username].history;

    console.log(`\nStarting Quiz for ${username} (Difficulty: ${difficulty})...`);

    const filteredQuestions = shuffleArray(questions.filter(q => q.difficulty === difficulty)).slice(0, 10);

    function askQuestion() {
        if (questionIndex < filteredQuestions.length) {
            const question = filteredQuestions[questionIndex];
    
            console.log(`\nQuestion: ${question.question}`);
            console.log(`A. ${question.choices[0]}`);
            console.log(`B. ${question.choices[1]}`);
            console.log(`C. ${question.choices[2]}`);
            console.log(`D. ${question.choices[3]}`);
    
            let answerReceived = false;
    
            // Timeout to handle no answer scenario
            const timeout = setTimeout(() => {
                if (!answerReceived) {
                    console.log('\nTime is up!');
                    console.log(`Correct answer: ${question.answer.split('||')[0].trim()}`);
                    questionIndex++;
                    askQuestion();
                }
            }, 15000);
    
            rl.question('\nYour answer: ', (answer) => {
                if (!answerReceived) {
                    answerReceived = true;
                    clearTimeout(timeout);
    
                    const correctAnswers = question.answer.split('||').map(a => a.trim());
                    if (correctAnswers.includes(answer.trim()) || correctAnswers.includes(answer.toUpperCase())) {
                        console.log('Correct!');
                        score++;
                    } else {
                        console.log(`Wrong! The correct answer was ${correctAnswers[0]}.`);
                    }
                    questionIndex++;
                    askQuestion();
                }
            });
        } else {
            console.log(`\nQuiz completed! Your score: ${score}/${filteredQuestions.length}`);
            userHistory.push({ date: new Date().toISOString(), difficulty, score });
            userData[username].history = userHistory;
            saveQuizData();
            rl.close();
        }
    }
    
    askQuestion();
}

mainMenu();
