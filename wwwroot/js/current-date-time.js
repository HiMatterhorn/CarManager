var objToday = new Date();

function getRoundedSearchDateTime() {
    let startDateTime = getDateTime(objToday);
    let endDateTime = getDateTime(objToday, 3, '18:00');

    return { startDateTime, endDateTime }
}

function getLimitDates() {

    let minDate = new Date();
    minDate.setFullYear((minDate.getFullYear() - 3));

    let maxDate = new Date();
    maxDate.setFullYear((maxDate.getFullYear() + 3));

    let DateMin = formatDate(minDate);
    let DateMax = formatDate(maxDate);
    let YearMin = minDate.getFullYear();
    let YearMax = maxDate.getFullYear();

    return {DateMin, DateMax, YearMin, YearMax }
}

function getNextYear() {
    let nextYear = new Date();
    nextYear.setFullYear((nextYear.getFullYear() + 1));

    let NextYear = formatDate(nextYear);

    return NextYear
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

function getDateTime(date, offsetDays = 0, hour = 0) {
    let dayOffset = Number(offsetDays);
    let objDay = date;
    objDay.setDate(date.getDate() + dayOffset);

    let Day = objDay.getDate(),
        Month = objDay.getMonth() + 1,
        Year = objDay.getFullYear(),
        Hour = objDay.getHours(),
        Minute = objDay.getMinutes(),
        Seconds = objDay.getSeconds(),
        // weekday = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'),
        // dayOfWeek = weekday[objToday.getDay()],
        Date = Day + "/" + Month + "/" + Year,
        Time;

    if (hour != 0) {
        Time = hour;
    }
    else {
        Time = roundTimeQuarterHour(objDay).getHours() + ":" + roundTimeQuarterHour(objDay).getMinutes();
    }

    let DateTime = Date + " " + Time;
    return DateTime
}

function formatDate(date) {
    let objDay = date;

    let Day = objDay.getDate(),
        Month = objDay.getMonth() + 1,
        Year = objDay.getFullYear(),
        Date = Day + "/" + Month + "/" + Year;

    return Date
}