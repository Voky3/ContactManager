document.addEventListener('DOMContentLoaded', () => {
    const form = document.querySelector('form');
    if (!form) return;

    form.addEventListener('submit', e => {
        let valid = true;
        const firstName = form.querySelector('[name="FirstName"]');
        const lastName = form.querySelector('[name="LastName"]');
        const phone = form.querySelector('[name="Phone"]');
        const email = form.querySelector('[name="Email"]');

        if (!firstName.value.trim()) {
            alert("Neplatné křestní jméno.");
            firstName.focus();
            valid = false;
        } else if (!lastName.value.trim()) {
            alert("Neplatné příjmení.");
            firstName.focus();
            valid = false;
        } else if (!phone.value.trim()) {
            alert("Neplatné telefoní číslo.");
            firstName.focus();
            valid = false;
        }
        if (!valid) {
            e.preventDefault(); // stop form submission
        }
    });
});
