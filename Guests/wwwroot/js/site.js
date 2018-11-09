function updateUserState(id, state) {
	$.ajax({
		type: 'POST',
		url: '/Guest/UpdateState',
		contentType: 'application/json',
		data: JSON.stringify({
			userId: id,
			userState: state
		}),
		success: function () {
			location.reload();
		}
	});
}
