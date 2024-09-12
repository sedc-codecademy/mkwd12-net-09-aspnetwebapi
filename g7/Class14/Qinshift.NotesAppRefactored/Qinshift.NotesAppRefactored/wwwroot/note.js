let getAllBtn = document.getElementById("btn1");
let addNoteBtn = document.getElementById("btn3");
let logoutBtn = document.getElementById("logoutBtn");
let addNoteTextInput = document.getElementById("noteText1");
let addNotePrioInput = document.getElementById("notePriority1");
let addNoteTagInput = document.getElementById("noteTag1");
let addNoteUserIdInput = document.getElementById("noteUserId1");

let url = "https://localhost:7106/api/Notes";

let getAllNotes = async () => {
    //getting the token from localStorage
    let token = localStorage.getItem("notesApiToken");

    let response = await fetch("https://localhost:7106/api/Notes", {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    });

    if (response.ok) {
        let notes = await response.json();
        console.log(notes);
        displayNotes(notes)
    } else {
        console.log('Failed to fetch notes:', response.statusText)
    }
}

let addNote = async () => {

    //getting the token from localStorage
    let token = localStorage.getItem("notesApiToken");

    let note = {
        Text: addNoteTextInput.value,
        Priority: addNotePrioInput.value,
        Tag: addNoteTagInput.value,
        UserId: addNoteUserIdInput.value
    };

    let response = await fetch(url + "/addNote", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(note)
    })
        .then(function (response) {
            console.log(response);
        })
        .catch(function (err) {
            console.log(err);
        })
}

let displayNotes = (notes) => {

    //creating the elements
    let table = document.createElement("table");
    let thead = document.createElement("thead");
    let tbody = document.createElement("tbody");

    // Table headers
    let headers = ["Text", "Priority", "Tag", "UserId"]
    let headerRow = document.createElement("tr");
    headers.forEach(headerText => {
        let th = document.createElement("th");
        th.textContent = headerText;
        headerRow.appendChild(th);
    });
    thead.appendChild(headerRow);

    // Table rows
    notes.forEach(note => {
        let row = document.createElement("tr");
        Object.values(note).forEach(value => {
            let td = document.createElement("td");
            td.textContent = value;
            row.appendChild(td);
        });
        tbody.appendChild(row);
    });
    table.appendChild(thead);
    table.appendChild(tbody);

    let tableContainer = document.getElementById("tableContainer");
    tableContainer.innerHTML = "";
    tableContainer.appendChild(table);
}

let logout = () => {
    localStorage.removeItem("notesApiToken"); // this should remove the token from localstorage
    window.location.href = "https://localhost:7106/login.html" // after logout, this will redirect us to login page
}

logoutBtn.addEventListener("click", logout);
getAllBtn.addEventListener("click", getAllNotes);
addNoteBtn.addEventListener("click", addNote);