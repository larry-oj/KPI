// Include SVG
let svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
let svgNS = svg.namespaceURI;

// ========== Parameters ========== //
const canvasWidth = 1000;
const canvasHeight = 500;

let padding = 20; // padding of... yeah

let innerWidth = 8; // line width

const red = '#FF4500';
const blue = '#1E90FF';
const black = '#000000';
const white = '#FFFFFF';
let colors = [];

let numRed = 0; // individual scores
let numBlue = 0; // individual scores

let turn = true; // true -> blue | false -> red
let winner = false; // true -> game is over

let difficulty = 3;
// ========== Parameters ========== //

// ============= Data ============= //
let allLines = [];
// ============= Data ============= //


// set canvas size
svg.setAttribute('width', canvasWidth + padding * 2)
svg.setAttribute('height', canvasHeight + padding * 2)

// create ground line
createLine(padding, canvasHeight, canvasWidth, canvasHeight, 'base', black, false);

// creates line
function createLine(x1, y1, x2, y2, classId, color, remove = true) {
    var line = document.createElementNS(svgNS, 'line'); // create line object

    line.setAttribute('class', classId);        // set class
    line.setAttribute('trueColor', color);  // set color

    // if removeable
    if (remove) {
        line.onclick = removeLine;
    }

    // set coordinates
    line.setAttribute('x1', x1);
    line.setAttribute('x2', x2);
    line.setAttribute('y1', y1);
    line.setAttribute('y2', y2);

    // set colors and outline
    line.setAttribute('stroke', color);
    line.setAttribute('stroke-width', innerWidth);
    line.setAttribute('stroke-linecap', 'round'); // also make fancy

    svg.appendChild(line);

    // add to canvas
    document.body.appendChild(svg)

    // add line to the list of lines
    if (classId == 'base') return;

    // add line to lines array
    var newLine = {
        x1: x1,
        y1: y1,
        x2: x2,
        y2: y2,
        classId: classId,
        color: color
    };
    allLines.push(newLine);
}

// removes line
function removeLine(e, lines = allLines) {
    var targetLine = e.target;
    trueColor = targetLine.getAttribute('trueColor');

    // check turn
    if (trueColor == red && turn != false) return;
    if (trueColor == blue && turn != true) return;

    classId = targetLine.getAttribute('class');
    
    // get line that was clicked on
    var line = {};
    for (var i = 0; i < lines.length; i++)
    {
        var item = lines[i];

        if (item.classId == classId) {
            line = item;
            break;
        }
    }

    // contains all children + line itself
    var linesToDelete = getAllLinesToDelete(line);
    
    // remove lines form canvas and array
    linesToDelete.forEach(item => {
        removeSvg(item.classId);
        lines.splice(lines.indexOf(item), 1);
    });

    // change turn
    if (turn == true) {
        turn = false;
    }
    else {
        turn = true;
    }

    countScore();
    updateCounter();
}

// helper method, removes line from canvas
function removeSvg(classId) {
    var lineToDelete = svg.querySelectorAll("[class='" + classId + "']")[0];
    svg.removeChild(lineToDelete);
}

// finds all lines, that will be deleted after line removal
function getAllLinesToDelete(line, lineList = []) {
    lineList.push(line); // all line to list

    var children = [];
    var condition = false; // determines if there are any children at all

    for (var i = 0; i < allLines.length; i++) {
        var item = allLines[i];

        // if this is a child
        if (item.x1 != line.x2) continue;
        if (item.y1 != line.y2) continue;

        // check that its not the parent itself
        if (item.classId != line.classId) {
            children.push(item);
            condition = true;
        }
    };

    if (!condition) {
        return lineList;
    }
    else {
        children.forEach(child => {
            lineList = getAllLinesToDelete(child, lineList);
        });
        return lineList;
    }
}

// draws boards (presets)
function drawBoard(num) {
    switch (num) {
        default:
        case 1:
            createLine(575, 500, 600, 400, '0', blue);
            createLine(575, 500, 625, 425, '1', blue);
            createLine(650, 500, 650, 275, '2', blue);
            createLine(650, 275, 575, 200, '3', blue);
            createLine(675, 125, 625, 50, '4', blue);
            createLine(675, 125, 725, 75, '5', blue);
            createLine(375, 400, 425, 350, '6', blue);
            createLine(425, 150, 350, 75, '7', blue);
            createLine(425, 150, 475, 100, '8', blue);
            createLine(150, 225, 50, 125, '9', blue);
            createLine(150, 225, 150, 125, '10', blue);
            createLine(150, 225, 250, 125, '11', blue);
            createLine(800, 500, 800, 400, '12', blue);
            createLine(800, 400, 750, 350, '13', blue);
            createLine(800, 400, 850, 325, '14', blue);
            createLine(850, 325, 850, 225, '15', blue);
            createLine(750, 250, 800, 200, '16', blue);
            createLine(850, 150, 900, 100, '17', blue);
            createLine(250, 500, 250, 325, '18', red);
            createLine(250, 325, 150, 225, '19', red);
            createLine(250, 325, 350, 225, '20', red);
            createLine(375, 500, 375, 400, '21', red);
            createLine(425, 350, 450, 275, '22', red);
            createLine(450, 275, 425, 150, '23', red);
            createLine(575, 500, 525, 425, '24', red);
            createLine(575, 500, 550, 400, '25', red);
            createLine(650, 275, 675, 200, '26', red);
            createLine(675, 200, 675, 125, '27', red);
            createLine(675, 200, 725, 150, '28', red);
            createLine(800, 400, 725, 375, '29', red);
            createLine(800, 400, 900, 350, '30', red);
            createLine(750, 350, 750, 250, '31', red);
            createLine(800, 200, 800, 150, '32', red);
            createLine(850, 225, 850, 150, '33', red);
            createLine(850, 150, 800, 100, '34', red);
            break;
    
        case 2:
            createLine(500, 500, 525, 400, '0', red);
            createLine(525, 400, 500, 300, '1', red);
            createLine(500, 300, 525, 200, '2', red);
            createLine(525, 200, 500, 100, '3', red);
            createLine(500, 300, 600, 225, '4', red);
            createLine(600, 225, 600, 150, '5', red);
            createLine(600, 225, 650, 150, '6', red);
            createLine(600, 225, 675, 225, '7', red);
            createLine(525, 400, 425, 325, '8', blue);
            createLine(425, 325, 375, 250, '9', blue);
            createLine(375, 250, 325, 200, '10', blue);
            createLine(375, 250, 400, 175, '11', red);
            createLine(325, 200, 325, 150, '12', blue);
            createLine(325, 200, 300, 150, '13', blue);
            createLine(325, 200, 275, 175, '14', blue);
            createLine(400, 500, 400, 375, '15', blue);
            createLine(600, 500, 600, 375, '16', blue);
            createLine(525, 200, 425, 125, '17', blue);
            break;
    }

    countScore();
    updateCounter();
}

// counts score and sets it to variables
function countScore(lines = allLines) {
    var blues = 0;
    var reds = 0;
    lines.forEach(item => {
        if (item.color == blue) {
            blues++;
        }
        else if (item.color == red) {
            reds++;
        }
    });

    numBlue = blues;
    numRed = reds;
}

// calculates evaluation of a given state
function evaluation(lines) {
    var blues = 0;
    var reds = 0;
    lines.forEach(item => {
        if (item.color == blue) {
            blues++;
        }
        else if (item.color == red) {
            reds++;
        }
    });

    return blues - reds;
}

// updates the counter && checks for a winner
function updateCounter() {
    var counter = document.getElementById("counter");
    var winner = document.getElementById("winner");

    if (numBlue == 0 && numRed != 0) {
        counter.innerHTML = "";
        winner.innerHTML = "Red wins!";
        winner = true;
        return;
    }
    else if (numBlue != 0 && numRed == 0) {
        counter.innerHTML = "";
        winner.innerHTML = "Blue wins!";
        winner = true;
        return;
    }
    else if (numBlue == 0 && numRed == 0) {
        counter.innerHTML = "";
        winner.innerHTML = "Blue wins!";
        winner = true;
        return;
    }

    counter.innerHTML = numBlue - numRed;
}

// main loop
function gameLoop() {
    if (winner) return; // if winner is defined, stop the loop
    
    // update scores
    countScore();
    updateCounter();

    setTimeout(gameLoop, 100); // run loop every 0.1s bot

    if (turn) return; // if not bots turn

    aiAdvance(); // run AI
}

// alpha & beta, used as variables to be easily referenced in recursion
let alpha = -100;
let beta = 100;

// main AI function, runs every AI's turn
function aiAdvance() {
    // reset alpha / beta
    alpha = -100;
    beta = 100;

    // use current canvas state as initial
    var initialState = {
        state: [...allLines],
        deletedLine: allLines[allLines.length - 1],
        eval: evaluation(allLines)
    };


    // set the difficulty
    if (document.getElementById("easy").checked) {
        difficulty = 1;
    }
    else if (document.getElementById("medium").checked) {
        difficulty = 3;
    }
    else {
        difficulty = 5;
    }

    // start minimax
    var move = minimax(initialState, difficulty, red);
    var deletedLine = move.deletedLine; // line that was deleted

    // if the AI resigned
    if (deletedLine == undefined || Object.keys(deletedLine).length === 0) {
        var counter = document.getElementById("counter");
        var bot = document.getElementById("bot")

        counter.innerHTML = "";
        bot.innerHTML = "Bot resigned! "

        winner = true;

        countScore();
        updateCounter();
        counter.innerHTML = "";
        return;
    }

    // create fake svg object
    var lineToDelete = document.createElementNS(svgNS, 'line'); // create line object
    lineToDelete.setAttribute('class', deletedLine.classId); // set class
    lineToDelete.setAttribute('trueColor', deletedLine.color);  // set color

    // delete a line from canvas and state
    removeLine({ target: lineToDelete });
}

// alpha-beta pruning 
function minimax(state, depth, color) {
    if (depth == 0 || state.state.length <= depth) return {
        state: state.state,
        deletedLine: state.deletedLine,
        eval: evaluation(state.state)
    };

    if (color == blue) {
        var maxEval = -100;
        var bestState = {};
        var childStates = getPossibleDeletions(state.state, color);
        for (var i = 0; i < childStates.length; i++) {
            var minimaxResult = minimax(childStates[i], depth - 1, color == blue ? red : blue);
            var eval = minimaxResult.eval;
            if (maxEval < eval) {
                maxEval = eval;
                bestState = minimaxResult;
            }
            alpha = Math.max(alpha, eval);
            if (beta <= alpha) break;
        }
        return {
            state: bestState.state,
            deletedLine: bestState.deletedLine,
            eval: maxEval
        }
    }
    else {
        var minEval = 100;
        var bestState = {};
        var childStates = getPossibleDeletions(state.state, color);
        for (var i = 0; i < childStates.length; i++) {
            var minimaxResult = minimax(childStates[i], depth - 1, color == blue ? red : blue);
            var eval = minimaxResult.eval;
            if (minEval > eval) {
                minEval = eval;
                bestState = minimaxResult;
            }
            beta = Math.min(beta, eval);
            if (beta <= alpha) break;
        }
        return {
            state: bestState.state,
            deletedLine: bestState.deletedLine,
            eval: minEval
        }
    }
}

// used for child states generation
function getPossibleDeletions(lines = allLines, color) {
    var coloredLines = [];
    var possibleDeletions = [];

    // get all lines of some color
    lines.forEach(line => {
        if (line.color === color) {
            coloredLines.push(line);
        }
    });

    // create all possible child states
    for (var i = 0; i < coloredLines.length; i++) {
        var copy = [...coloredLines];

        var temp = copy[i]; // take a line

        copy.splice(i, 1); // remove a line from copy

        // find deleted line in initial state
        var deletedLine = {};
        lines.forEach(line => {
            if (line.color == color && !copy.includes(line)) {
                deletedLine = line;
            }
        });

        // copy initial state and remove the line in here
        copy = [...lines];
        copy.splice(copy.indexOf(temp), 1);

        // create new state and add to list
        possibleDeletions.push({ 
            state: copy, 
            deletedLine: deletedLine, 
            eval: countScore(copy) 
        });
    }

    return possibleDeletions;
}


// run the game
drawBoard(1);
gameLoop();