var objToday = new Date(),
    curDay = objToday.getDate(),
    curMonth = objToday.getMonth() + 1,
    curYear = objToday.getFullYear(),
    curHour = objToday.getHours(),
    curMinute = objToday.getMinutes(),
    curSeconds = objToday.getSeconds();
    // weekday = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'),
    // dayOfWeek = weekday[objToday.getDay()],


function getRoundedSearchDateTime() {
    let startDate = curDay + "/" + curMonth + "/" + curYear;
    let startTime = roundTimeQuarterHour(objToday).getHours() + ":" + roundTimeQuarterHour(objToday).getMinutes();
    let startDateTime = startDate + " " + startTime;

    let endDate = (curDay + 1) + "/" + curMonth + "/" + curYear;
    let endTime = "18:00";
    let endDateTime = endDate + " " + endTime;

    return { startDateTime, endDateTime }
}

function getCurrentDateTime() {
    return objToday
}

function roundTimeQuarterHour(time) {
    var timeToReturn = new Date(time);

    timeToReturn.setMilliseconds(Math.floor(timeToReturn.getMilliseconds() / 1000) * 1000);
    timeToReturn.setSeconds(Math.floor(timeToReturn.getSeconds() / 60) * 60);
    timeToReturn.setMinutes(Math.ceil(timeToReturn.getMinutes() / 15) * 15);

    if ((Math.ceil(timeToReturn.getMinutes() / 15) * 15) == 4) {
        timeToReturn.setHours(timeToReturn.getHours() + 1);
    }

    return timeToReturn
}