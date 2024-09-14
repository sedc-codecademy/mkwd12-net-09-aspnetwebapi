let getAllBtn = document.getElementById("btn1");
let addNoteBtn = document.getElementById("btn3");
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
        //displayNotes(notes)
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

}

getAllBtn.addEventListener("click", getAllNotes);
addNoteBtn.addEventListener("click", addNote);