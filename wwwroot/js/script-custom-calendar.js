var routeURL = location.protocol + "//" + location.host;

$(document).ready(function () {
    InitializeCalendar();
});

function InitializeCalendar() {
    try {
        var calendarEl = document.getElementById('calendar');

        console.log($('#carVIN').val());

        if (calendarEl != null) {
            calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',

                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                },
                eventDisplay: 'block',
/*                events: [{
                    title: 'event2',
                    start: '2022-08-05',
                    end: '2022-08-07'
                }],*/

              events: function (fetchInfo, successCallback, failureCallback) {
                    $.ajax({
                        url: routeURL + '/api/Booking/GetCalendarDataForCar?carVIN=' + $('#carVIN').val(),
                        type: 'GET',
                        dataType: 'JSON',
                        success: function (response) {
                            var events = [];
                            if (response.status === 1) {
                                console.log(response);


                                $.each(response.dataenum, function (i, data) {
                                    events.push({
                                        title: data.userName,
                                        description: data.destination,
                                        start: data.startDate,
                                        end: data.endDate,

                                        //backgroundColor: data.isApproved ? "#28a745" : "#dc3545",
                                        //borderColor: "#162466",
                                        //textColor: "white",
                                        //id: data.Id
                                    });

                                })
                            }
                            successCallback(events);
                            console.log(events);
                        },
                        error: function (xhr) {
                            console.log("failure");
                            $.notify("Error", "error");
                        }
                    });
                },
                eventClick: function (info) {
                    getEventDetailsByEventId(info.event)
                }
            });
            calendar.render();
        }

    }
    catch (e) {
        alert(e);
    }
}


//SECOND OPTION
/*$(document).ready(function () {
    InitializeCalendar();
});
document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth'
    });
    calendar.render();
});*/

