$(document).ready(function () {

    if ($("input[name='PaymentMethod']:checked").val() == "AlBurhan") {
        $('.JS_PayManual').css("display", "block");
        $('.JS_PayEasyPaisa').css("display", "none");
    } else {
        $('.JS_PayManual').css("display", "none");
        $('.JS_PayEasyPaisa').css("display", "block");
    }

    //$(".submit_register").enable();
    //$(".submit_register").css('display', 'block');
    //$(".submit_apply").css('display', 'block');

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
});

$(".JS_Enrol").click(function() {
    return confirm("Are you sure that you are an Administrator or acting as an Administrator to perform this action?");
});

$(".JS_PaymentPass").click(function () {
    return confirm("You have chosen to Pass this applicant. Are you sure?");
});

$(".JS_PaymentFail").click(function () {
    return confirm("You have chosen to Fail this applicant. Are you sure?");
});

$(".submit_apply").click(function() {
    if ($(".JS_FeeConsentCheck").is(':checked')) {
        return true;
    }

    $(".JS_ConsentMessage").css('display', 'block');

    return false;
});

$(".submit_register").click(function () {
    var returnValue = true;

    var paymentMethod = $("input[name='PaymentMethod']:checked").val();

    if (paymentMethod === "InterviewDay") {
        return true;
    }

    if (paymentMethod === "AlBurhan") {
        var txtValuePre = $("#PaymentCodeManualPreHyphen").val();
        var txtValuePost = $("#PaymentCodeManualPostHyphen").val();
        if (txtValuePre.length <= 0 || txtValuePost.length <= 0) {
            $('.JS_PaymentMethod').css("display", "block");
            returnValue = false;
            $(window).scrollTop($('.JS_PaymentMethod').offset().top - 100);
        }
    }
    else
    {
        var txtValueEasyPaisa = $("#PaymentCodeEasyPaisa").val();
        if (txtValueEasyPaisa.length <= 0) {
            $('.JS_PaymentMethod').css("display", "block");
            returnValue = false;
            $(window).scrollTop($('.JS_PaymentMethod').offset().top - 100);
        }
    }

    $(".JS_Education_Field").each(function() {
        if ($(this).is(":visible")) {
            if ($(this).val().length == 0) {
                $(".JS_Education_Message").css('display', 'block');
                $(this).css('border-color', 'maroon');
                returnValue = false;
                $(window).scrollTop($('.JS_Education_Message').offset().top - 100);
            } else {
                $(this).css('border-color', '');
            }
        }
    });

    $(".JS_Current_Field").each(function () {
        if ($(this).is(":visible")) {
            if ($(this).val().length == 0) {
                $(".JS_Current_Message").css('display', 'block');
                $(this).css('border-color', 'maroon');
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

$('.JS_Payment').on('change', function ()
{
    if ($("input[name='PaymentMethod']:checked").val() == "AlBurhan") {
        $('.JS_PayManual').css("display", "block");
        $('.JS_PayEasyPaisa').css("display", "none");
    } else {
        $('.JS_PayManual').css("display", "none");
        $('.JS_PayEasyPaisa').css("display", "block");
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
    if ($("#Education").val() == 0)
    {
        $(".JS_Inter").hide();
        $(".JS_Graduation").hide();
        $(".JS_Masters").hide();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 1)
    {
        $(".JS_Inter").show();
        $(".JS_Graduation").hide();
        $(".JS_Masters").hide();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 2)
    {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").hide();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 3)
    {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").show();
        $(".JS_MS").hide();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 4)
    {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").show();
        $(".JS_MS").show();
        $(".JS_PHD").hide();
    }
    else if ($("#Education").val() == 5)
    {
        $(".JS_Inter").show();
        $(".JS_Graduation").show();
        $(".JS_Masters").show();
        $(".JS_MS").show();
        $(".JS_PHD").show();
    }
});

$(".JS_MaxPaymentCodeManualPreHyphen").keypress(function () {
    var returnValue = true;

    if ($(this).val().length >= 3 || event.which < 48 || event.which > 57) {
        $(this).val($(this).val().substr(0, 3));
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
