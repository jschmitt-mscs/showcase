let selectedCells = [];

function updateTimes() {
    let eventStartTime = document.getElementById("eventStartTime");
    let eventEndTime = document.getElementById("eventEndTime");
    let egDate = new Date();
    egDate.setSeconds(0);
    egDate.setMinutes(0);
    egDate.setHours(egDate.getHours() + 1);
    eventStartTime.value = egDate.toTimeString().split(" ")[0];
    egDate.setHours(egDate.getHours() + 1);
    eventEndTime.value = egDate.toTimeString().split(" ")[0];
}

function updateToAllDay(){
    let eventStartTime = document.getElementById("eventStartTime");
    let eventEndTime = document.getElementById("eventEndTime");
    let egDate = new Date();
    egDate.setSeconds(0);
    egDate.setMinutes(0);
    egDate.setHours(0);
    eventStartTime.value = egDate.toTimeString().split(" ")[0];
    egDate.setHours(23);
    egDate.setMinutes(59);
    eventEndTime.value = egDate.toTimeString().split(" ")[0];
}

function validateNewEvent(){
    let form = document.forms["newEvent"];
    let eventStartDay = form["eventStartDay"].value;
    let eventEndDay = form["eventEndDay"].value;
    let eventStartTime = form["eventStartTime"].value;
    let eventEndTime = form["eventEndTime"].value;

    let startDate = new Date(eventStartDay + " " + eventStartTime);
    let endDate = new Date(eventEndDay + " " + eventEndTime);

    if(endDate > startDate){
        return true;
    } else {
        document.getElementById("validationWarning").style.display = "block";
        return false;
    }    
}

function dayView(){
    let dg = new Date(document.getElementsByClassName("bg-info")[0].getAttribute("celldate"));
    console.log(dg.toString());
}

function generateDayView(dateString){
    let hours = [0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23];

    let mainDiv = document.createElement("div");

    let zoomDate = new Date(dateString);

}

function showCalendar(month, year) {
    let firstDay = (new Date(year, month)).getDay();
    document.getElementById("monthyear").innerHTML = months[month] + " " + year;
    tbl = document.getElementById("calendarbody");

    let date = 1;
    for (let i = 0; i < 6; i++) {
        // creates a table row
        let row = document.createElement("tr");
        //creating individual cells, filing them up with data.
        for (let j = 0; j < 7; j++) {
            if (i === 0 && j < firstDay) {
                cell = document.createElement("td");
                cellText = document.createTextNode("");
                cell.appendChild(cellText);
                row.appendChild(cell);
            }
            else if (date > daysInMonth(month, year)) {
                break;
            }

            else {
                cell = document.createElement("td");
                cellP = document.createElement("p");
                cellP.classList.add("calendarDay");              

                cellText = document.createTextNode(date);
                cell.classList.add("cellDate");
                let CellDate = new Date(year, month, date);
                cell.setAttribute('onclick', 'selectedCell(this.id)');
                cell.setAttribute('id', date);
                cell.setAttribute('cellDate', CellDate.toISOString().split(".")[0].split("T")[0]);
                // if (date === today.getDate() && year === today.getFullYear() && month === today.getMonth()) {
                //     cell.classList.add("bg-info");
                // } // color today's date
                cellP.appendChild(cellText);
                cell.appendChild(cellP);
                row.appendChild(cell);
                date++;
            }
        }
        tbl.appendChild(row); // appending each row into calendar body.
    }
}

function addMonthToTable(){
    
}


function daysInMonth(iMonth, iYear) {
    return 32 - new Date(iYear, iMonth, 32).getDate();
}

function selectedCell(id){
    let selectedCell = document.getElementById(id);

    for( let i =0; i < selectedCells.length; i++){
        selectedCells[i].classList.remove("bg-info");
    }
    selectedCells.length = 0;
    selectedCell.classList.add("bg-info");
    selectedCells.push(selectedCell);
}

function loadEvents(events){
    events.forEach(element => addEventToCalendar(element));        
}

function timeString(date){
    let dated = new Date(date.toString());
    dated.setMinutes(dated.getMinutes()+360)
    let timeString ="";
    if (dated.getHours().toString().length < 2) {
        timeString += "0" + dated.getHours();
    } else {
        timeString += dated.getHours();
    }
    if(dated.getMinutes().toString().length < 2){
        timeString += ":0" + dated.getMinutes();
    } else {
        timeString += ":" + dated.getMinutes();
    }
    return timeString;
}

function addEventToCalendar(element){
    let eventStart = new Date(element.Start.toString());
    eventStart.setMinutes(eventStart.getMinutes()+360); //need to add 360 minutes for timezone
    let eventend = new Date(element.End);
    eventend.setMinutes(eventend.getMinutes()+360);
    let eventDay = eventStart.getDate();
    let eventTime = eventStart.getTime();
    let cell = document.getElementById(eventDay);
    let listElement = document.createElement("li");
    let elementText = document.createTextNode(timeString(element.Start) + " | " + element.Name);
    listElement.appendChild(elementText);
    listElement.classList.add("listElement");
    cell.appendChild(listElement);

}

