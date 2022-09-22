"use strict";

$(document).ready(function () {

    // Header 

    $(document).on("click", ".mobile-nav", function () {
        $(".mobile-menu").slideToggle();
    });
    $(document).on("click", ".sub-menu-mobile-li", function () {
        $(this).children('ul').slideToggle();
        $(this).siblings('.sub-menu-mobile-li').children('ul').slideUp();

        // $(".sub-menu-mobile-li ul").hide();
        // $(this).next().slideToggle();
    });


    //   End Header

    //Start Tabs

    $(document).on("click", ".tabs li", function () {
        $('.tabs li.active').removeClass("active");
        $(this).addClass("active");

        TabContentChanger();
    })

    function TabContentChanger() {
        $('.tab_content.active').removeClass("active");

        const index = $('.tabs li.active').attr("data-index");
        const connectedTab = $(`.tab_content[data-index="${index}"]`);
        connectedTab.addClass("active");
    }

    //End Tabs

    //Gallery Section 

    $(document).on("click", ".photo-overlay", function () {
        var sourceGallery = $(this).next().attr("src");

        $(".inner-slide img").attr("src", sourceGallery);
        $(".inner-slider").css("display", "block");

    });

    $(document).on("click", ".close", function () {

        $(".inner-slider").css("display", "none");

    });


    // lang dropdown
    $('.lang-dropdown').click(function () {
        $('.lang-unordered-list').toggleClass('block');
    });

});