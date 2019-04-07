$(document).ready(function() {
	$("#search").keyup(function() {
		_this = this;
		$.each($(".table tbody tr"), function() {
			var rowText = $(this).find(".searchable").text();
			if(rowText.toLowerCase().indexOf($(_this).val().toLowerCase()) === -1)
				$(this).fadeOut("slow");
			else
				$(this).fadeIn("slow");
		});
	});
	
	$(".delete-favour").click(function () {
		var currentRow = $(this).closest('tr');
		var favourId = currentRow.children('td:first').text();
		$.ajax({
			type: 'DELETE',
			url: '/Manager/DeleteFavour',
			data: {
				id: favourId
			},
			success: function (result) {
				if (result.error) {
					alert("Произошла ошибка: " + result.message);
				}
				currentRow.remove();
			},
			error: function() {
				alert("Произошла ошибка.");
			}
		});
	})
});