$(document).ready(function (){
    
    var time = 2500
;
    

    $('.next-btn').click(function(){

        $('.account-info-container.current').animate(
            {width : 0}, time
        )
        $('.account-info-container.next').animate(
            {width : 370}, time, '', goInitState(true)
        )
    });

    $('.prev-btn').click(function(){

        $('.account-info-container.current').animate(
            {width : 0}, time
        )
        $('.account-info-container.prev').animate(
            {width : 370}, time, '', goInitState(false)
        )
    });

    function goInitState(goForward){

        var copy = $('.account-info-container.template').clone();

        if(goForward){
            
            var copy = $('.account-info-container.prev').clone();
            
            copy.removeClass('prev');

            $('.account-info-container.prev').remove();

            $('.account-info-container.current').removeClass('current').addClass('prev');

            $('.account-info-container.next').removeClass('next').addClass('current');

            copy.addClass('next');

            copy.insertBefore('.account-info-container.current');
        }
        else{
              
        }

        /*$('.account-info-container.current').css(
            {width : 370}
        )
        $('.account-info-container.prev, .account-info-container.next').css(
            {width : 0}
        )*/
    }

});
