$(document).ready(function () {
	$('.popup-cover').click(function () {
		$('.nice-popup').addClass('hide');
	});

	$('.log-in').click(function () {
		$('.nice-popup').removeClass('hide');
	});

	$('.socials-popup-cover').click(function () {
		$('.socials-popup').addClass('hide');
	});

	$('.footer-icon.telegram.tmsgroup').click(function () {
		$('.socials-popup').removeClass('hide');
		$('.socials-block.youtube').addClass('hide');
		$('.socials-block.telegram').removeClass('hide');
	});

	$('.footer-icon.youtube.teacher').click(function () {
		$('.socials-popup').removeClass('hide');
		$('.socials-block.telegram').addClass('hide');
		$('.socials-block.youtube').removeClass('hide');
	});
});
