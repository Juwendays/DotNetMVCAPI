$.ajax({
    url: https://localhost:44364/api/Employee,
    type: "GET",
}).done(result => {
    console.log(error);
    $("#Employee").html(result);
});


$(document).ready(function () {
    $('#Employess').DataTable({
        dataSrc: "results",
        dataType: "JSON"
    },
        "columns": [
        {
            "data": null,
            "render": function (data, type, row, meta) {
                return meta.row + meta.settings._iDisplayStart + 1;
            }
        },
        {
            "data": "",
            "render": function (data, type, row) {
                return `${row.name}`;
            }
        },
        {
            "data": "",
            "render": function (data, type, row) {
                return `${row.url}`;
            }
        },
        {
            "data": "",
            "render": function (data, type, row, meta) {
                return `<button type="button" class="btn btn-warning btn-lg action" data-toggle="modal" data-id="${meta.row + meta.settings._iDisplayStart + 1}" data-target="#myModal">Detail</button>`;
            }
        },
        {
            "data": "",
            "render": function () {
                return `<button type="button" class="btn btn-primary btn-lg action" data-toggle="modal" data-id="$(#myModal2)" data-target="#myModal2">Form</button>`
            }
        }
    ]

    })
})