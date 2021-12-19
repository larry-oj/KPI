//#region interface elements
const turnSpan = document.getElementById('turn');
const playerScoreSpan = document.getElementById('player-score');
const botScoreSpan = document.getElementById('bot-score');
const currentGambleSpan = document.getElementById('currnet-gamble');
const currentScoreSpan = document.getElementById('current-score');
const rollDiceBtn = document.getElementById('roll-dice');
const keepScoreBtn = document.getElementById('keep-score');
const infoSpan = document.getElementById('info');

let dice = document.getElementById('dice');
// #endregion


//#region state class
class State {
    constructor(botScore, playerScore) {
        this.bScore = botScore;
        this.pScore = playerScore;
    }

    get eval() { return this.calcEval(); }


    get choice() { return this.isSuitable(); }

    get children() {
        var cList = [];
        for (var i = 1; i <= 6; i++) {
            if (botTurn) {
                cList.push(new State(i == 1 ? 0 : (this.bScore + i), this.pScore));
            }
            else {
                cList.push(new State(this.bScore, i == 1 ? 0 : (this.pScore + i)));
            }
        }
        return cList;
    }


    isSuitable() {
        var choice = currentGamble - 15 < 0 ? (t = true) => { return t; } : (f = false) => { return f; }
        // var choice = currentGamble - 15 < 0 ? () => { return false; } : () => { return true; }
        return choice();
    }

    calcEval() {
        return this.bScore - 15;
    }

    isWinState() {
        if (this.pScore >= 100 || this.bScore >= 100) return true;
        return false;
    }
}
//#endregion


//#region data and settings
const goal = 100;           // Max number of rounds
let botTurn = false;        // false -> player | true -> ai
let botScore = 0;
let playerScore = 0;
let currentScore = 0;       // Current score earned by rolling
let currentGamble = 0;      // Side tshat was dropped by rolling a dice

function getCurrentState() {
    return new State(botScore, playerScore);
}
// #endregion


//#region miscallenous
function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

let tmp = 1;
function diceAnimation(number) {
	dice.classList.remove('show-' + tmp);

	tmp = number;

	dice.classList.add('show-' + tmp);
}

function sleep(ms) {
	return new Promise(resolve => setTimeout(resolve, ms));
}
// #endregion


//#region preparation
function updateStats() {
    turnSpan.innerText = botTurn == false ? "Player" : "Bot";
    playerScoreSpan.innerText = playerScore;
    botScoreSpan.innerText = botScore;
    currentScoreSpan.innerText = currentScore;
    currentGambleSpan.innerText = currentGamble;
}
updateStats();
rollDiceBtn.onclick = () => { rollDiceAction() };
keepScoreBtn.onclick = () => { keepScoreAction() };
// #endregion


//#region board actions
async function rollDice() { 
	var rnd = getRandomInt(1, 6);
	diceAnimation(rnd);
    await sleep(1000);
	currentScore = rnd;
}

function changeTurn() {
	botTurn = !botTurn;
}

async function rollDiceAction(isBot = false) {
    if (botTurn && !isBot) return;

	await rollDice();

    currentScoreSpan.innerText = currentScore;

    if (currentScore == 1) {
        infoSpan.innerText = turnSpan.innerText + " rolled 1!";

        currentGamble = 0;

        changeTurn();
    }
    else {
        currentGamble += currentScore;
    }

    updateStats();
}

function keepScoreAction(isBot = false) {
    if (botTurn && !isBot) return;

    infoSpan.innerText = turnSpan.innerText + " is keeping!";

    if (botTurn) { botScore += currentGamble; }
    else { playerScore += currentGamble; }

    currentGamble = 0;

	changeTurn();
    
    updateStats();
}
//#endregion


async function aiLoop() {
    while (true) {
        await sleep(100);
        if (!botTurn) continue;

        var res = minimax(getCurrentState(), 3);

        if (res.isSuitable()) {
            await rollDiceAction(true);
        }
        else {
            keepScoreAction(true);
        }
    }
}
aiLoop();


function minimax(state, depth) {
    if (depth == 0) {
        return state;
    }

    if (botTurn) {
        var maxEval = -100;
        var betterState = {};
        state.children.forEach(child => {
            var nextState = minimax(child, depth - 1);
            if (maxEval < nextState.eval) {
                maxEval = nextState.eval;
                betterState = nextState;
            }
        });
        return betterState;
    }
    else {
        var minEval = 100;
        var betterState = {};
        state.children.forEach(child => {
            var nextState = minimax(child, depth - 1);
            if (minEval > nextState.eval) {
                minEval = nextState.eval;
                betterState = nextState;
            }
        });
        return betterState;
    }
}