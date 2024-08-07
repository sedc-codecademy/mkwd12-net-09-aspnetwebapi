let getAllNotesBtn = document.getElementById("btn1");
let notesList = document.getElementById("notes");
let input = document.getElementById("noteValue");
let addNoteBtn = document.getElementById("btn2");

let port = "7013";
let getAllNotes = async () => {
    let url = "https://localhost:" + port + "/api/notes";

    let response = await fetch(url);
    console.log(response);

    let notes = await response.json();
    console.log(notes);

    for (let i = 0; i < notes.length; i++) {
        let li = document.createElement("li");
        li.innerText = notes[i];
        notesList.appendChild(li);
    }
}

let addNote = async () => {
    let url = "https://localhost:" + port + "/api/notes";

    let response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "text/plain"
        },
        body: input.value
    });

    let data = await response.text();
    console.log(data);

}

getAllNotesBtn.addEventListener("click", getAllNotes);
addNoteBtn.addEventListener("click", addNote);