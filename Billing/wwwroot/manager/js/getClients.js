$(document).ready(function() {
	$("#search").keyup(function(){
		_this = this;
		$.each($(".table tbody tr"), function() {
			var rowText = $(this).find(".searchable").text();
			if(rowText.toLowerCase().indexOf($(_this).val().toLowerCase()) === -1)
				$(this).fadeOut("slow");
			else
				$(this).fadeIn("slow");
		});
	});
});