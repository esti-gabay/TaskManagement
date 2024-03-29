const uri = '/Task';
let tasks = [];
const token = sessionStorage.getItem('auth');

function getItems() {
    fetch(uri, { headers: { 'Authorization': token } })
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    alert('Add Task', tasks.length);
    const item = {
        id: tasks.length + 104,
        name: addNameTextbox.value.trim(),
        taskAccomplished: false
    };
    alert(item.id);

    fetch(uri, {
        method: 'POST',
        headers: {
            'Authorization': token,
            'Accept': 'application/json',
            'Content-Type': 'application/json'
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

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE',
        headers: { 'Authorization': token }

    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = tasks.find(t => t.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-taskAccomplished').checked = item.taskAccomplished;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    alert('Update task');
    const item = {
        id: parseInt(itemId, 10),
        taskaccomplished: document.getElementById('edit-taskAccomplished').checked,
        name: document.getElementById('edit-name').value.trim()
    };

    fetch(`${uri}/${item.id}`, {
        method: 'PUT',
        headers: {
            'Authorization': token,
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'task' : 'task kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let TaskAccomplishedCheckbox = document.createElement('input');
        TaskAccomplishedCheckbox.type = 'checkbox';
        TaskAccomplishedCheckbox.disabled = true;
        TaskAccomplishedCheckbox.checked = item.taskAccomplished;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.className = 'button';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = '❌';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(TaskAccomplishedCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    tasks = data;
}