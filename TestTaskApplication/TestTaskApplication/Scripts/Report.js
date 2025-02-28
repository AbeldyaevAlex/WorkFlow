const apiUrl = 'https://localhost:44374/api/Report';

document.getElementById('addForm').addEventListener('submit', async (e) => {
    e.preventDefault();

    const OpenDate = document.getElementById('OpenDate').value;
    const ClosedDate = document.getElementById('ClosedDate').value;


    const response = await fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ OpenDate, ClosedDate})
    });

    const data = await response.json();
    console.log(data);

    // Reload table data
    getUsers();
});

const getUsers = async () => {
    const response = await fetch(apiUrl);
    const data = await response.json();

    const usersData = document.getElementById('ReportData');
    usersData.innerHTML = '';

    data.forEach(user => {
        const row = document.createElement('tr');
        row.innerHTML = `
      <td>${user.Role}</td>
      <td>${user.CountOrder}</td>

    `;
        usersData.appendChild(row);
    });
};

const editUser = async (userId) => {
    // Implement edit user functionality
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