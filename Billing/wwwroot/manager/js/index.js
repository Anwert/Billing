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
				if (result.error) {
					alert("Произошла ошибка: " + result.message);
				}
			},
			failure: function(errMsg) {
				alert("Произошла ошибка: " + errMsg);
			}
		});
	});

	$("#search").keyup(function(){
		_this = this;
		$.each($(".table tbody tr"), function() {
			var rowText = $(this).find(".searchable").text();
			var currentRowStatusText = $(this).find(".status option:selected").text();
			rowText += currentRowStatusText;
			if(rowText.toLowerCase().indexOf($(_this).val().toLowerCase()) === -1)
				$(this).fadeOut("slow");
			else
				$(this).fadeIn("slow");
		});
	});
});