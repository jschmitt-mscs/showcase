$(document).ready(function() {

    //main calendar table
    today = new Date();
    currentMonth = today.getMonth();
    currentYear = today.getFullYear();

    months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    tbl = document.getElementById("calendarbody");
    showCalendar(currentMonth, currentYear);
    loadEvents(initData);





    //new event modal

    $("#eventStartDay").change(function(){
        let eventEndDay = document.getElementById("eventEndDay");
        eventEndDay.value = document.getElementById("eventStartDay").value;
        updateTimes();
    });

    $("#allDay").change(function(){
        if($("#allDay").is(':checked')){
            updateToAllDay();
        } else {
            updateTimes(); 
        }
    });

    //calendar infinite scroll

    $(window).scroll(function(){
        if($(document).height() - $(this).height == $(this).scrollTop()){
            console.log("att bottom");
        }
    });
    
    

    //misc

    $("#dayView").click(function(){
        dayView();
    });

    $(".listElement").click(function(){
        console.log(this.innerHTML);
    });

    $("#testButton").click(function(){
        $.get("/api/events", function(data, status){
            console.log(data);
        })
    })

});

