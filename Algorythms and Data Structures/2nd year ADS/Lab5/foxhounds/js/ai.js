"use strict"
/* global board Board Hound Fox notify */

/* eslint-disable-next-line no-unused-vars */
function mainAILoop() {
    // This is the main loop for the AI functionality.
    setTimeout(mainAILoop, 100)

    if (board.gameover) {
        return
    }

    nextAIMove(false)
}

function nextAIMove(manual=true) {
    // If this function is called automatically,
    // it should only make the next move if the AI is enabled.
    // If this function is called manually, it should move regardless.

    if (board.currentTurn === "hound" && (manual || true)) {
        houndAI.makeMove()
    }
}

class AI {
    // Super class for the AI, implements the algorithms for both AI subclasses
    constructor() {
        this.name = ""
    }

    makeMove() {
        var depth = 6
        var baseAlpha = -1000
        var baseBeta = 1000
        var isAi = true

        this.evaluateMinimax(board, depth, baseAlpha, baseBeta, isAi)
    }

    terminalScore(boardState) {
        const victory = boardState.checkVictory(false)
        if (victory === "") {
            return 0
        }
        if (victory === this.name) {
            return 100
        } else {
            return -100
        }
    }

    cloneBoard(boardState) {
        const animals = []
        boardState.animals.forEach(animal => {
            if (animal.name === "fox") {
                animals.push(new Fox(animal.x, animal.y))
            } else if (animal.name === "hound") {
                animals.push(new Hound(animal.x, animal.y))
            }
        })
        return new Board(animals, boardState.currentTurn)
    }

    evaluateMinimax(currentBoard, depth, alpha, beta, root=false) {
        const animals = []
        currentBoard.animals.forEach(animal => {
            if (animal.name === currentBoard.currentTurn) {
                animals.push(animal)
            }
        })
        if (root) {
            let highestSoFar = -1000
            let bestAnimal = null
            let bestMove = null
            try {
                animals.forEach(animal => {
                    const moves = animal.possibleMoves(currentBoard)

                    moves.forEach(move => {
                        const newBoard = this.cloneBoard(currentBoard)

                        newBoard.animalAt(animal.x, animal.y)
                            .moveTo(move.x, move.y, newBoard)

                        const scores = this.evaluateMinimax(newBoard, depth - 1, alpha, beta)

                        // set new alpha OR beta
                        if (scores.beta < alpha) { // if hare makes things better for wolves
                            alpha = scores.beta
                            throw BreakException // prune this branch
                        } else if (scores.alpha > beta) {
                            beta = scores.alpha
                            throw BreakException
                        }
   
                        if (scores.lowest > highestSoFar) {
                            highestSoFar = scores.lowest
                            bestAnimal = animal
                            bestMove = move
                        }
                    })
                })
            }
            catch (e) {
                if (e !== BreakException) throw e;
            }
                
            bestAnimal.moveTo(bestMove.x, bestMove.y)
            return
        } else if (depth === 0 || this.terminalScore(currentBoard) !== 0) {
            var nalpha = 0
            var nbeta = 0
            var lowest = this.evaluateScore(currentBoard)
            
            if (currentBoard.currentTurn === "hound") { 
                nalpha = lowest
            } else if (currentBoard.currentTurn === "fox") {
                nbeta = lowest
            }
            
            return {
                lowest: lowest,
                all: [lowest],
                alpha: nalpha,
                beta: nbeta
            }
        } else {
            let lowestScore = 1000
            const allScores = []
            animals.forEach(animal => {
                const moves = animal.possibleMoves(currentBoard)
                moves.forEach(move => {
                    const newBoard = this.cloneBoard(currentBoard)

                    newBoard.animalAt(animal.x, animal.y)
                        .moveTo(move.x, move.y, newBoard)

                    const scores = this.evaluateMinimax(newBoard, depth - 1, alpha, beta)

                    if (scores.beta < alpha) { // if hare makes things better for wolves
                        alpha = scores.beta
                        throw BreakException // prune this branch
                    } else if (scores.alpha > beta) {
                        beta = scores.alpha
                        throw BreakException // prune this branch
                    }

                    if (scores.lowest < lowestScore) {
                        lowestScore = scores.lowest
                    }
                    allScores.push(...scores.all)
                })
            })
            return {
                alpha: alpha,
                beta: beta,
                lowest: lowestScore,
                all: allScores
            }
        }
    }
}

class FoxAI extends AI {
    constructor() {
        super()
        this.name = "fox"
    }
}

class HoundAI extends AI {
    constructor() {
        super()
        this.name = "hound"
    }

    evaluateScore(boardState) {
        let score = 0
        let fox = null
        const hounds = []
        boardState.animals.forEach(animal => {
            if (animal.name === "fox") {
                fox = animal
            } else {
                hounds.push(animal)
            }
        })
        score += fox.y * 3
        // Let the hounds keep roughly the same y axis
        let highestHound = -10
        let lowestHound = 20
        hounds.forEach(hound => {
            if (hound.y > highestHound) {
                highestHound = hound.y
            } else if (hound.y < lowestHound) {
                lowestHound = hound.y
            }
        })
        score -= (highestHound - lowestHound) * 2
        // Make sure the fox does not get above the hounds
        // As this means the game is lost
        if (fox.y <= lowestHound) {
            score -= 50
        }
        // Lower the score when the hounds are on average not above the fox
        let totalWidth = 0
        hounds.forEach(hound => {
            totalWidth += hound.x
        })
        const averageXPosition = totalWidth / hounds.length
        score -= Math.abs(averageXPosition - fox.x)
        // Make sure there are no gaps between the hounds
        // -1 and 8 are the "walls", where gaps are also discouraged
        const xPositions = []
        hounds.forEach(hound => {
            xPositions.push(hound.x)
        })
        xPositions.push(-1)
        xPositions.push(8)
        xPositions.sort((a, b) => {return a-b})
        let biggestGap = 0
        for (let i = 0; i < xPositions.length -1; i++) {
            const gap = xPositions[i+1] - xPositions[i]
            if (gap > biggestGap) {
                biggestGap = gap
            }
        }
        if (biggestGap > 4) {
            score -= 10
        }
        // Make losing a no go if possible
        // and winning top priority
        // console.log("Houds score: " + score)
        score += this.terminalScore(boardState)
        return score
    }
}

const foxAI = new FoxAI()
const houndAI = new HoundAI()
