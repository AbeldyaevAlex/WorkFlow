const apiUrl = 'https://localhost:44374/api/TasksApi';

document.getElementById('addForm').addEventListener('submit', async (e) => {
    e.preventDefault();

    const TaskName = document.getElementById('TaskName').value;
    const Brigade = document.getElementById('Brigade').value;
    const StatusId = document.getElementById('StatusId').value;

    const response = await fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ TaskName, Brigade, StatusId })
    });

    const data = await response.json();
    console.log(data);

    // Reload table data
    getUsers();
});

const getUsers = async () => {
    const response = await fetch(apiUrl);
    const data = await response.json();

    const usersData = document.getElementById('usersData');
    usersData.innerHTML = '';

    // Пример данных для выпадающих списков
    const brigades = ["Бригада1", "Бригада2", "Бригада3"];
    const statuses = ["Open", "Closed", "InProgress"];


    data.forEach(user => {
        const row = document.createElement('tr');
        row.innerHTML = `


      <td><input type="text" value="${user.TaskName}" data-index="${user.TaskName}" data-field="name" id="TaskName${user.TaskId}"></td>
      <td>
      <select data-index="${user.Brigade}" data-field="brigade" id="Brigade${user.TaskId}">
        ${brigades.map(brigade => `<option value="${brigade}" ${brigade === user.Brigade ? 'selected' : ''}>${brigade}</option>`).join('')}
      </select>
     </td>

     <td>
      <select data-index="${user.Status}" data-field="status" id="Status${user.TaskId}">
        ${statuses.map(status => `<option value="${status}" ${status === user.Status ? 'selected' : ''}>${status}</option>`).join('')}
      </select>
     </td>
      <td><input type="email" value="${user.FullCustomerName}" data-index="${user.FullCustomerName}" data-field="email" id="FullCustomerName${user.TaskId}"></td>
      <td><input type="email" value="${user.OpenDate}" data-index="${user.OpenDate}" data-field="email" id="OpenDate${user.TaskId}"></td>
      <td><input type="email" value="${user.ClosedDate}" data-index="${user.ClosedDate}" data-field="email" id="ClosedDate${user.TaskId}"></td>
      <td>
        <button onclick="editUser('${user.TaskId}')">SaveTask</button>
      </td>





    `;
        usersData.appendChild(row);
    });
};

const editUser = async (userId) => {
    // Implement edit user functionality
    const TaskName = document.getElementById(`TaskName${userId}`).value;
    const Brigade = document.getElementById(`Brigade${userId}`).value;
    const Status = document.getElementById(`Status${userId}`).value;
    const OpenDate = document.getElementById(`OpenDate${userId}`).value;

    fetch(`${apiUrl}/${userId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ TaskName, Brigade, Status, OpenDate })
    })
};

const deleteUser = async (userId) => {
    const response = await fetch(`${apiUrl}/${userId}`, {
        method: 'DELETE'
    });

    const data = await response.json();
    console.log(data);

    // Reload table data
    getUsers();
};

getUsers();






