$(document).ready(function () {
    $(document).on('click', "#attendance-toggle-button-click", function (e) {
        $(".ajax_loader").show();
        e.preventDefault();
        var individualattendance = {};
        individualattendance.Id = $("#hdnIndividualGuid").attr("individualguid");
        individualattendance.AttendanceId = $(this).attr("attendanceid");
        individualattendance.Status = $(this).attr("status");

        $.ajax({
            url: 'attendance-partial-update',
            method: "post",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: "{attendance: " + JSON.stringify(individualattendance) + "}",
            success: function (data) {
                $("#divIndividualAttendance").html(data);
            },
            error: function () {
                alert("There was a problem updating your attendance");
            }
        });
    });

    $(document).on('change', "#dropdown-section-attendance", function (e) {
        $(".ajax_loader").show();
        e.preventDefault();

        var section = $(".js_sections_attendance option:selected").text();//$(".js_sections").val();
        //var selectedEvent = $(".js_courses option:selected").val();//$(".js_quizes").val();

        var url = 'individual-attendance-by-section-get';

        $.get(url, { section: section }, function (data) {
            $('#individual-attendance-by-section').html(data);
            $(".ajax_loader").hide();
        });
    });

    $(document).on('change', "#dropdown-subject-attendance", function (e) {
        e.preventDefault();

        var subject = $(".js_subjects_attendance option:selected").text();//$(".js_sections").val();
        //var selectedEvent = $(".js_courses option:selected").val();//$(".js_quizes").val();

        var url = 'individual-attendance-by-subject-get';

        $.get(url, { subject: subject }, function (data) {
            $('#individual-attendance-by-subject').html(data);
        });
    });

    $(document).on('click', "#SaveCallDetails", function (e) {
        e.preventDefault();
        var individualid = $(this).attr('individualid');
        var calloutcome = $(".JS_Attendance_CallOutcome" + individualid).val();
        var callnotes = $(".JS_Attendance_CallNotes" + individualid).val();
        var caller = $(".JS_Attendance_Caller" + individualid).val();
        var kefiyat = $(".JS_Attendance_Kefiyat" + individualid).val();

        alert(kefiyat);

        if (calloutcome.length == 0 || callnotes.length == 0 || caller.length == 0 || kefiyat.length == 0) {
            alert('please provide all the details for the call');
            return;
        }

        var data = JSON.stringify({
            'individualid': individualid,
            'calloutcome': calloutcome,
            'callnotes': callnotes,
            'caller': caller,
            'kefiyat': kefiyat
        });

        $.ajax({
            url: 'create-calldetails-ajax',
            method: "post",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: function () {
                $(".JS_Attendance_CallOutcome" + individualid).attr('style', 'opacity:0.6');
                $(".JS_Attendance_CallNotes" + individualid).attr('style', 'opacity:0.6');
                $(".JS_Attendance_Caller" + individualid).attr('style', 'opacity:0.6');
                $(".JS_Attendance_Kefiyat" + individualid).attr('style', 'opacity:0.6');
            },
            error: function () {
                alert("There was a problem updating your record");
            }
        });
    });
});

