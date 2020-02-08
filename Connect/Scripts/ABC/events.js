$(document).ready(function () {

    // main tab stuff
    $(document).on('click', "#tabAssessments", function (e) {
        e.preventDefault();
        var url = 'assessments-get';

        $.get(url, function (data) {
            $('#divTabAssessment').html(data);
        });
    });

    // date picker
    $(document).on('focus', "#datepicker1", function () {
        $("#datepicker1").datepicker({ dateFormat: "dd-MM-yy" });
    });

    $(document).on('click', "#close-event-first-modal3", function () {
        $('#gameModal3').modal('hide');
    });

    $(document).on('click', "#create-assessment", function (e) {
        e.preventDefault();
        var url = $(this).attr('url');

        $.get(url, function (data) {
            $('#gameContainer3').html(data);
            $('#gameModal3').modal('show');
        });
    });

    $(document).on('click', "#close-event-first-modal3", function () {
        $('#gameModal3').modal('hide');
    });

    $(document).on('click', "#create-assessment-post", function (e) {
        e.preventDefault();
        var assessment = {};
        assessment.EventTypeId = $("#dropdown-assessment option:selected").val();
        assessment.ConductedBy = $("#ConductedBy").val();
        assessment.AdditionalText = $("#AdditionalText").val();
        assessment.DateConducted = $("#datepicker1").val();
        assessment.MaxMarks = $("#MaxMarks").val();

        if (assessment.ConductedBy.length == 0) {
            if (confirm("You have not provided details of 'Who' conducted this Quiz. Typically it would be the [Subject Teacher]")) {
                assessment.ConductedBy = "Subject Teacher"
            }
            else {
                return false;
            }
        }

        if (assessment.DateConducted.length == 0) {
            alert('Please provide a date when this quiz was conducted')
            return false;
        }

        $('#gameModal3').modal('hide');

        $.ajax({
            url: 'create-assessment-ajax',
            method: "post",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: "{assessment: " + JSON.stringify(assessment) + "}",
            success: function (data) {
                $("#divTabAssessment").html(data);
            },
            error: function () {
                //alert("There was a problem creating your quiz");
            }
        });
    });

    $(document).on('click', "#delete-assessment", function (e) {
        e.preventDefault();
        var url = $(this).attr('url');

        $.get(url, function (data) {
            $('#divTabAssessment').html(data);
        });
    });


    $(document).on('click', "#create-assessment-type", function (e) {
        e.preventDefault();
        var url = $(this).attr('url');
        $.get(url, function (data) {
            $('#divCreateAssessmentTypeContainer').html(data);
            $('#createAssessmentTypeModal').modal('show');
        });
    });

    $(document).on('click', "#close-create-assessment-type-modal", function () {
        $('#createAssessmentTypeModal').modal('hide');
    });

    $(document).on('click', "#create-assessment-type-post", function (e) {
        e.preventDefault();
        $('#createAssessmentTypeModal').modal('hide');
        var individualEvent = {};
        individualEvent.Type = $("#Type").val();
        individualEvent.Subject = $("#Subject").val();
        individualEvent.AdditionalText = $("#AdditionalText").val();

        $.ajax({
            url: 'create-assessment-type-ajax',
            method: "post",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: "{individualEvent: " + JSON.stringify(individualEvent) + "}",
            success: function (data) {
                $("#divAssessmentTypes").html(data);
            },
            error: function () {
                alert("There was a problem updating your individual quiz");
            }
        });
    });

    $(document).on('click', "#delete-event-type", function (e) {
        e.preventDefault();
        var url = $(this).attr('url');

        $.get(url, function (data) {
            $('#divAssessmentTypes').html(data);
        });
    });

    $(document).on('click', "#close-event-first-modal2", function () {
        $('#gameModal2').modal('hide');
    });

    $(document).on('click', "#create-individual-event", function (e) {
        e.preventDefault();
        var url = $(this).attr('url');

        $.get(url, function (data) {
            $('#gameContainer2').html(data);
            $('#gameModal2').modal('show');
        });
    });

    $(document).on('click', "#close-event-first-modal2", function () {
        $('#gameModal2').modal('hide');
    });

    $(document).on('click', "#create-individual-event-post", function (e) {
        e.preventDefault();
        $('#gameModal2').modal('hide');
        var individualEvent = {};
        individualEvent.EventId = $("#dropdown-individual-event option:selected").val();
        individualEvent.Value = $("#Value").val();
        individualEvent.AdditionalText = $("#AdditionalText").val();
        individualEvent.IndividualId = $("#hdnIndividualEventIndividualId").val();

        $.ajax({
            url: 'create-individual-event-ajax',
            method: "post",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: "{individualEvent: " + JSON.stringify(individualEvent) + "}",
            success: function (data) {
                $("#divIndividualEventOccurences").html(data);
            },
            error: function () {
                alert("There was a problem updating your individual quiz");
            }
        });
    });

    $(document).on('click', "#delete-individual-event", function (e) {
        e.preventDefault();
        var url = $(this).attr('url');

        $.get(url, function (data) {
            $('#divIndividualEventOccurences').html(data);
        });
    });

    $(document).on('change', "#dropdown-section-events", function (e) {
        e.preventDefault();

        var section = $(".js_sections option:selected").text();//$(".js_sections").val();
        var selectedEvent = $(".js_quizes option:selected").val();//$(".js_quizes").val();

        var url = 'individuals-by-section-get';

        $.get(url, { section: section, selectedEvent: selectedEvent }, function (data) {
            $('#individual-events-by-section').html(data);
        });
    });
    
    $(document).on('click', "#showGame", function (e) {
        e.preventDefault();
        var url = $(this).attr('url');

        $.get(url, function (data) {
            $('#gameContainer').html(data);
            $('#gameModal').modal('show');
        });
    });

    $(document).on('click', "#close-event-first-modal", function () {
        $('#gameModal').modal('hide');
    });

    $(document).on('click', "#main_quiz_tab", function () {
        location.reload();
    });

    $(document).on('focusout', "#item_MarksObtained", function (e) {
        e.preventDefault();
        var marks = $(this).val();
        var individualid = $(this).attr("individualid");
        var eventid = $(".js_quizes option:selected").val();

        if (marks.length == 0) {
            marks = -1;
            $(this).removeClass("textbox-my-assessments-quizmarks");
            $(this).addClass("textbox-my-assessments-quizmarks-null");
        }
        else {
            $(this).removeClass("textbox-my-assessments-quizmarks-null");
            $(this).addClass("textbox-my-assessments-quizmarks");
        }

        var data = JSON.stringify({
            'individualid': individualid,
            'eventid': eventid,
            'marks': marks
        });

        $.ajax({
            url: 'create-or-update-or-delete-individual-quiz-marks-ajax',
            method: "post",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: function (data) {
            },
            error: function () {
                alert("There was a problem updating your individual quiz");
            }
        });
        // give it an id so we can save against it. ideally we should give it individualeventid by pulling it from the database first when initially loaded
    });

    $(document).on('keyup', "#item_MarksObtained", function (e) {
        e.preventDefault();
        var maxmarks = $("#max_marks").text();
        // the input should be an integer between 2 number
        var val = parseInt(this.value);
        if (val > maxmarks || val < 0) {
            this.value = this.value.substr(0, maxmarks.length-1);
        }
        if (val < 0) {
            this.value = "";
        }
    });

    $(document).on('click', "#btn_confirm_fake", function () {
        //location.reload(); 
        alert('Your data has been saved successfully. Please note that an Auto Save operation was in progress while you entered your data.');
    });
});

