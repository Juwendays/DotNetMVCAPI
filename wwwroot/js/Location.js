$(document).ready(function){
    (#LocationTable).Datatable({
        orderCellsTop: true,
        fixedHeader: true,
        paging: true,
        lengthChange: true,
        searching: true,
        ordering: false,
        info: true,
        autoWidth: false,
        sLengthMenu: "Show _MENU_",
        order: [[1, 'asc']],
        lengthMenu: [[10, 25, 50, 100], [10, 25, 50, 100]],
        buttons: [
            {
                extend: 'csv',
                text: 'Copy all data',
                exportOptions: {
                    modifier: {
                        search: 'none'
                    }
                }
            }
        ],
        "ajax": {
            url: "https://localhost:44364/api/Location",
            type: "GET",
            dataSrc: "results",
            dataType: "JSON"
        },
        "columns": [
            {
                "data": null,
                "render": function (data, type, row) {
                    return `${row.id}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `${row.StreetAddress}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `${row.City}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    return `${row.Country}`;
                }
            },
        ]
    });
}
