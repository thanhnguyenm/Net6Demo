// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function(){

    $('.sidenav-trigger,#mobile-demo').click(function () {
        if ($('#mobile-demo').hasClass('sidenav-fixed')) {
            $('#mobile-demo').removeClass('sidenav-fixed');
        } else {
            $('#mobile-demo').addClass('sidenav-fixed')
        }
    })

});