const uri = '/users';
let users = [];
const token = localStorage.getItem("token");
const Auth = "Bearer " + JSON.parse(token);

function getItems() {
    fetch(uri, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': Auth,

            }
        })
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}


function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addPasswordTextbox = document.getElementById('add-password');
    const addIsAdminCheckbox = document.getElementById('add-isAdmin');
    const item = {
        isAdmin: addIsAdminCheckbox.checked,
        name: addNameTextbox.value.trim(),
        password: addPasswordTextbox.value.trim()
    };
    fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': Auth,

            },
            body: JSON.stringify(item)
        })
        .then(response => {
            jumpToLogin(response.status)
            return response.json()
        })
        .then(() => {
            getItems();
            addNameTextbox.value = '';
            addPasswordTextbox.value = '';
            addIsAdminCheckbox.checked = false;

        })
        .catch(error => console.error('Unable to add item.', error));
}



function deleteItem(id) {
    fetch(`${uri}/${id}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': Auth,
            }
        })
        .then(response => {
            if (!jumpToLogin(response.status))
                getItems();
        }).catch(error => console.error('Unable to delete item.', error));
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'user' : 'users ';
    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}



function _displayItems(data) {
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isAdminCheckbox = document.createElement('input');
        isAdminCheckbox.type = 'checkbox';
        isAdminCheckbox.disabled = true;
        isAdminCheckbox.checked = item.isAdmin;

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isAdminCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(deleteButton);

    });
    users = data;
}

function jumpToLogin(status) {
    if (status === 401) {
        alert("your token got expired,please login ")
        localStorage.setItem('token', "");
        location.href = "../index.html";
        return true;
    }
    return false;
}