/*!
    * Start Bootstrap - SB Admin v7.0.7 (https://startbootstrap.com/template/sb-admin)
    * Copyright 2013-2023 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-sb-admin/blob/master/LICENSE)
    */
    // 
// Scripts
// 
window.addEventListener('DOMContentLoaded', event => {
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    const body = document.body;

    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            body.classList.toggle('sb-sidenav-toggled');

            // Always add the class, regardless of the toggle state
            body.classList.add('sb-nav-fixed');

            localStorage.setItem('sb|sidebar-toggle', body.classList.contains('sb-sidenav-toggled'));
        });
    }
});

var maskHtml = "<div class='all-mask'></div>";
function slider_Open(SliderId) {
    $('#' + SliderId).show();
    $('#' + SliderId).animate({ 'right': 0 });
    $('body').append(maskHtml);
    $('body').css('overflow', 'hidden');
    $('.all-mask').not(':first').remove();
}
/*-- dynamic slider open end --*/

/*-- dynamic slider close start --*/
function slider_Close(SliderId) {
    sliderWidth = $('#' + SliderId).width();
    $('#' + SliderId).animate({ 'right': -sliderWidth });
    $('.all-mask').remove();
    $('body').css('overflow', 'auto');
    $(".rform-wrapper, .rform-inner, .pad-box-gray, .add-body-scroll").animate({ scrollTop: 0 }, "slow");
}
