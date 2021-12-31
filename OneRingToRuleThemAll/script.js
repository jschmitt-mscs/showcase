const headers = {
    'Accept': 'application/json',
    'Authorization': 'Bearer cR8RczB_XO_M2i6SAyjC'
};

document.addEventListener('DOMContentLoaded', function () {
    getCharacterData()
        .then(data => buildSelect(data))
        .then(function () {
            var _id = document.getElementById("lotrCharacterSelect").value;
            getCharacterData2(_id)
                .then(data => buildCharacterTable(data))
        });
});

document.getElementById('search').addEventListener('keyup', function () {
    filterTable();
});

document.getElementById("lotrCharacterSelect").addEventListener('change', function () {
    var _id = document.getElementById("lotrCharacterSelect").value;
    getCharacterData2(_id)
        .then(data => buildCharacterTable(data));

    var checkBox = document.getElementById("quotes");
    if (checkBox.checked) {

        getQuoteData(_id)
            .then(data => buildQuoteTable(data));

    } else {

    }

});

document.getElementById("quotes").addEventListener("change", function(){
    var checkBox = document.getElementById("quotes");
    if(!checkBox.checked){
        clearTable();
    } else {
        var _id = document.getElementById("lotrCharacterSelect").value;
        clearTable();
        getQuoteData(_id)
        .then(data => buildQuoteTable(data));
    }
});

function getBookData() {

    return fetch('https://the-one-api.dev/v2/book', { headers: headers })
        .then(response => response.json())
        .then(data => (data))
        .catch((error) => {
            console.error('Error: ', error);
        });
};

function getChapterData() {
    return fetch('https://the-one-api.dev/v2/book/' + document.getElementById('lotrBookSelect').value + '/chapter', { headers: headers })
        .then(response => response.json())
        .then(data => (data))
        .catch((error) => {
            console.error('Error: ', error);
        });
};

function getCharacterData() {
    return fetch('https://the-one-api.dev/v2/character', { headers: headers })
        .then(response => response.json())
        .then(data => (data))
        .catch((error) => {
            console.error('Error: ', error);
        });
};

function getCharacterData2(_id) {
    return fetch('https://the-one-api.dev/v2/character/' + _id, { headers: headers })
        .then(response => response.json())
        .then(data => (data))
        .catch((error) => {
            console.error('Error: ', error);
        });
};

function getQuoteData(characterId) {
    return fetch('https://the-one-api.dev/v2/character/' + characterId + '/quote', { headers: headers })
        .then(response => response.json())
        .then(data => (data))
        .catch((error) => {
            console.error('Error: ', error);
        });
}

function buildSelect(data) {
    data.docs.forEach(element => {
        var opt = document.createElement('option');
        opt.value = element._id;
        opt.innerHTML = element.name;
        document.getElementById("lotrCharacterSelect").appendChild(opt);
    });
};

function buildTable(data) {
    clearTable();
    var table = document.getElementById('tableTBody');
    var i = 1;
    data.docs.forEach(element => {
        var row = table.insertRow(table.rows.length - 1);
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        cell1.innerHTML = i;
        cell2.innerHTML = element.chapterName;
        table.appendChild(row);
        i++;
    });
};

function buildQuoteTable(data) {
    clearTable();
    var table = document.getElementById('tableTBody');
    var i = 1;
    data.docs.forEach(element => {
        console.log(JSON.stringify(element));
        var row = table.insertRow(table.rows.length - 1);
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        cell1.innerHTML = i;
        cell2.innerHTML = element.dialog;
        table.appendChild(row);
        i++;
    });
}


function buildCharacterTable(data) {
    var characterName = document.getElementById("characterName");
    var characterRace = document.getElementById("characterRace");
    var characterGender = document.getElementById("characterGender");
    var characterHeight = document.getElementById("characterHeight");
    var characterBirth = document.getElementById("characterBirth");
    var characterDeath = document.getElementById("characterDeath");
    var characterDetails = document.getElementById("characterDetails");

    characterName.innerHTML = data.docs[0].name;
    characterRace.innerHTML = data.docs[0].race;
    characterGender.innerHTML = data.docs[0].gender;
    characterHeight.innerHTML = data.docs[0].height;
    characterBirth.innerHTML = data.docs[0].birth;
    characterDeath.innerHTML = data.docs[0].death;
    characterDetails.setAttribute('href', data.docs[0].wikiUrl);
}


function clearTable() {
    var table = document.getElementById('tableTBody');
    table.innerHTML = '';
};

function filterTable() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("search");
    filter = input.value.toUpperCase();
    table = document.getElementById("tableTBody");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}
