$(document).ready(function(){
	$('.user-photo').mouseleave(function () {
		$('.bg-chage-delete').animate({
			height: 0
		}, 500);
	})

	$('.user-photo').mouseenter(function () {
		$('.bg-chage-delete').animate({
			height: 50
		}, 500);
	})
})