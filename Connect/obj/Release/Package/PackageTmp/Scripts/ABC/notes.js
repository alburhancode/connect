$(document).ready(function () {
    $(document).on('click', "#note-save-button-click", function (e) {
        e.preventDefault();

        var noteText = $("#textbox-note").val().trim();

        if (noteText.length < 1)
            return;

        var individualid = $("#hdnIndividualId").attr("individualid");

        var note = {};
        note.NoteText = noteText;
        note.Individualid = individualid;


        $.ajax({
            url: 'notes-partial-create',
            method: "post",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: "{note: " + JSON.stringify(note) + "}",
            success: function (data) {
                $("#divIndividualNotes").html(data);
            },
            error: function () {
                alert("There was a problem creating your notes");
            }
        });

    });

    $(document).on('click', ".submit-leave-partial-delete", function (e) {
        e.preventDefault();
        var leave = {};
        leave.LeaveId = $(this).attr("leaveid");
        leave.Id = $(this).attr("id");

        $.ajax({
            url: 'leave-partial-delete',
            method: "post",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: "{leave: " + JSON.stringify(leave) + "}",
            success: function (data) {
                $("#divIndividualLeaves").html(data);
            },
            error: function () {
                alert("There was a problem deleting your leave");
            }
        });
    });

    $(document).on('click', "#leave-save-button-click", function (e) {
        e.preventDefault();

        var dateOfLeave = $("#leaveDatepicker1").val().trim();

        // need to check the format of the value and make sure its datetime format

        var reason = $("#reasonForLeave").val().trim();

        if (reason.length < 1)
            return;

        var id = $("#hdnIndividualGuid").attr("individualguid");
        var individualid = $("#hdnIndividualId").attr("individualid");

        var leave = {};
        leave.Reason = reason;
        leave.Id = id;
        leave.Individualid = individualid;
        leave.DateOfLeave = dateOfLeave;

        $.ajax({
            url: 'leave-partial-create',
            method: "post",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: "{leave: " + JSON.stringify(leave) + "}",
            success: function (data) {
                $("#divIndividualLeaves").html(data);
            },
            error: function () {
                alert("There was a problem in creating this leave");
            }
        });

    });

    // date picker
    $(document).on('focus', "#leaveDatepicker1", function () {
        $("#leaveDatepicker1").datepicker({ minDate: 0, dateFormat: "dd-MM-yy"});
    });
});

