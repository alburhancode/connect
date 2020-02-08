$(document).ready(function () {

    //var isAdmin = $('#hdnAdmin');
    //if (isAdmin.val() == "false") {
    //    $(".JS_Campus_Selection option[value=1]").hide();
    //    $(".JS_Campus_Selection option[value=3]").hide();
    //}

    $(".JS_Campus_Selection").change(function (e) {
        e.preventDefault();

        // do this for timings as well
        var selectedCampusText = $(".JS_Campus_Selection option:selected").text(); 
        if (selectedCampusText == "-- Please select --")
        {
            $(".JS_InterviewSelectionShowHide").css('display', 'none');
            $(".JS_TimingSelectionShowHide").css('display', 'none');
        }
        else
        {
            $(".JS_InterviewSelectionShowHide").css('display', 'block');
            $(".JS_TimingSelectionShowHide").css('display', 'block');
        }

        if (selectedCampusText == "Islamabad (Men)" || selectedCampusText == "Karachi") {
            $(".JS_Consents").css('display', 'block');
        }
        else {
            $(".JS_Consents").css('display', 'none');
        }

        if ($(".JS_Campus_Selection option:selected").text() == "-- Please select --") {
            $(".JS_Campus_Selection").css('border-color', 'red');
        }
        else {
            $(".JS_Campus_Selection").css('border-color', '');
        }

        $.ajax({
            url: 'AvailableInterviewSlots',
            method: "get",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: { campusSelection: $(this).val() },
            success: function (data) {
                $(".JS_InterviewSelectionShowHide").html(data);
            },
            error: function () {
                alert("There was a problem trying to load interview data");
            }
        });

        $.ajax({
            url: 'AvailableBatchTimings',
            method: "get",
            dataType: "html",
            contentType: 'application/json; charset=utf-8',
            data: { campusSelection: $(this).val() },
            success: function (data) {
                $(".JS_TimingSelectionShowHide").html(data);
            },
            error: function () {
                alert("There was a problem trying to load interview data");
            }
        });
    });

    $("#check_all_dropped").click(function () {
        $("input[name='dropped']:checkbox").not(this).prop('checked', this.checked);
    });

    $(".button-checkbox-dropped").click(function (e) {
        e.preventDefault();
        var favorite = [];
        $.each($("input[name='enrolled']:checked"), function () {
            favorite.push($(this).val());
        });

        var ids = favorite.join(",");
        var decision = 'dropped';

        if (ids.length == 0) {
            alert('Please select atleast one record to drop.');
            return;
        }

        var data = JSON.stringify({
            'ids': ids,
            'decision': decision
        });

        $.ajax({
            url: 'decision-partial',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: function () {
                setTimeout(function () {// wait for 5 secs(2)
                    location.reload(); // then reload the page.(3)
                }, 500);
            },
            error: function () {
                alert("There was a problem. Please retry or contact your IT team");
            }
        });
    });

    $(".button-checkbox-undodrop").click(function (e) {
        e.preventDefault();
        var favorite = [];
        $.each($("input[name='dropped']:checked"), function () {
            favorite.push($(this).val());
        });

        var ids = favorite.join(",");
        var decision = 'enrolled';

        if (ids.length == 0) {
            alert('Please select atleast one record to undo your previous decision.');
            return;
        }

        if (!confirm("Please note that an email had already been sent to the selected individual(s) therefore this action should only be performed if you would like to change your decision. Are you entirely sure that you want to undo your initial decision?"))
            return false;

        var data = JSON.stringify({
            'ids': ids,
            'decision': decision
        });

        $.ajax({
            url: 'decision-partial',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: function () {
                setTimeout(function () {// wait for 5 secs(2)
                    location.reload(); // then reload the page.(3)
                }, 500);
            },
            error: function () {
                alert("There was a problem. Please retry or contact your IT team");
            }
        });
    });

    $("#check_all").click(function () {
        $("input[name='enrolled']:checkbox").not(this).prop('checked', this.checked);
    });

    $("#check_all_pending").click(function () {
        $("input[name='pending']:checkbox").not(this).prop('checked', this.checked);
    });

    $("#check_all_rejected").click(function () {
        $("input[name='rejected']:checkbox").not(this).prop('checked', this.checked);
    });
    
    $(".dropdown-section").change(function (e) {
        e.preventDefault();
        var section = this.value;
        var favorite = [];
        $.each($("input[name='enrolled']:checked"), function () {
            favorite.push($(this).val());
        });

        var ids = favorite.join(",");

        if (ids.length == 0) {
            alert('Please select atleast one record for section change.');
            $(".dropdown-section").val('NotSelected');
            $("#check_all").attr('checked', false);

            return;
        }

        //if (ids.split(',').length > 5) {
        //    alert('You have selected more individuals than the maximum number allowed in a section.');
        //    $(".dropdown-section").val('NotSelected');

        //    return;
        //}

        var data = JSON.stringify({
            'ids': ids,
            'section': section
        });

        $.ajax({
            url: 'update-section',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: function (response) {

                //$.each($("input[name='enrolled']:checked"), function () {
                //    $('#section_' + $(this).val()).text(section);
                //    $(this).attr('checked', false);
                //});

                $(".dropdown-section").val('NotSelected');
                $("#check_all").attr('checked', false);

                $('#emailscount').text(response.emails)
                $('#sectionscount').text(response.sections)


                setTimeout(function () {// wait for 5 secs(2)
                    location.reload(); // then reload the page.(3)
                }, 500);
            },
            error: function () {
                alert("There was a problem. Please retry or contact your IT team");
            }
        });
    });

    $(".button-send-email").click(function (e) {
        e.preventDefault();
        var favorite = [];
        $.each($("input[name='enrolled']:checked"), function () {
            favorite.push($(this).val());
        });

        var ids = favorite.join(",");

        if (ids.length == 0) {
            alert('Please select atleast one record to send email to.');
            $("#check_all").attr('checked', false);
            return;
        }

        var data = JSON.stringify({
            'ids': ids
        });

        $.ajax({
            url: 'send-email',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: function (response) {

                $.each($("input[name='enrolled']:checked"), function () {
                    $('#emailsent_' + $(this).val()).text('True');
                    $(this).attr('checked', false);
                });

                $("#check_all").attr('checked', false);
                $('#emailscount').text(response.emails)
                $('#sectionscount').text(response.sections)

                setTimeout(function () {// wait for 5 secs(2)
                    location.reload(); // then reload the page.(3)
                }, 500);
            },
            error: function () {
                alert("There was a problem. Please retry or contact your IT team");
            }
        });
    });

    $(".button-checkbox-selection").click(function (e) {
        e.preventDefault();
        var favorite = [];
        $.each($("input[name='pending']:checked"), function () {
            favorite.push($(this).val());
        });

        var ids = favorite.join(",");
        var decision = 'enrolled';

        if (ids.length == 0) {
            alert('Please select atleast one record to enrol.');
            return;
        }

        var data = JSON.stringify({
            'ids': ids,
            'decision': decision
        });

        $.ajax({
            url: 'decision-partial',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: function () {
                setTimeout(function () {// wait for 5 secs(2)
                    location.reload(); // then reload the page.(3)
                }, 500);
            },
            error: function () {
                alert("There was a problem. Please retry or contact your IT team");
            }
        });
    });

    $(".button-checkbox-rejection").click(function (e) {
        e.preventDefault();
        var favorite = [];
        $.each($("input[name='pending']:checked"), function () {
            favorite.push($(this).val());
        });

        var ids = favorite.join(",");
        var decision = 'rejected';

        if (ids.length == 0) {
            alert('Please select atleast one record for rejection.');
            return;
        }

        var data = JSON.stringify({
            'ids': ids,
            'decision': decision
        });

        $.ajax({
            url: 'decision-partial',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: function () {
                setTimeout(function () {// wait for 5 secs(2)
                    location.reload(); // then reload the page.(3)
                }, 500);
            },
            error: function () {
                alert("There was a problem. Please retry or contact your IT team");
            }
        });
    });

    $(".button-checkbox-undo-enrolled").click(function (e) {
        e.preventDefault();
        var favorite = [];
        $.each($("input[name='enrolled']:checked"), function () {
            favorite.push($(this).val());
        });

        var ids = favorite.join(",");
        var decision = 'undo';

        if (ids.length == 0) {
            alert('Please select atleast one record to undo your previous decision.');
            return;
        }

        if (!confirm("Please note that an email had already been sent to the selected individual(s) therefore this action should only be performed if you would like to change your decision. Are you entirely sure that you want to undo your initial decision?"))
            return false;

        var data = JSON.stringify({
            'ids': ids,
            'decision': decision
        });

        $.ajax({
            url: 'decision-partial',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: function () {
                setTimeout(function () {// wait for 5 secs(2)
                    location.reload(); // then reload the page.(3)
                }, 500);
            },
            error: function () {
                alert("There was a problem. Please retry or contact your IT team");
            }
        });
    });
       
    $(".button-checkbox-undo").click(function (e) {
        e.preventDefault();
        var favorite = [];
        $.each($("input[name='rejected']:checked"), function () {
            favorite.push($(this).val());
        });

        var ids = favorite.join(",");
        var decision = 'undo';

        if (ids.length == 0) {
            alert('Please select atleast one record to undo your previous decision.');
            return;
        }

        if (!confirm("Please note that an email had already been sent to the selected individual(s) therefore this action should only be performed if you would like to change your decision. Are you entirely sure that you want to undo your initial decision?"))
            return false;

        var data = JSON.stringify({
            'ids': ids,
            'decision': decision
        });

        $.ajax({
            url: 'decision-partial',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: function () {
                setTimeout(function () {// wait for 5 secs(2)
                    location.reload(); // then reload the page.(3)
                }, 500);
            },
            error: function () {
                alert("There was a problem. Please retry or contact your IT team");
            }
        });
    });

    $(".submit_payment_partial").click(function (e) {

        e.preventDefault();
        var model = {};
        model.Id = $("#Id").val();

        $(".JS_PaymentMethod").css("display", "none");
        var paymentMethod = $("input[name='PaymentMethod']:checked").val();
        //if (paymentMethod === "InterviewDay") {
        //    return true;
        //}
        if (paymentMethod === "AlBurhan") {
            var txtValuePre = $("#PaymentCodeManualPreHyphen").val();
            var txtValuePost = $("#PaymentCodeManualPostHyphen").val();
            if (txtValuePre.length <= 0 || txtValuePost.length <= 0) {
                $('.JS_PaymentMethod').css("display", "block");
                $(window).scrollTop($('.JS_PaymentMethod').offset().top - 100);
                return false;
            }

            model.PaymentCodeManualPreHyphen = txtValuePre;
            model.PaymentCodeManualPostHyphen = txtValuePost;
        }
        else {
            var txtValueEasyPaisa = $("#PaymentCodeEasyPaisa").val();
            if (txtValueEasyPaisa.length <= 0) {
                $('.JS_PaymentMethod').css("display", "block");
                $(window).scrollTop($('.JS_PaymentMethod').offset().top - 100);
                return false;
            }

            model.PaymentCodeEasyPaisa = txtValueEasyPaisa;
        }

        model.PaymentMethod = paymentMethod;

        $.ajax({
            url: 'payments-partial',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: "{model: " + JSON.stringify(model) + "}",
            success: function (data) {
                alert("payment saved successfully");
//                windows.location = data.newurl;
            },
            error: function () {
                alert("There was a problem saving your payment details");
            }
        });

    });

    $("#SubmitPanelGrading1").click(function (e) {
        e.preventDefault();
        var model = {};

        // Id
        model.Id = $("#Id").val();

        // Other Grades
        model.GradeCommitment = $("#GradeCommitment option:selected").val();
        model.GradeStrengths = $("#GradeStrengths option:selected").val();
        model.GradePassion = $("#GradePassion option:selected").val();
        model.GradeFunctionalExpertise = $("#GradeFunctionalExpertise option:selected").val();
        model.GradeExistingAffiliation = $("#GradeExistingAffiliation option:selected").val();

        // Other Comments
        model.IntroProfileDescription =	$("#IntroProfileDescription").val();
        model.IntroKeyAchievements =	$("#IntroKeyAchievements").val();
        model.MotivationReasonForEnrollment =	$("#MotivationReasonForEnrollment").val();
        model.MotivationGoals =	$("#MotivationGoals").val();
        model.MotivationIfNotSelected =	$("#MotivationIfNotSelected").val();
        model.StrengthsTopTwo =	$("#StrengthsTopTwo").val();
        model.StrengthsHobbies =	$("#StrengthsHobbies").val();
        model.CommittmentAnyCourseBefore =	$("#CommittmentAnyCourseBefore").val();
        model.CommittmentFulfillPromises =	$("#CommittmentFulfillPromises").val();
        model.CommittmentExample =	$("#CommittmentExample").val();
        model.CommittmentFailures =	$("#CommittmentFailures").val();
        model.AdditionalSkillsAnyOther =	$("#AdditionalSkillsAnyOther").val();
        model.AdditionalSkillsSocial = $("#AdditionalSkillsSocial").val();

        // Panel Comments
        var panelComments = $("#PanelComments").val();
        if (panelComments === "") {
            alert('Please enter some text for the "Overall Comments"');
            return false;
        }

        model.PanelComments = panelComments;
        
        // Panel Grade
        var selectionText = $("#PanelGrade option:selected").text();

        if (selectionText === "please select a grade") {
            alert('Please select an "Overall Grade"');
            return false;
        }

        model.PanelGrade = $("#PanelGrade option:selected").val();

        $.ajax({
            url: 'panel-grading-partial',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: "{model: " + JSON.stringify(model) + "}",
            success: function (data) {
                setTimeout(function () {// wait for 5 secs(2)
                    location.reload(); // then reload the page.(3)
                }, 500); 
            },
            error: function () {
                alert("Your grading did not save successfully. Please contact your IT team");
            }
        });
    });

    $("#SubmitPanelGrading2").click(function (e) {
        e.preventDefault();
        var model = {};

        // Id
        model.Id = $("#Id").val();

        // Other Grades
        model.GradeCommitment = $("#GradeCommitment option:selected").val();
        model.GradeStrengths = $("#GradeStrengths option:selected").val();
        model.GradePassion = $("#GradePassion option:selected").val();
        model.GradeFunctionalExpertise = $("#GradeFunctionalExpertise option:selected").val();
        model.GradeExistingAffiliation = $("#GradeExistingAffiliation option:selected").val();

        // Other Comments
        model.IntroProfileDescription = $("#IntroProfileDescription").val();
        model.IntroKeyAchievements = $("#IntroKeyAchievements").val();
        model.MotivationReasonForEnrollment = $("#MotivationReasonForEnrollment").val();
        model.MotivationGoals = $("#MotivationGoals").val();
        model.MotivationIfNotSelected = $("#MotivationIfNotSelected").val();
        model.StrengthsTopTwo = $("#StrengthsTopTwo").val();
        model.StrengthsHobbies = $("#StrengthsHobbies").val();
        model.CommittmentAnyCourseBefore = $("#CommittmentAnyCourseBefore").val();
        model.CommittmentFulfillPromises = $("#CommittmentFulfillPromises").val();
        model.CommittmentExample = $("#CommittmentExample").val();
        model.CommittmentFailures = $("#CommittmentFailures").val();
        model.AdditionalSkillsAnyOther = $("#AdditionalSkillsAnyOther").val();
        model.AdditionalSkillsSocial = $("#AdditionalSkillsSocial").val();

        // Panel Comments
        var panelComments = $("#PanelComments").val();
        if (panelComments === "") {
            alert('Please enter some text for the "Overall Comments"');
            return false;
        }

        model.PanelComments = panelComments;

        // Panel Grade
        var selectionText = $("#PanelGrade option:selected").text();

        if (selectionText === "please select a grade") {
            alert('Please select an "Overall Grade"');
            return false;
        }

        model.PanelGrade = $("#PanelGrade option:selected").val();

        $.ajax({
            url: 'panel-grading-partial',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: "{model: " + JSON.stringify(model) + "}",
            success: function (data) {
                setTimeout(function () {// wait for 5 secs(2)
                    location.reload(); // then reload the page.(3)
                }, 500);
            },
            error: function () {
                alert("Your grading did not save successfully. Please contact your IT team");
            }
        });
    });

    $("#SubmitCeoGrading").click(function (e) {
        e.preventDefault();
        var model = {};
        model.Id = $("#Id").val();
        model.Comments = $("#Comments").val();

        var selectionText = $("#Grade option:selected").text();

        if (selectionText === "please select a grade") {
            alert('Please select a grade');
            return false;
        }

        var selectedGrade = $("#Grade option:selected").val();
        model.Grade = selectedGrade;

        $.ajax({
            url: 'ceo-grading-partial',
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            data: "{model: " + JSON.stringify(model) + "}",
            success: function (data) {
                setTimeout(function () {// wait for 5 secs(2)
                    location.reload(); // then reload the page.(3)
                }, 500); 
            },
            error: function () {
                alert("Your grading did not save successfully. Please contact your IT team");
            }
        });
    });

    if ($("input[name='PaymentMethod']:checked").val() === "InterviewDay") {
        $('.JS_PayInterviewDay').css("display", "block");
        $('.JS_Chk_InterviewDayMessage').css("color", "black");
        $('.JS_PayManual').css("display", "none");
        $('.JS_Chk_AlBurhanMessage').css("color", "gray");
        $('.JS_PayEasyPaisa').css("display", "none");
        $('.JS_Chk_EasyPaisaMessage').css("color", "gray");
        $('.JS_InterviewSelectionShowHide').hide();
    }
    else if ($("input[name='PaymentMethod']:checked").val() === "AlBurhan")
    {
        $('.JS_PayInterviewDay').css("display", "none");
        $('.JS_Chk_InterviewDayMessage').css("color", "gray");
        $('.JS_PayManual').css("display", "block");
        $('.JS_Chk_AlBurhanMessage').css("color", "black");
        $('.JS_PayEasyPaisa').css("display", "none");
        $('.JS_Chk_EasyPaisaMessage').css("color", "gray");
        $('.JS_InterviewSelectionShowHide').show();
    }
    else
    {
        $('.JS_PayInterviewDay').css("display", "none");
        $('.JS_Chk_InterviewDayMessage').css("color", "gray");
        $('.JS_PayManual').css("display", "none");
        $('.JS_Chk_AlBurhanMessage').css("color", "gray");
        $('.JS_PayEasyPaisa').css("display", "block");
        $('.JS_Chk_EasyPaisaMessage').css("color", "black");
        $('.JS_InterviewSelectionShowHide').show();
    }

    // Profession
    if ($("#Student").is(":checked")) {
        $('.JS_Profession').css("display", "none");
        $('.JS_Headings_Pro').css("display", "none");
        $('.JS_Business').css("display", "none");
        $('.JS_Headings_Bus').css("display", "none");
    }

    if ($("#Professional").is(":checked")) {
        $('.JS_Profession').css("display", "block");
        $('.JS_Headings_Pro').css("display", "block");
        $('.JS_Business').css("display", "none");
        $('.JS_Headings_Bus').css("display", "none");
    }

    if ($("#Businessman").is(":checked")) {
        $('.JS_Profession').css("display", "none");
        $('.JS_Headings_Pro').css("display", "none");
        $('.JS_Business').css("display", "block");
        $('.JS_Headings_Bus').css("display", "block");
    }

    // Highest Qualification
    if ($("#Education").val() == 0) {
        $(".JS_Inter").hide();
        $(".JS_Graduation").hide();
        $(".JS_Masters").hide();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 1) {
        $(".JS_Inter").show();
        $(".JS_Graduation").hide();
        $(".JS_Masters").hide();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 2) {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").hide();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 3) {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").show();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 4) {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").show();
        $(".JS_MS").show();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 5) {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").show();
        $(".JS_MS").show();
        $(".JS_PHD").show();
    }

    // do this for timings as well
    if ($(".JS_Campus_Selection option:selected").text() == "-- Please select --") {
        $(".JS_InterviewSelectionShowHide").css('display', 'none');
        $(".JS_TimingSelectionShowHide").css('display', 'none');
    }
    else {
        $(".JS_InterviewSelectionShowHide").css('display', 'block');
        $(".JS_TimingSelectionShowHide").css('display', 'block');
    }

});

$(".JS_Enrol").click(function () {
    return confirm("Are you sure that you are an Administrator or acting as an Administrator to perform this action?");
});

//$(".JS_PaymentPass").click(function () {
//    return confirm("You have chosen to Pass this applicant. Are you sure?");
//});

//$(".JS_PaymentFail").click(function () {
//    return confirm("You have chosen to Fail or Remind this applicant. Are you sure?");
//});

$(".submit_apply_admin").click(function () {
    var returnValue = true;

    $("#Name").css('border-color', 'rgba(0,0,0,.3)');
    $("#Phone").css('border-color', 'rgba(0,0,0,.3)');
    $("#Age").css('border-color', 'rgba(0,0,0,.3)');
    $("#Password").css('border-color', 'rgba(0,0,0,.3)');
    $("#ConfirmPassword").css('border-color', 'rgba(0,0,0,.3)');

    if ($(".JS_Campus_Selection option:selected").text() == "-- Please select --") {
        $(".JS_Campus_Selection").css('border-color', 'red');
        returnValue = false;
    }

    if ($("#Name").val() == "") {
        $("#Name").css('border-color', 'red');
        returnValue = false;
    }

    if ($("#Phone").val() == "") {
        $("#Phone").css('border-color', 'red');
        returnValue = false;
    }

    if ($("#Age").val() == "") {
        $("#Age").css('border-color', 'red');
        returnValue = false;
    }

    var p1 = $("#Password").val();
    var p2 = $("#ConfirmPassword").val();

    if (p1 == "") {
        $("#Password").css('border-color', 'red');
        $("#Password").val("");
        returnValue = false;
    }

    if (p2 == "") {
        $("#ConfirmPassword").css('border-color', 'red');
        $("#ConfirmPassword").val("");
        returnValue = false;
    }

    if (p1 != "" && p2 != "" && p1 != p2) {
        $(".JS_PasswordMisMatch").css("display", "block");
        returnValue = false;
    }

    return returnValue;
});


$(".submit_apply").click(function () {
    var returnValue = true;

    $('.tdChkFeeConsent').css("border", "none");
    $('.tdChkCommittmentConsent').css("border", "none");

    $("#Name").css('border-color', 'rgba(0,0,0,.3)');
    $("#Phone").css('border-color', 'rgba(0,0,0,.3)');
    $("#Age").css('border-color', 'rgba(0,0,0,.3)');
    $("#Password").css('border-color', 'rgba(0,0,0,.3)');
    $("#ConfirmPassword").css('border-color', 'rgba(0,0,0,.3)');

    var selectedCampusText = $(".JS_Campus_Selection option:selected").text(); 
    var feeConsent = $('.JS_FeeConsent').is(':checked');
    var committmentConsent = $('.JS_CommittmentConsent').is(':checked');

    if ((selectedCampusText == "Islamabad (Men)" || selectedCampusText == "Karachi"))
    {
        if (feeConsent == false) {
            returnValue = false;
            $('.tdChkFeeConsent').css("border", "1px solid red");
        }

        if (committmentConsent == false) {
            returnValue = false;
            $('.tdChkCommittmentConsent').css("border", "1px solid red");
        }
    }

    if ($(".JS_Campus_Selection option:selected").text() == "-- Please select --")
    {
        $(".JS_Campus_Selection").css('border-color', 'red');
        returnValue = false;
    }

    if ($("#Name").val() == "") {
        $("#Name").css('border-color', 'red');
        returnValue = false;
    }

    if ($("#Phone").val() == "") {
        $("#Phone").css('border-color', 'red');
        returnValue = false;
    }

    if ($("#Age").val() == "") {
        $("#Age").css('border-color', 'red');
        returnValue = false;
    }

    var p1 = $("#Password").val();
    var p2 = $("#ConfirmPassword").val();

    if (p1 == "") {
        $("#Password").css('border-color', 'red');
        $("#Password").val("");
        returnValue = false;
    }

    if (p2 == "")
    {
        $("#ConfirmPassword").css('border-color', 'red');
        $("#ConfirmPassword").val("");
        returnValue = false;
    }

    if (p1 != "" && p2 != "" && p1 != p2)
    {
        $(".JS_PasswordMisMatch").css("display", "block");
        returnValue = false;
    }

    //$(".JS_PaymentMethod").css("display", "none");
    //var paymentMethod = $("input[name='PaymentMethod']:checked").val();
    //if (paymentMethod === "InterviewDay") { return true; }
    //if (paymentMethod === "AlBurhan") {
    //    var txtValuePre = $("#PaymentCodeManualPreHyphen").val();
    //    var txtValuePost = $("#PaymentCodeManualPostHyphen").val();
    //    if (txtValuePre.length <= 0 || txtValuePost.length <= 0) {
    //        $('.JS_PaymentMethod').css("display", "block");
    //        $(window).scrollTop($('.JS_PaymentMethod').offset().top - 100);
    //        return false;
    //    }
    //}
    //else {
    //    var txtValueEasyPaisa = $("#PaymentCodeEasyPaisa").val();
    //    if (txtValueEasyPaisa != undefined && txtValueEasyPaisa.length <= 0) {
    //        $('.JS_PaymentMethod').css("display", "block");
    //        $(window).scrollTop($('.JS_PaymentMethod').offset().top - 100);
    //        return false;
    //    }
    //}

    //if (returnValue == false)
    //    return returnValue;

    //TODO same for timing
    var interviewSlotId = $("#SelectedInterviewSlotId").val();
    $("#SelectedInterviewSlotId").val(interviewSlotId);

    //$.ajax({
    //    url: 'SaveSelectedInterviewSlot',
    //    method: "post",
    //    dataType: "json",
    //    contentType: 'application/json; charset=utf-8',
    //    data: "{ interviewSlotId: " + interviewSlotId + "}",
    //    success: function (data) {
    //        alert(data);
    //    },
    //    error: function (response) {
    //        alert("error is this ...")
    //        alert(response.responseText);
    //    }
    //});
    return returnValue;
});

$(".submit_register").click(function () {

    $(".JS_PaymentMethod").css("display", "none");
    var returnValue = true;

    $(".JS_Education_Field").each(function () {
        if ($(this).is(":visible")) {
            if ($(this).val().length === 0) {
                $(".JS_Education_Message").css('display', 'block');
                $(this).css('border-color', 'red');
                returnValue = false;
                $(window).scrollTop($('.JS_Education_Message').offset().top - 100);
            } else {
                $(this).css('border-color', '');
            }
        }
    });

    $(".JS_Current_Field").each(function () {
        if ($(this).is(":visible")) {
            if ($(this).val().length === 0) {
                $(".JS_Current_Message").css('display', 'block');
                $(this).css('border-color', 'red');
                $(window).scrollTop($('.JS_Current_Message').offset().top - 100);
                returnValue = false;
            } else {
                $(this).css('border-color', '');
            }
        }
    });

    //if (returnValue) {
    //    $(".submit_register").css('display', 'none');
    //}

    return returnValue;
});

$(".submit_payment").click(function () {
    $(".JS_PaymentMethod").css("display", "none");
    var paymentMethod = $("input[name='PaymentMethod']:checked").val();
    if (paymentMethod === "InterviewDay") {
        return true;
    }
    if (paymentMethod === "AlBurhan") {
        var txtValuePre = $("#PaymentCodeManualPreHyphen").val();
        var txtValuePost = $("#PaymentCodeManualPostHyphen").val();
        if (txtValuePre.length <= 0 || txtValuePost.length <= 0) {
            $('.JS_PaymentMethod').css("display", "block");
            $(window).scrollTop($('.JS_PaymentMethod').offset().top - 100);
            return false;
        }
    }
    else {
        var txtValueEasyPaisa = $("#PaymentCodeEasyPaisa").val();
        if (txtValueEasyPaisa.length <= 0) {
            $('.JS_PaymentMethod').css("display", "block");
            $(window).scrollTop($('.JS_PaymentMethod').offset().top - 100);
            return false;
        }
    }
});

$('.JS_Payment').on('change', function () {
    if ($("input[name='PaymentMethod']:checked").val() === "InterviewDay") {
        $('.JS_PayInterviewDay').css("display", "block");
        $('.JS_Chk_InterviewDayMessage').css("color", "black");
        $('.JS_PayManual').css("display", "none");
        $('.JS_Chk_AlBurhanMessage').css("color", "gray");
        $('.JS_PayEasyPaisa').css("display", "none");
        $('.JS_Chk_EasyPaisaMessage').css("color", "gray");
        $('.JS_InterviewSelectionShowHide').hide();

    }
    else if ($("input[name='PaymentMethod']:checked").val() === "AlBurhan") {
        $('.JS_PayInterviewDay').css("display", "none");
        $('.JS_Chk_InterviewDayMessage').css("color", "gray");
        $('.JS_PayManual').css("display", "block");
        $('.JS_Chk_AlBurhanMessage').css("color", "black");
        $('.JS_PayEasyPaisa').css("display", "none");
        $('.JS_Chk_EasyPaisaMessage').css("color", "gray");
        $('.JS_InterviewSelectionShowHide').show();
    }
    else {
        $('.JS_PayInterviewDay').css("display", "none");
        $('.JS_Chk_InterviewDayMessage').css("color", "gray");
        $('.JS_PayManual').css("display", "none");
        $('.JS_Chk_AlBurhanMessage').css("color", "gray");
        $('.JS_PayEasyPaisa').css("display", "block");
        $('.JS_Chk_EasyPaisaMessage').css("color", "black");
        $('.JS_InterviewSelectionShowHide').show();
    }
});

$('.JS_CurrentStatus').on('change', function () {
    var currentStatus = $(this).val();

    if (currentStatus == "Student") {
        $('.JS_Profession').css("display", "none");
        $('.JS_Headings_Pro').css("display", "none");
        $('.JS_Business').css("display", "none");
        $('.JS_Headings_Bus').css("display", "none");
    }

    if (currentStatus == "Professional") {
        $('.JS_Profession').css("display", "block");
        $('.JS_Headings_Pro').css("display", "block");
        $('.JS_Business').css("display", "none");
        $('.JS_Headings_Bus').css("display", "none");
    }

    if (currentStatus == "Businessman") {
        $('.JS_Profession').css("display", "none");
        $('.JS_Headings_Pro').css("display", "none");
        $('.JS_Business').css("display", "block");
        $('.JS_Headings_Bus').css("display", "block");
    }
});

$('select[name=Education]').change(function () {
    if ($("#Education").val() == 0) {
        $(".JS_Inter").hide();
        $(".JS_Graduation").hide();
        $(".JS_Masters").hide();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 1) {
        $(".JS_Inter").show();
        $(".JS_Graduation").hide();
        $(".JS_Masters").hide();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 2) {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").hide();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 3) {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").show();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 4) {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").show();
        $(".JS_MS").show();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 5) {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").show();
        $(".JS_MS").show();
        $(".JS_PHD").show();
    }
});

$(".JS_MaxPaymentCodeManualPreHyphen").keypress(function () {
    var returnValue = true;

    if ($(this).val().length >= 4 || event.which < 48 || event.which > 57) {
        $(this).val($(this).val().substr(0, 4));
        returnValue = false;
    }
    return returnValue;
});

$(".JS_MaxPaymentCodeManualPostHyphen").keypress(function () {
    var returnValue = true;
    if ($(this).val().length >= 4 || event.which < 48 || event.which > 57) {
        $(this).val($(this).val().substr(0, 4));
        returnValue = false;
    }
    return returnValue;
});

$(".JS_MaxPaymentCodeEasyPaisa").keypress(function () {
    var returnValue = true;
    if ($(this).val().length >= 10 || event.which < 48 || event.which > 57) {
        $(this).val($(this).val().substr(0, 10));
        returnValue = false;
    }
    return returnValue;
});

$(".JS_MaxNicNumber").keypress(function () {
    var returnValue = true;
    if ($(this).val().length >= 13 || event.which < 48 || event.which > 57) {
        $(this).val($(this).val().substr(0, 13));
        returnValue = false;
    }
    return returnValue;
});

$(".JS_MaxOtherContact").keypress(function () {
    var returnValue = true;
    if ($(this).val().length >= 14 || event.which < 48 || event.which > 57) {
        $(this).val($(this).val().substr(0, 14));
        returnValue = false;
    }
    return returnValue;
});

$(".JS_Marks").keypress(function () {
    var returnValue = true;
    if ($(this).val().length >= 2 || event.which < 48 || event.which > 57) {
        $(this).val($(this).val().substr(0, 2));
        returnValue = false;
    }
    return returnValue;
});

$(".JS_PhoneMaxLength").keypress(function () {
    var returnValue = true;
    if ($(this).val().length >= 14 || event.which < 48 || event.which > 57) {
        $(this).val($(this).val().substr(0, 14));
        returnValue = false;
    }
    return returnValue;
});

$(".JS_AgeRange").keyup(function () {
    var returnValue = true;
    var age = parseInt($(this).val());
    if (age < 12 || age > 99 || event.which < 48 || event.which > 57) {
        $(this).val($(this).val().substr(0, 2));
        returnValue = false;
    }
    return returnValue;
});

$(".JS_Password").keypress(function () {
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

$(".JS_ConfirmPassword").keypress(function () {
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

$(".JS_ConfirmPassword").keyup(function () {
    $(".JS_PasswordMisMatch").css("display", "none");
    var $password1 = $(".JS_Password");
    var $password2 = $(".JS_ConfirmPassword");


    if ($password1.val().length === $password2.val().length && $password1.val() === $password2.val()) {
        $(".JS_PasswordMisMatch").css("display", "none");
        return true;
    }
    else if ($password2.val() !== $password1.val().substr(0, $password2.val().length) ) {
        $(".JS_PasswordMisMatch").css("display", "block");
        return false;
    }
});

$(".JS_Password").keyup(function () {
    var $password1 = $(".JS_Password");
    var $password2 = $(".JS_ConfirmPassword");

    if ($password1.val().length === $password2.val().length && $password1.val() === $password2.val()) {
        $(".JS_PasswordMisMatch").css("display", "none");
        return true;
    }
    else if ($password1.val() === $password2.val().substr(0, $password1.val().length)) {
        $(".JS_PasswordMisMatch").css("display", "block");
        return false;
    }
});

$(".JS_Password_Login").keypress(function () {
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

