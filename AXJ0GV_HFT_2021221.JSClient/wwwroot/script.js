let owners = [];
let connection;
let ownerUpdate;

getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:31666/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("OwnerCreated", (user, message) => {
        getdata();
        console.log(user);
        console.log(message);
    });

    connection.on("OwnerDeleted", (user, message) => {
        getdata();
        console.log(user);
        console.log(message);
    });

    connection.on("OwnerUpdated", (user, message) => {
        getdata();
        console.log(user);
        console.log(message);
    });


    connection.onclose
        (async () => {
            await start();
        });
    start();
}



async function getdata() {
    await fetch('http://localhost:18683/owner')
        .then(x => x.json())
        .then(list => {
            owners = Object.values(list);
            display();
        })
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

function display() {
    document.getElementById('table_body_content').innerHTML = null;
    let sex;
    owners[1].forEach(
        (owner) => {
            if (owner.sex == 0) {
                sex = "Male";
            }
            else {
                sex = "Female";
            }
            console.log(owner.name)
            document.getElementById('table_body_content').innerHTML +=
                "<tr><td>" + owner.id + "</td><td>" + owner.name + "</td ><td>" + owner.identityCardNumber + "</td><td>"
                + sex + "</td><td>" +
                `<button type="button" class="deletebtn" onclick="remove(${owner.id})">Delete</button>` +
                `<button type="button" class="editbtn" onclick="edit(${owner.id})">Modify</button>`
                + "</td ></tr >"
        }
    )
}

function create() {
    let name = document.getElementById('cinemaname').value
    fetch('http://localhost:18683/owner', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Name: name,
                Rooms: null
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata()
        })
        .catch((error) => {
            console.error('Error:', error);
        })
}

function remove(id) {
    if (confirm("Do you want to delete the selected owner?") == true) {
        fetch('http://localhost:18683/owner/' + id, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
            body: null
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getdata()
            })
            .catch((error) => {
                console.error('Error:', error);
            })
    }
    else {
        return;
    }
}

function edit(id) {
    document.getElementById('cinemanametoupdate').value = cinemas.find(cinema => cinema['id'] == id)['name']
    document.getElementById('updatecinemaformdiv').style.visibility = "visible"
    cinemaUpdate = id
}

function update() {
    document.getElementById('updatecinemaformdiv').style.visibility = "collapse"
    let name = document.getElementById('cinemanametoupdate').value

    fetch('http://localhost:18683/owner', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Id: cinemaUpdate,
                Name: name,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata()
        })
        .catch((error) => {
            console.error('Error:', error);
        })
}