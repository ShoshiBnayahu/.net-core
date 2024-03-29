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