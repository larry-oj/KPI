// interface elements
let pScoreCont = document.getElementById('p-score');
let bScoreCont = document.getElementById('b-score');
let tGeneral = document.getElementById('t-general');
let tFour = document.getElementById('t-four');
let tFh = document.getElementById('t-th');
let tStreet = document.getElementById('t-street');
let tSix = document.getElementById('t-6');
let tFive = document.getElementById('t-5');
let tFours = document.getElementById('t-4');
let tThree = document.getElementById('t-3');
let tTwo = document.getElementById('t-2');
let tOne = document.getElementById('t-1');
// let diceOne = document.getElementById('dice-1');
// let diceOneCb = document.getElementById('dice-1-cb');
// let diceTwo = document.getElementById('dice-2');
// let diceTwoCb = document.getElementById('dice-2-cb');
// let diceThree = document.getElementById('dice-3');
// let diceThreeCb = document.getElementById('dice-3-cb');
// let diceFour = document.getElementById('dice-4');
// let diceFourCb = document.getElementById('dice-4-cb');
// let diceFive = document.getElementById('dice-5');
// let diceFiveCb = document.getElementById('dice-5-cb');
let rollButton = document.getElementById('roll-btn');
let keepButton = document.getElementById('keep-btn');

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

class Dice {
    constructor(num) {
        this.dice = document.getElementById('dice-' + num);
        this.cb = document.getElementById('dice-' + num + '-cb');
        this.score = 1;
        this.number = num;
    }

    get diceNumber() {
        return this.number;
    }

    get diceScore() {
        return this.score;
    }

    setCB(value = true) {
        this.cb = value;
    }

    rollDice() {
        if (this.cb.checked == false) return;
        this.score = getRandomInt(1, 6);
    }
}

class State {
    constructor(dices) {
        let d = [];
        for(var i = 1; i <= 5; i++) {
            d.push(new Dice(i));
        }
        this.dices = d;
        this.rollNumber = 0;
    }

    get scores() {
        var combo = [];
        dices.forEach(dice => {
           combo.push(dice.score); 
        });
        return combo;
    }

    get value(isCombo = true) {
        let counts = [0, 0, 0, 0, 0, 0];
        for (var i = 0; i < 5; i++) {
            switch (this.dices[i].score) {
                case 1:
                    counts[0] += 1;
                    break;
                
                case 2:
                    counts[1] += 1;
                    break;

                case 3:
                    counts[2] += 1;
                    break;

                case 4:
                    counts[3] += 1;
                    break;

                case 5:
                    counts[4] += 1;
                    break;

                case 6:
                    counts[5] += 1;
                    break;
            }
        }

        var name = "";
        var value = 0;

        if (counts.includes(5)) {
            if (this.rollNumber == 1) {
                value = 100;
            } else { value = 60; }
            name = "general";
        }
        else if (counts.includes(4)) {
            if (this.rollNumber == 1) {
                value = 45;
            } else { value = 40; }
            name = "four";
        }
        else if (counts.includes(3) && counts.includes(2)) {
            if (this.rollNumber == 1) {
                value = 35;
            } else { value = 30; }
            name = "fh";
        }
        else if 
    }

    checkStreet(counts) {
        var count = {};
        counts.forEach(function(i) { count[i] = (count[i]||0) + 1;});
        console.log(counts);
        console.log(count);
    }

    roll() {
        if (this.rollNumber > 3) return;

        this.rollNumber++;
        if (this.rollNumber == 1) {
            this.dices.forEach(dice => {
                dice.setCB();
            });
        }

        this.dices.forEach(dice => {
            dice.rollDice();
        });
    }

    keep() {
        if (playerTurn) {

        }
    }



    updateBoard() {
        
    }
}


// variables
let pScore = 0;
let bScore = 0;
let state = new State();
let playerTurn = true;

