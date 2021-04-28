$(document).ready(function () {
	$('.login-but').click(function () {
		$("#login-or-registration").attr("action", "/User/Login");
	})

	$('.registration-but').click(function () {
		$("#login-or-registration").attr("action", "/User/Registration");
	})
});
