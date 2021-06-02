$(document).ready(function () {
	$('.pointerhover').mouseleave(function () {
		$('.button-buy').find('.toRemoveHide').removeClass('hide1');
		$('.button-buy').find('.hide2').addClass('hide1');
	})

	$('.pointerhover').mouseenter(function () {
		$('.button-buy').find('.toRemoveHide').addClass('hide1');
		$('.hide2').removeClass('hide1');
	})
})