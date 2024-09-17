import { LoginRequest, PriorityEnum, TagEnum } from "./models.js"; // in order for this to work you need to start the web page with Live Server

// DEFINING VARIABLES

const BASE_URL = "https://localhost:7033/api"; // NOTE: Update the url respectively

const loginMenu = document.getElementById("loginMenu");
const usernameInput = document.getElementById("username");
const passwordInput = document.getElementById("password");
const usernameError = document.getElementById("usernameError");
const passwordError = document.getElementById("passwordError");
const loginError = document.getElementById("loginError");
const loginBtn = document.getElementById("loginBtn");

const notesMenu = document.getElementById("notesMenu");
const getNotesBtn = document.getElementById("getNotesBtn");

let btnHoverToggle = true;

// DEFINING EVENTS

usernameInput.addEventListener("input", () => {
    usernameError.textContent = "";
    loginError.textContent = "";
})

passwordInput.addEventListener("input", () => {
    passwordError.textContent = "";
    loginError.textContent = "";
})

loginBtn.addEventListener("mouseover", validateCredentials)
loginBtn.addEventListener("click", loginUser);
getNotesBtn.addEventListener("click", getNotes);

// DEFINING FUNCTIONS

function validateCredentials(e) {
    e.preventDefault();
    const username = usernameInput.value;
    const password = passwordInput.value;

    usernameError.textContent = "";
    passwordError.textContent = "";

    if (!username) {
        usernameError.textContent = "Please enter a username.";
    }

    if (!password) {
        passwordError.textContent = "Please enter a password.";
    }

    if (!username || !password) {
        if (btnHoverToggle) {
            loginBtn.classList.remove("move-left");
            loginBtn.classList.add("move-right");
            btnHoverToggle = false;
        }
        else {
            loginBtn.classList.remove("move-right");
            loginBtn.classList.add("move-left");
            btnHoverToggle = true;
        }
    }
    else {
        loginBtn.classList.remove("move-left");
        loginBtn.classList.remove("move-right");
    }
}

async function loginUser(e) {

}

function showNotesMenu() {
    loginMenu.setAttribute("hidden", "hidden");
    notesMenu.removeAttribute("hidden");
}

async function getNotes(e) {

}

function renderNotes(notes) {
    const notesContainer = document.getElementById("notesContainer");
    notesContainer.innerHTML = "";
    if (!notes?.length) {
        notesContainer.innerHTML = `<div class="text-danger text-center">No notes available !</div>`;
        return;
    }
    for (const note of notes) {
        notesContainer.insertAdjacentHTML("beforeend", createNoteCardHtml(note))
    }
}

function createNoteCardHtml(note) {
    return `
        <div class="col-md-4">
            <div class="card mb-3">
                <div class="card-body">
                    <p>Text: ${note.text}</p>
                    <p>Priority: ${PriorityEnum[note.priority]}</p>
                    <p>Tag: ${TagEnum[note.tag]}</p>
                    <p>User: ${note.userFullName}</p>
                </div>
            </div>
        </div>
    `;
}
