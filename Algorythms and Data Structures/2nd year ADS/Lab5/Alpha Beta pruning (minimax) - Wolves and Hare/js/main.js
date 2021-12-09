"use strict"
/* global board generateNewBoard updateBoard mainAILoop nextAIMove */

// Stores if the international or "dutch" tiles should be used
// Hare and Hounds is the default, but Wolf and Sheep is also possible
// Names on the screen and in the messages will be updated as well
let useInternationalTiles = true

function startup() {
    // This function is called when the page is first loaded
    // It generates a fresh board, and binds the listeners to the buttons
    generateNewBoard()
    setupListeners()
    // Lastly the AI main loop is called, which will keep running from there
    mainAILoop()
}

function setupListeners() {
    document.getElementById("controls-reset-scores").onclick = () => {
        document.getElementById("win-hare").innerText = "0"
        document.getElementById("win-wolf").innerText = "0"
        generateNewBoard()
    }
}

/* eslint-disable-next-line no-unused-vars */
function notify(level, message, duration=3000) {
    // Shows a notification in green (info)
    const body = `<div class="notification notify-${level}">${message}</div>`
    const notifications = document.getElementById("notifications")
    notifications.innerHTML += body
    setTimeout(() => {
        notifications.removeChild(notifications.childNodes[0])
    }, duration)
}

function title(string) {
    return string[0].toUpperCase()  + string.slice(1)
}

function nationalName(name, pretty=false) {
    
    if (pretty) {
        if (useInternationalTiles) 
        {
            let prettyName = title(name)

            if (prettyName == "wolf") 
            {
                prettyName = "s"
            }
            return prettyName
        }
    } 
    else if (useInternationalTiles) {
        return name
    }
}

window.onload = startup
