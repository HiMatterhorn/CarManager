var routeURL = location.protocol + "//" + location.host;
var urlBookings = routeURL + '/api/Booking/GetCalendarDataForCarList';
var checkedvalues;
var dtoVIN;
var urlCalendarEvent;


$(document).ready(function () {
    urlCalendarEvent = $('#loader').data('request-url');
    getEventsSelectedCars();
});


function getEventsSelectedCars() {
    $('input[type="checkbox"]').change(function () {
        console.log(dtoVIN);
        updateSelectedCarsList();

        //Bookings
        $.ajax({
            url: urlBookings,
            contentType: 'application/json',
            datatype: JSON,
            data: JSON.stringify(dtoVIN),
            method: 'POST',

            success: function (response) {
                if (response.status === 1) {
                    $.each(response.dataenum, function (i, data) {

                        let inStartDateTime = new Date(data.booking.startDate);
                        let StartTime = inStartDateTime.toLocaleString("en-us", { hour: '2-digit', minute: '2-digit', hour12: false }); //year: 'numeric', month: 'numeric', day: 'numeric',

                        let inEndDateTime = new Date(data.booking.endDate);
                        let EndTime = inEndDateTime.toLocaleString("en-us", { hour: '2-digit', minute: '2-digit', hour12: false }); //year: 'numeric', month: 'numeric', day: 'numeric',

                        CalendarEvents.push({
                            title: StartTime + ' ' + data.booking.registrationNumber + ' ' + EndTime,
                            description: data.booking.description,
                            start: data.booking.startDate,
                            end: data.booking.endDate,
                            backgroundColor: backgroundEventColor(data.booking.bookingStatus),
                            borderColor: "#162466",
                            textColor: fontEventColor(data.booking.bookingStatus),
                            id: data.booking.id
                        });

                    })
                }
            },
            error: function (xhr) {
                $.notify("Error", "error");
            }
        });
        calendar.refetchEvents();
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
                    var CalendarEvents = [];
                    updateSelectedCarsList();

                    //Bookings
                    $.ajax({
                        url: urlBookings,
                        contentType: 'application/json',
                        datatype: JSON,
                        data: JSON.stringify(dtoVIN),
                        method: 'POST',

                        success: function (response) {
                            if (response.status === 1) {
                                $.each(response.dataenum, function (i, data) {

                                    let inStartDateTime = new Date(data.booking.startDate);
                                    let StartTime = inStartDateTime.toLocaleString("en-us", { hour: '2-digit', minute: '2-digit', hour12: false }); //year: 'numeric', month: 'numeric', day: 'numeric',

                                    let inEndDateTime = new Date(data.booking.endDate);
                                    let EndTime = inEndDateTime.toLocaleString("en-us", { hour: '2-digit', minute: '2-digit', hour12: false }); //year: 'numeric', month: 'numeric', day: 'numeric',

                                    CalendarEvents.push({
                                        title: StartTime + ' ' + data.booking.registrationNumber + ' ' + EndTime,
                                        description: data.booking.description,
                                        start: data.booking.startDate,
                                        end: data.booking.endDate,
                                        backgroundColor: backgroundEventColor(data.booking.bookingStatus),
                                        borderColor: "#162466",
                                        textColor: fontEventColor(data.booking.bookingStatus),
                                        id: data.booking.id
                                    });

                                })
                                successCallback(CalendarEvents);
                            }
                        },
                        error: function (xhr) {
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


function backgroundEventColor(eventStatus) {
    switch (eventStatus) {
        case 0: {
            return "white";
            break;
        }
        case 10: {
            return "#9DC0D7";
            break;
        }
        case 20: {
            return "#81C38A";
            break;
        }
        case 30: {
            return "green";
            break;
        }
        case 50: {
            return "#A3A196";
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
        case 10: {
            return "white";
            break;
        }
        case 20: {
            return "white";
            break;
        }
        case 30: {
            return "white";
            break;
        }
        case 50: {
            return "white";
            break;
        }
    }
}

function getEventDetailsByEventId(info) {
    $.ajax({
        url: urlCalendarEvent,
        dataType: 'html',
        data: { id: Number(info.id)},
        method: 'GET',

        success: function (result) {
            $('#_CalendarEventModal').html('').html(result);
            onCalendarEventShowModal()
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
}


function onCalendarEventShowModal() {
    $("#calendarEvent").modal("show");
}

function onCalendarEventCloseModal() {
    $("#calendarEventForm")[0].reset();
    $("#userName").val(' ');
    $("#registrationNumber").val(' ');
    $("#description").val(' ');
    $("#projectCost").val(' ');
    $("#calendarEvent").modal("hide");
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