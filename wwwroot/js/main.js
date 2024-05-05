const openFormButton = document.getElementById('openFormButton');
const popupForm = document.getElementById('popupForm');
const eventNameInput = document.getElementById('eventName');
const eventDateInput = document.getElementById('eventDate');
const eventCategoryInput = document.getElementById('eventCategory');

function clearForm() {
	eventNameInput.value = '';
	eventDateInput.value = '';
}

function sendDataAndReload(url, data) {
	fetch(url, {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(data)
	})
	.then(() => {
		window.location.reload();
	});
}

openFormButton.addEventListener('click', function () {
	if (popupForm.style.display === 'none') {
		document.getElementById('eventCategory').value = document.getElementById('category').value;
		popupForm.style.display = 'block';
	} else {
		popupForm.style.display = 'none';
		clearForm();
	}
});

document.getElementById('closeFormButton').addEventListener('click', function () {
	document.getElementById('addEvent').style.display = 'block';
	document.getElementById('editEvent').style.display = 'none';
	document.getElementById('deleteEvent').style.display = 'none';
	document.getElementById('popupForm').style.display = 'none';
	clearForm();
});

document.getElementById('addEvent').addEventListener('click', function () {
	const eventData = {
		Name: eventNameInput.value,
		Date: eventDateInput.value,
		Category: eventCategoryInput.value
	};
	sendDataAndReload('/Home/AddEvent', eventData);
});

document.getElementById('editEvent').addEventListener('click', function () {
	const eventId = document.getElementById('eventId').value;
	const eventData = {
		Id: eventId,
		Name: eventNameInput.value,
		Date: eventDateInput.value,
		Category: eventCategoryInput.value
	};
	sendDataAndReload('/Home/EditEvent', eventData);
});

document.getElementById('deleteEvent').addEventListener('click', function () {
	const eventId = document.getElementById('eventId').value;
	sendDataAndReload('/Home/DeleteEvent', eventId);
});