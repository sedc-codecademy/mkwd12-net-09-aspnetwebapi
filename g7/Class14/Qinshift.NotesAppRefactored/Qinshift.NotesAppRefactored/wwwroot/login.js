let usernameInput = document.getElementById("username");
let passInput = document.getElementById("pass");
let loginBtn = document.getElementById("loginBtn");

let login = async () => {

    //first we need to get the values from the input
    let user = {
        Username: usernameInput.value,
        Password: passInput.value
    };

    //afterwards we create the post with FETCH
    //for the appropriate ednpoint (url)
    //then we store the token that the endpoint created in the browser's local storage
    let response = await fetch("https://localhost:7106/api/users/login", {
        //we set bellow what kind of method this function will trigger (GET/POST/PUT/DELETE)
        method: 'POST',
        //we set the headers here and put only what type of content will be
        headers: {
            'Content-type': 'application/json'
        },
        //here we set the value in the body that will be send
        //and for that purpose we stringify the login model
        //or we convert the values from the input into JSON
        body: JSON.stringify(user)
    })
    //here we make the response that we get from the endpoint
        .then(function (response) {
            console.log(response);
            response.text()
                .then(function (text) {
                    //here we save the token into our local storage in the browser
                    localStorage.setItem("notesApiToken", text);
                    debugger
                    //after everything is finished we are redirected to the notes view
                    window.location.href = "https://localhost:7106/note.html"
                })
        })
        //here we are catching the error if it happens and logging it into the console
        .catch(function (err) {
            console.log(err)
        })
}



//we are adding the event listener to the button we created so when pressed with click
//it will call the login function
loginBtn.addEventListener("click", login);