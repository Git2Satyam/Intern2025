function updateQuantity(id, qty)
{
    if (id > 0 && qty > 0) {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: '/Cart/UpdateQuantity?productId=' + id + '&quantity=' + qty,
            success: function (data) {
                if (data > 0) {
                    location.reload();
                }
            },
            error: function (result) {
                alert(result);
            }
        })
    }
    else if (id > 0 && qty < 0) {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: '/Cart/UpdateQuantity?productId=' + id + '&quantity=' + qty,
            success: function (data) {
                if (data > 0) {
                    location.reload();
                }
            },
            error: function (result) {
                alert(result);
            }
        })
    }
}
