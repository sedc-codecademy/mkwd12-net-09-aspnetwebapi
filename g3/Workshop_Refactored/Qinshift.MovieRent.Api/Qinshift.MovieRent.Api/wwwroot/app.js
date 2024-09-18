
fetch('https://localhost:7156/api/user/login', {
    method: 'POST',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    },
    body: JSON.stringify({ userName: 'pano', password: 'Test123#' })
})
    .then(res => res.json())
    .then(user => {
        console.log(user);
        localStorage.setItem("user", JSON.stringify({ token: user.token, fullName: user.fullName }));
    })


//const headers = { 'Authorization': 'Bearer ' }


fetch('https://localhost:7156/api/movies/all', {
    headers: {
        'Authorization': 'Bearer ' + JSON.parse(localStorage.getItem('user')).token
    }
})
    .then(res => res.json())
    .then(movies => {
        console.log(movies);
    })
    .catch(err => {
        console.log(err);
    });

fetch('https://localhost:7156/api/movies/1', {
    headers: {
        'Authorization': 'Bearer ' + JSON.parse(localStorage.getItem('user')).token
    }
})
    .then(res => res.json())
    .then(movies => {
        console.log(movies);
    })
    .catch(err => {
        console.log(err);
    });