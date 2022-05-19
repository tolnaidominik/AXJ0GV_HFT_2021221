let owners = [];
let ownerOrderByICN = [];
let injectionOrderByPrice = [];
let injectionSumPrice;
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
    await fetch('http://localhost:18683/stat/OrderByPrice')
        .then(x => x.json())
        .then(result => {
            injectionOrderByPrice = Object.values(result);
            displayNonCrudOrderByPrice();
        })
    await fetch('http://localhost:18683/stat/OrderByIdentityCardNumber')
        .then(x => x.json())
        .then(result => {
            ownerOrderByICN = Object.values(result);
            displayStat();
        })
    await fetch('http://localhost:18683/stat/SumPrice')
        .then(x => x.json())
        .then(result => {
            injectionSumPrice = result;
            displayStat();
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
                `<button type="button" id="btn_delete" class="deletebtn" onclick="remove(${owner.id})">Delete</button>` +
                `<button type="button" id="btn_modify" class="editbtn" onclick="edit(${owner.id});showSelection()">Modify</button>`
                + "</td ></tr >"
        }
    )
}
function displayNonCrudOrderByPrice() {
    let name;
    let commonness;
    document.getElementById("non_crud_injection").innerHTML = null;
    injectionOrderByPrice[1].forEach(
        (injection) => {
            switch (injection.name) {
                case 1: name = "Bordetella_Bronchiseptica"; break;
                case 2: name = "Canine_Distemper"; break;
                case 3: name = "Canine_Hepatitis"; break;
                case 4: name = "Canine_Parainfluenza"; break;
                case 5: name = "Heartworm"; break;
                case 6: name = "Leptospirosis"; break;
                case 7: name = "Parvovirus"; break;
                case 7: name = "Rabies"; break;
                default: case 0: name = "Null"; break;
            }
            switch (injection.commonness) {
                case 1: commonness = "Monthly"; break;
                case 2: commonness = "Half_year"; break;
                case 3: commonness = "Yearly"; break;
                case 4: commonness = "Null"; break;
                default: case 0: commonness = "Once"; break;
            }
            document.getElementById('non_crud_injection').innerHTML +=
                "<tr><td>" + injection.id + "</td><td>" + name + "</td ><td>" + injection.price + "</td><td>" + commonness + "</td>"
        }
    )

}
function displayStat() {
    document.getElementById("non_crud_sum_price").innerHTML = null;
    document.getElementById("non_crud_owners").innerHTML = null;

    document.getElementById("non_crud_sum_price").innerHTML = "Sum price of injections: " + injectionSumPrice;
    ownerOrderByICN[1].forEach(
        (owner) => {
            if (owner.sex == 0) {
                sex = "Male";
            }
            else {
                sex = "Female";
            }
            document.getElementById('non_crud_owners').innerHTML +=
                "<tr><td>" + owner.id + "</td><td>" + owner.name + "</td ><td>" + owner.identityCardNumber + "</td><td>" + sex + "</td>"
        }
    )
}
function showSelection() {
        var x = document.getElementById("update_owner_section");
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {
            x.style.display = "none";
        }
}
function create() {
    let name = document.getElementById('create_owner_name').value
    let icn = document.getElementById('create_owner_icn').value
    let sex = document.getElementById('create_owner_sex').value
    console.log(name, icn, sex)
    fetch('http://localhost:18683/owner', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: name,
                identityCardNumber: icn,
                Sex: parseInt(sex)
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
    ownerUpdate = id
    console.log(ownerUpdate);
    document.getElementById("update_owner_name").value = owners[1].find(owner => owner['id'] == id)['name'];
    document.getElementById("update_owner_icn").value = owners[1].find(owner => owner['id'] == id)['identityCardNumber'];
    document.getElementById("update_owner_sex").value = owners[1].find(owner => owner['id'] == id)['sex'];
}

function update() {
    let name = document.getElementById('update_owner_name').value
    let icn = document.getElementById('update_owner_icn').value
    let sex = document.getElementById('update_owner_sex').value

    fetch('http://localhost:18683/owner', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Id: ownerUpdate,
                Name: name,
                identityCardNumber: icn,
                Sex: parseInt(sex)
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