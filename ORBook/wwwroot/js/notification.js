const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connection.on("ReceiveNotification", function (message) {
    alert("thông báo mới: " + message); // hoặc hiển thị ở giao diện theo cách bạn muốn
    //window.location.reload();
});

connection.start()
    .then(() => console.log("Kết nối SignalR thành công!"))
    .catch(function (err) {
        console.error("Kết nối SignalR thất bại: " + err.toString());
    });
