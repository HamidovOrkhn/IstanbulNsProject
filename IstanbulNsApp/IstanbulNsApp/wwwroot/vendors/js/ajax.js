$(document).ready(function () {

    //order of categories on page
    $("#btn-category-order").click(function (e) {

        //category data
        let orders = [];

        for (let f of $(".ui-state-default")) {
            orders.push(parseInt($(f).attr("data-id")));
        }

        var data = { "orders": orders };

        console.log(orders);

        $.ajax({
            type: "POST",
            url: "/Admin/Menu/Order",
            data: data
        }).done(function () {
            location.reload();
        })

    })

    ////No Ajax, halls and rooms
    let totalPrice = 0;

    $('select').on('change', function () {
        let newFood = document.createElement('tr');
        let foodName = $(this).children("option:selected").text();
        if (this.value != "Seç" && !$("tbody").is(`:contains(${foodName})`)) {
            $(newFood).html(
                `<th scope="row">${foodName}</th>` +
                `<th scope="row" class="price-food">${this.value} ₼</th>` +
                `<th scope="row"><input data-inputId="${this.value}" class="form-control" type="number" name="say" value="1" min="1" max="99" /></th>` +
                `<th scope="row"><span class="totalFoodSpan" data-totalfood=${this.value}>${this.value} ₼</span></th>`+
                `<th scope="row"> <button class="btn btn-danger deleteFoodRow"> <i class="fas fa-trash-alt"> </i> </button> </th>`
            )
            $("tbody").append(newFood);
            onChangeInput();
            totalCalc();
            deleteFoodHandler();
        }
    });

    function onChangeInput() {
        $('th').on("change", "input.form-control", function () {
            let totalField = $(this).parent().siblings().slice(2,3);
            totalPrice = $(this).attr("data-inputId") * $(this).val();
            $(totalField).html(`<span class="totalFoodSpan" data-totalfood=${totalPrice}>${totalPrice} ₼</span>`);

            let subTotalPrice = 0;
            for (let f of $("span[data-totalfood]")) {
                subTotalPrice += parseFloat($(f).attr("data-totalfood"));
            }
            $("#SubTotalPrice").html(`Yekun Məbləğ : ${subTotalPrice} ₼`);
        })
    }

    function deleteFoodHandler() {
        $("button.deleteFoodRow").click(function () {
            $(this).parent().parent("tr").remove();
            totalCalc();
        })
    }

    function totalCalc() {
        let total = 0;
        if ($("span.totalFoodSpan").length != 0) {
            for (let f of $("span.totalFoodSpan")) {
                total += parseFloat($(f).attr("data-totalfood"));
            }
        }
        $("#SubTotalPrice").html(`Yekun Məbləğ : ${total} ₼`);
    }

    $(".exit-button").click(function () {
        window.history.back();
    })
})