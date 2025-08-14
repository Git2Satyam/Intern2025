function updateQuantity(id, existQty, qty)
{
    if (id > 0 && qty > 0) {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: '/Cart/UpdateQuantity/' + id + "/" + qty,
            success: function (data) {
                if (data > 0) {
                    location.reload();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                // Handle error
                console.error('AJAX Error:', textStatus, errorThrown);
                console.error('jqXHR:', jqXHR);
                alert('An error occurred: ' + textStatus);
            }
        })
    }
    else if (id > 0 && existQty > 1 && qty > 0)
    {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: '/Cart/UpdateQuantity/' + id + "/" + qty,
            success: function (data) {
                if (data > 0) {
                    location.reload();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                // Handle error
                console.error('AJAX Error:', textStatus, errorThrown);
                console.error('jqXHR:', jqXHR);
                alert('An error occurred: ' + textStatus);
            }


        })
    }
}