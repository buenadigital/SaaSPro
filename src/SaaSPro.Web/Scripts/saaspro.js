(function ($) {

    $(".date-picker").datepicker({
 
    });

    $(".hijax").on("click", function (e) {
        e.preventDefault();

        var $that = $(this);

        $.ajax({
            url: this.href,
            type: "post",
            success: function () {
                $that.parents($that.data("parent")).fadeOut(200, function () { $(this).remove(); });
            }
        });
    });

    // give optional select lists a placeholder
    $("select.placeholder").change(function () {
        if ($(this).prop("selectedIndex") > 0) {
            $(this).addClass("placeholder-changed");
        } else {
            $(this).removeClass("placeholder-changed");
        }
    })
    .change();

})(jQuery);