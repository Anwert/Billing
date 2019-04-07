$(document).ready(function() {
	var previousStatusId;
	var currentDropBox;
	$(".status").on('focus', function () {
		currentDropBox = this;
		previousStatusId = this.value;
	}).change(function() {
		var newStatusId = this.value;
		var currentRow = $(this).closest('tr');
		var contractId = currentRow.children('td:first').text();
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
					currentDropBox.value = previousStatusId;
				} else {
					currentRow.removeClass();
					switch (newStatusId) {
						case "2":
							currentRow.addClass("table-success");
							break;
						case "3":
							currentRow.addClass("table-danger");
							break;
						default:
							currentRow.addClass("table-warning");
							break;
					}
				}
			},
			error: function() {
				alert("Произошла ошибка.");
				currentDropBox.value = previousStatusId;
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