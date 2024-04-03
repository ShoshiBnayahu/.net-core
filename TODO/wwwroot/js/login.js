const uri = '/login';

function withoutLogin() {
    if (localStorage.getItem("token") != undefined && localStorage.getItem("token") != "" && sessionStorage.getItem("changeUser") == undefined)
        location.href = "./html/tasks.html";
}






function login() {
    const user = {
        name: document.getElementById('name').value.trim(),
        password: document.getElementById('password').value.trim()
    }
    console.log(user);
    fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        })
        .then(response => {
            if (response.status === 401) {
                user.name.value = "";
                user.password.value = "";
                alert("user not exist")
                throw "user not exist";
            }
            return response.text()
        })
        .then((result) => {
            console.log(result);
            localStorage.setItem("token", result)
            location.href = "./html/tasks.html";
        })
        .catch(error => console.error(error));
}
function onSuccess(response) {
    if (response.credential) {
        var idToken = response.credential;
        var decodedToken = parseJwt(idToken);
        var userPassword = decodedToken.sub;
        console.log(userPassword);
        var userName = decodedToken.name;
        console.log(userName);
        document.getElementById('name').value=userName;
        document.getElementById('password').value=userPassword;
        login();
    } else {
        alert('Google Sign-In failed.');
    }
}
function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}