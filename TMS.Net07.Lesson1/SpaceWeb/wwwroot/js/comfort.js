$(document).ready(function (){
	// $('.log').click(function(){
	// 	$(this).hide();
	// 	$('.login-block').hide();
	// })
	//
	// $('.log-in').click(function(){
	// 	$('.log').show();
	// 	$('.login-block').show();
	// })
	$('.log').click(function(){
		$(this).addClass('hide');
		$('.login-block').addClass('hide');
	})

	$('.log-in').click(function(){
		$('.log').removeClass('hide');
		$('.login-block').removeClass('hide');
	})
})