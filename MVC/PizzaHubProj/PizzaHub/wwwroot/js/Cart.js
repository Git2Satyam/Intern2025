function updateQuantity(id, existQty, qty)
{
    if (id > 0 && qty > 0) {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: '/Cart/UpdateQuantity?productId=' + id + '&qty=' + qty,
            success: function (data) {
                if (data > 0) {
                    location.reload();
                }
            },
            //error: function (result) { }

            error: function (xhr, status, error) {
                console.error("AJAX Error:", status, error);
                alert("Error updating quantity: " + xhr.responseText || error);
            }
        })
    }
    else if (id > 0 && existQty > 1 && qty < 0)
    {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: '/Cart/UpdateQuantity?productId=' + id + '&qty=' + qty,
            success: function (data) {
                if (data > 0) {
                    location.reload();
                }
            },
            //error: function (result) { }
            error: function (xhr, status, error) {
                console.error("AJAX Error:", status, error);
                alert("Error updating quantity: " + xhr.responseText || error);
            }
        })
    }
}