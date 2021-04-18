$(document).ready(function(){
	$('.user-photo').mouseout(function () {
		$('.bg-chage-delete').addClass('hide');
	})

	$('.user-photo').mouseover(function () {
		$('.bg-chage-delete').removeClass('hide');
	})
})