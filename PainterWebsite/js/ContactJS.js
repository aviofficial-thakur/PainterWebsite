
    $(function () {

        //called when key is pressed in textbox
        $("#txtmobileno").keypress(function (e) {
            if (e.which != 9 && e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                swal({ title: "Only Numeric Value", type: "error", showConfirmButton: true });
                return false;
            }
        });
    });
    function isValidEmail(email) {
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailPattern.test(email);
}


    function SaveUser(flag) {
        debugger;
    var listobj = {
        ClientName: $('#txtname').val(),
    ClientEmailId: $('#txtemailid').val(),
    ClientMobileNo: $('#txtmobileno').val(),
    ClientMessage: $('#txtmessage').val()
        }


    if (listobj.ClientName == '') {
        swal({ title: "Please enter name", type: "error", showConfirmButton: true });
    return false;
        }

    if (!isValidEmail(listobj.ClientEmailId)) {
        swal({ title: "Please enter a valid email address", type: "error", showConfirmButton: true });
    return false;
        }

    if (listobj.ClientEmailId == '') {
        swal({ title: "Please enter emailid", type: "error", showConfirmButton: true });
    return false;
        }
    if (listobj.ClientMobileNo == '') {
        swal({ title: "Please enter mobile no", type: "error", showConfirmButton: true });
    return false;
        }



    try {
        $.ajax({
            url: "/Home/SaveClient",
            method: "POST",
            data: JSON.stringify(listobj),
            contentType: "application/json",
            dataType: "json",
            success: function (result) {
                if (result === "1") {
                    swal({ title: "Successfully Added", type: "success", timer: 3000, showConfirmButton: false });
                    ResetData();
                    if (flag == 2) {
                        closePopup();
                    }
                }
                else {
                    swal({ title: "Not Saved", type: "error", showConfirmButton: true });
                }
            },
            error: function (xhr, status, error) {
                console.log("Error occurred during AJAX call:", error);
            }
        });


        } catch (e) {
        console.log('Exception from User method Name is SaveUser() :--->' + e);
        }
    }

function closePopup() {
    document.getElementById('popup').style.display = 'none';
}

    function ResetData() {
        debugger;
    $('#txtname').val('');
    $('#txtemailid').val('');
    $('#txtmobileno').val('');
    $('#txtmessage').val('');
    }

