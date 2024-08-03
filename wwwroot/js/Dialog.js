function onDelete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'delete',
                success: function (data) {
                    if (data.success) {
                        location.reload();
                    } else {
                        Swal.fire({
                            title: "Deleted! fail",
                            text: "Your file not has been deleted.",
                            icon: "error"
                        });
                    }
                   

                }
            })
        }
    });
 
}


function conformationLogOut(url) {
    Swal.fire({
        title: "Are you sure LogOut?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, LogOut it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'delete',
                success: function (data) {
                    if (data.success) {
                        location.reload();
                    } else {
                        Swal.fire({
                            title: "LogOut! fail",
                            icon: "error"
                        });
                    }


                }
            })
        }
    });
}