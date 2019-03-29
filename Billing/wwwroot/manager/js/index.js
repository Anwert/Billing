$(document).ready(function() {
	$(".status").change(function() {
		var newStatusId = this.value;
		var contractId = $(this).closest('tr').children('td:first').text();
		$.ajax({
			type: 'POST',
			url: '/Manager/UpdateStatusForContract',
			data: {
				new_status_id: newStatusId,
				contract_id: contractId
			},
			success: function (result) {
				if (!result.error) {
					alert("success vrode kak");
				} else {
					alert("Произошла ошибка: " + result.message);
				}
			},
			failure: function(errMsg) {
				alert("Произошла ошибка: " + errMsg);
			}
		});
	});
});