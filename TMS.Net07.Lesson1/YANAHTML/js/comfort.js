$(document).ready(function (){
	$('.log').click(function(){
		$(this).hide();
		$('.login-block').hide();
	})

	$('.log-in').click(function(){
		$('.log').show();
		$('.login-block').show();
	})
})