/// <reference path="../microsoft/signalr/dist/browser/signalr.js" />

$(() => {    

    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();
    connection.on("LoadNhanVien", function (/*NvId, Name, Position, IdDepartment*/) {
        LoadNhanVienData();
    })
    LoadNhanVienData();
    

    function LoadNhanVienData() {
        var tr = '';

        $.ajax({
            url: '/odata/NhanViens',
            method: 'GET',
            success: (result) => {
                $.each(result.value, (k, v) => {
                        tr += `<tr>
                        <td>${v.IdNv}</td>
                        <td>${v.Name}</td>
                        <td>${v.Position}</td>
                        <td>${v.IdDepartment}</td>
                        <td>
                        <a href='../NhanViens/Edit?id=${v.IdNv}'>Edit</a>
                        <a href='../NhanViens/Details?id=${v.IdNv}'>Details</a>
                        <a href='../NhanViens/Delete?id=${v.IdNv}'>Delete</a>
                        </td>
                    </tr>`;
                    console.log('Value: ' + v);
                    console.log('Key: ' + k);
                });
                $("#tableBody").html(tr);
               
            },
            error: (error) => {
                console.log('error');
            }
        });
    };
    //connection.invoke("LoadNhanVien", NvId, Name, Position, IdDepartment).catch(function (err) {
    //    return console.error(err.toString());
});