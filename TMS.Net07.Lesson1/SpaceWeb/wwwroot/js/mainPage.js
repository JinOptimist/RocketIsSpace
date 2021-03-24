$(document).ready(function(){

        $('.settings-icon').click(function(){
            $('.settings-menu').toggleClass('hide');
        })

        $('.settings-picture-block img').click(function(){
            $(this).toggleClass('full-size');
        })

        $('[name=back-img]').change(function(){
            var val=$(this)
                .closest('.settings-picture-block')
                .find('img')
                .attr('src');
            $('body').css({"background":"url("+val+")",
                "background-size":"cover"});
        });

})