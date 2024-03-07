const uri = '/users';
let users = [];
const token = localStorage.getItem("token");
const Auth ="Bearer " + JSON.parse(token);
function getItems() {
    fetch(uri,
        {
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
     const item = {
         isDone: false,
         Name: addNameTextbox.value.trim()
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
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

//  function displayEditForm(id) {
//      const item = tasks.find(item => item.id === id);

//      document.getElementById('edit-name').value = item.name;
//     document.getElementById('edit-id').value = item.id;
//     document.getElementById('edit-isDone').checked = item.isDone;
//     document.getElementById('editForm').style.display = 'block';
// }


function deleteItem(id) {
    fetch(`${uri}/${id}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': Auth,
           }
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
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