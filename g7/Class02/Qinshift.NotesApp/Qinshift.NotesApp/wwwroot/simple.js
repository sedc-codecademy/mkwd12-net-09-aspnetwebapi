let getAllBtn = document.getElementById("btn1");
console.log(getAllBtn);
let getByIdBtn = document.getElementById("btn2");
let addNoteBtn = document.getElementById("btn3");
let getByIdInput = document.getElementById("input2");
let addNoteInput = document.getElementById("input3");

//you need to change this port to your own that it located in the launch settings
let port = "7089";

let getAllNotes = async () => {
    let url = "https://localhost:" + port + "/api/Notes";
    console.log(port);
    let response = await fetch(url);
    console.log(response);
    debugger;
    let data = await response.json();
    console.log(data);
}

let getNoteById = async () => {
    let url = "https://localhost:" + port + "/api/Notes/" + getByIdInput.value;
    debugger;
    let response = await fetch(url);
    let data = await response.text();
    console.log(data);
}

let addNote = async () => {
    let url = "https://localhost:" + port + "/api/Notes";

    let response = await fetch(url, {
        method: 'Post',
        headers: {
            'Content-Type': 'text/plain'
        },
        body: addNoteInput.value
    });
    let data = await response.text();
    console.log(data);
}

getAllBtn.addEventListener("click", getAllNotes);
getByIdBtn.addEventListener("click", getNoteById);
addNoteBtn.addEventListener("click", addNote);

