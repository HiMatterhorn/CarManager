var routeURL = location.protocol + "//" + location.host;
var checkedvalues;
var dtoVIN;


$(document).ready(function () {
    InitializeCalendar();
    getEventsSelectedCars();
});


function getEventsSelectedCars() {
    $('input[type="checkbox"]').change(function () {
        updateSelectedCarsList();

        $.ajax({
            url: routeURL + '/api/Booking/GetCalendarDataForCarList',
            contentType: 'application/json',
            datatype: JSON,
            data: JSON.stringify(dtoVIN),
            method: 'POST',

            success: function (response) {
                if (response.status === 1) {
                    calendar.refetchEvents();
                }
                else {
                    //TODO notify
                }
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        })
    });
}

function updateSelectedCarsList() {
    checkedvalues = $('input:checkbox:checked').map(function () {
        return this.value;
    }).get();

    dtoVIN = {
        Selected: checkedvalues
    };
}

function InitializeCalendar() {
    try {
        var calendarEl = document.getElementById('calendar');

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
                    onCalendarEventShowModal(event, null);
                },
                eventDisplay: 'block',
                displayEventTime: false,
                displayEventEnd: false,

                eventTimeFormat: { // like '14:30'
                    hour: '2-digit',
                    minute: '2-digit',

                    meridiem: false
                },

                weekNumbers: 'true',
                weekText: 'CW',

                events: function (fetchInfo, successCallback, failureCallback) {
                    updateSelectedCarsList();

                    $.ajax({
                        url: routeURL + '/api/Booking/GetCalendarDataForCarList',
                        contentType: 'application/json',
                        datatype: JSON,
                        data: JSON.stringify(dtoVIN),
                        method: 'POST',

                        success: function (response) {
                            var events = [];
                            if (response.status === 1) {
                                $.each(response.dataenum, function (i, data) {

                                    let inStartDateTime = new Date(data.startDate);
                                    let StartTime = inStartDateTime.toLocaleString("en-us", { hour: '2-digit', minute: '2-digit', hour12: false }); //year: 'numeric', month: 'numeric', day: 'numeric',

                                    let inEndDateTime = new Date(data.endDate);
                                    let EndTime = inEndDateTime.toLocaleString("en-us", { hour: '2-digit', minute: '2-digit', hour12: false }); //year: 'numeric', month: 'numeric', day: 'numeric',

                                    events.push({
                                        title: StartTime + ' ' + data.registrationNumber + ' ' + EndTime,
                                        description: data.destination,
                                        start: data.startDate,
                                        end: data.endDate,
                                        backgroundColor: backgroundEventColor(data.bookingStatus),
                                        /*data.isApproved ? "#28a745" : "#dc3545",*/
                                        borderColor: "#162466",
                                        textColor: fontEventColor(data.bookingStatus),
                                        id: data.id
                                    });

                                })
                            }
                            successCallback(events);
                        },
                        error: function (xhr) {
                            $.notify("Error", "error");
                            alert(xhr.responseText);
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

function backgroundEventColor(eventStatus) {
    switch (eventStatus) {
        case 0: {
            return "#DCE3F8";
            break;
        }
        case 1: {
            return "#A9BCF5";
            break;
        }
        case 2: {
            return "#5167A8";
            break;
        }
        case 3: {
            return "#F5E0A9";
            break;
        }
    }
}

function fontEventColor(eventStatus) {
    switch (eventStatus) {
        case 0: {
            return "black";
            break;
        }
        case 1: {
            return "black";
            break;
        }
        case 2: {
            return "white";
            break;
        }
        case 3: {
            return "black";
            break;
        }
    }
}

function getEventDetailsByEventId(info) {
    $.ajax({
        url: routeURL + '/api/Booking/GetCalendarDataById/' + info.id,
        type: 'GET',
        dataType: 'JSON',


        success: function (response) {
            if (response.status === 1 && response.dataenum != undefined) {
                onCalendarEventShowModal(response.dataenum, true)
            }
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
}

function onCalendarEventShowModal(obj, isEventDetail) {
    if (isEventDetail != null) {
        $("#id").val(obj.id);
        $("#userName").html(obj.userName);
        $("#registrationNumber").html(obj.registrationNumber);
        $("#destination").html(obj.destination);
        $("#projectCost").html(obj.projectCost);

        $("#calendarEvent").modal("show");
    }
}

function onCalendarEventCloseModal() {
    $("#calendarEventForm")[0].reset();
    $("#userName").val(' ');
    $("#registrationNumber").val(' ');
    $("#destination").val(' ');
    $("#projectCost").val(' ');
    $("#calendarEvent").modal("hide");
}

function onCarChange() {
    calendar.refetchEvents();
}

function onCalendarEventConfirm() {
    var eventId = parseInt($("#id").val());

    $.ajax({
        url: routeURL + '/api/Booking/ConfirmEvent',
        type: 'GET',
        data: { id: eventId },
        dataType: 'JSON',
        success: function (response) {
            if (response.status === 1) {
                $.notify(response.message, "success");
                calendar.refetchEvents();
                onCalendarEventCloseModal();
            }
            else {
                $.notify(response.message, "error");
            }

        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
}

function onCalendarEventDelete() {
    var eventId = parseInt($("#id").val());

    $.ajax({
        url: routeURL + '/api/Booking/DeleteEvent',
        type: 'GET',
        data: { id: eventId },
        dataType: 'JSON',
        success: function (response) {
            if (response.status === 1) {
                $.notify(response.message, "success");
                calendar.refetchEvents();
                onCalendarEventCloseModal();
            }
            else {
                $.notify(response.message, "error");
            }

        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
}