$(function () {
    $('.editModal').modal();
});

function editProduct(productId) {
    $.ajax({
        url: '/Apps/Add/' + productId, // The method name + paramater
        success: function (data) {
            $('#modalWrapper').html(data); // This should be an empty div where you can inject your new html (the partial view)
        }
    });
}