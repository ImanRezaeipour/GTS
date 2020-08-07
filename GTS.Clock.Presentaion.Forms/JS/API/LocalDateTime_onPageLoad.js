
var angleSec = 0;
var angleMin = 0;
var angleHour = 0;

$(document).ready
(
   function () {
       GetBoxesHeaders_LocalDateTime();
       document.body.dir = document.LocalDateTimeForm.dir;
       //$("#sec").rotate(angleSec);
       //$("#min").rotate(angleMin);
       //$("#hour").rotate(angleHour);
   }
);

setInterval(function () {
    var d = new Date;

    angleSec = (d.getSeconds() * 6);
    $("#sec").rotate(angleSec);

    angleMin = (d.getMinutes() * 6);
    $("#min").rotate(angleMin);

    angleHour = ((d.getHours() * 5 + d.getMinutes() / 12) * 6);
    $("#hour").rotate(angleHour);

}, 1000);

