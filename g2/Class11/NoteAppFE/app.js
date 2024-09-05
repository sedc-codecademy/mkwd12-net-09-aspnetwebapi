let ul = document.getElementById("list");
 const apiUrl = 'https://localhost:7033/api/Notes';
const token= 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InN0cmluZyIsInVzZXJGdWxsTmFtZSI6InN0cmluZyBzdHJpbmciLCJuYmYiOjE3MjU1NTE4NTgsImV4cCI6MTcyNTYzODI1OCwiaWF0IjoxNzI1NTUxODU4fQ.ZS0jwPQEnbMRXtLozuCchHgv7gmx_ifi4wtUHn38MpI'
 fetch(apiUrl, {
    method: 'GET',
    headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
    }
})
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(data => {
        console.log('Data fetched successfully:', data);
        data.forEach(x => {
            let li = document.createElement("li");
            li.textContent = x.text;
            ul.appendChild(li);
        });
    })
    .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
    });