var objToday = new Date();

function getRoundedSearchDateTime() {
    let startDateTime = getDateTime(objToday);
    let endDateTime = getDateTime(objToday, 3, '18:00');

    /*    let startDate = curDay + "/" + curMonth + "/" + curYear;
        let startTime = roundTimeQuarterHour(objToday).getHours() + ":" + roundTimeQuarterHour(objToday).getMinutes();
        let startDateTime = startDate + " " + startTime;
    
        let endDate = (curDay + 1) + "/" + curMonth + "/" + curYear;
        let endTime = "18:00";
        let endDateTime = endDate + " " + endTime;*/

    return { startDateTime, endDateTime }
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

function getDateTime(date, offset = 0, hour = 0) {
    let dayOffset = Number(offset);
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