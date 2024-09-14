let regBtn = document.getElementById("regBtn");
let fn = document.getElementById("firstName");
let ln = document.getElementById("lastName");
let username = document.getElementById("username");
let pass = document.getElementById("pass");
let confPass = document.getElementById("confPass");

//we got our port from properties => launchsettings.json
let port = "7106";

let register = async () => {
    let url = "https://localhost:" + port + "/api/users/register";
    let user = {
        username: username.value,
        FirstName: fn.value,
        LastName: ln.value,
        Password: pass.value,
        ConfirmedPassword: confPass.value
    };

    let reposnse = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(function (response) {
            console.log(response);
            window.location.href = "https://localhost:7106/login.html"
        })
        .catch(function (error) {
            console.log(error);
        });
}

regBtn.addEventListener("click", register);