var routeURL = location.protocol + "//" + location.host;

$(document).ready(function () {
    InitializeCalendar();
});

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
                    onShowModal(event, null);
                },
                eventDisplay: 'block',

              events: function (fetchInfo, successCallback, failureCallback) {
                    $.ajax({
                        url: routeURL + '/api/Booking/GetCalendarDataForCar?carVIN=' + $('#carVIN').val(),
                        type: 'GET',
                        dataType: 'JSON',
                        success: function (response) {
                            var events = [];
                            if (response.status === 1) {
                                $.each(response.dataenum, function (i, data) {
                                    events.push({
                                        title: data.userName,
                                        description: data.destination,
                                        start: data.startDate,
                                        end: data.endDate,

                                        backgroundColor: data.isApproved ? "#28a745" : "#dc3545",
                                        borderColor: "#162466",
                                        textColor: "white",
                                        id: data.id
                                    });

                                })
                            }
                            successCallback(events);
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

function getEventDetailsByEventId(info) {
    $.ajax({
        url: routeURL + '/api/Booking/GetCalendarDataById/' + info.id,
        type: 'GET',
        dataType: 'JSON',


        success: function (response) {
            if (response.status === 1 && response.dataenum != undefined)
            {
                onShowModal(response.dataenum, true)
            }
            //successCallback(events);
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
}


function onShowModal(obj, isEventDetail) {
    if (isEventDetail != null) {
        $("#id").val(obj.id); 
        $("#userName").html(obj.userName);
        $("#registrationNumber").html(obj.registrationNumber);
        $("#destination").html(obj.destination);
        $("#projectCost").html(obj.projectCost);


   //     $("#title").val(obj.title);
       /* $("#description").val(obj.description);
        $("#appointmentDate").val(obj.startDate);

        $("#id").val(obj.id);*/
 
       /* if (obj.isDoctorApproved) {
            $("#lblStatus").html('Approved');
            $("#btnConfirm").addClass("d-none");
            $("#btnSubmit").addClass("d-none");
        }
        else {
            $("#lblStatus").html('Pending');
            $("#btnConfirm").removeClass("d-none");
            $("#btnSubmit").removeClass("d-none");
        }
        $("#btnDelete").removeClass("d-none");*/
    }

/*    else {
        $("#appointmentDate").val(obj.startStr + " " + new moment().format("hh:mm A"));
        $("#id").val(0);
        $("#btnDelete").addClass("d-none");

    }
    */

    $("#calendarEvent").modal("show");

}

function onCloseModal() {
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

function onConfirm() {
    var id = parseInt($("#id").val());

    $.ajax({
        url: routeURL + '/api/Booking/ConfirmEvent/' + id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status === 1) {
                $.notify(response.message, "success");
                calendar.refetchEvents();
                onCloseModal();
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

function onDelete() {
    var id = parseInt($("#id").val());

    $.ajax({
        url: routeURL + '/api/Booking/DeleteEvent/' + id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status === 1) {
                $.notify(response.message, "success");
                calendar.refetchEvents();
                onCloseModal();
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