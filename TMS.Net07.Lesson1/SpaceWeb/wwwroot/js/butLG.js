$(document).ready(function () {
	$('.but1').click(function () {
		$("#login-or-registration").attr("action", "/User/Login");
	})

	$('.but2').click(function () {
		$("#login-or-registration").attr("action", "/User/Registration");
	})
});
