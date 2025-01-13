// contactManager.js
class ContactManager {
    constructor() {
        this.initializeDataTable();
        this.bindEvents();

        // Make deleteContact globally available
        window.deleteContact = this.deleteContact;
    }

    initializeDataTable() {
        this.table = $('#contactsTable').DataTable();
    }

    bindEvents() {
        // Handle inline editing
        $('[contenteditable="true"]').on('blur', (e) => this.handleInlineEdit(e));

        // Handle checkbox editing
        $('input[type="checkbox"]').on('change', (e) => this.handleCheckboxEdit(e));
    }

    handleInlineEdit(event) {
        const row = $(event.target).closest('tr');
        const id = row.data('id');

        try {
            const contact = this.collectRowData(row);
            this.updateContact(contact);
        } catch (error) {
            console.error('Error preparing data:', error);
            alert('Error preparing data. Please check the format of all fields.');
        }
    }

    handleCheckboxEdit(event) {
        const row = $(event.target).closest('tr');
        try {
            const contact = this.collectRowData(row);
            this.updateContact(contact);
        } catch (error) {
            console.error('Error preparing data:', error);
            alert('Error preparing data. Please check the format of all fields.');
        }
    }

    collectRowData(row) {
        // Parse date
        const dateStr = row.find('[data-column="DateOfBirth"]').text().trim();
        const dateParts = dateStr.split('.');
        const formattedDate = `${dateParts[2]}-${dateParts[1]}-${dateParts[0]}`; // Convert to yyyy-MM-dd

        // Parse salary
        let salaryStr = row.find('[data-column="Salary"]').text().trim();
        salaryStr = salaryStr.replace(',', '.');  // Заміна коми на точку
        const salary = parseFloat(salaryStr.replace(/[^0-9.-]+/g, ''));
        return {
            id: parseInt(row.data('id')),
            Name: row.find('[data-column="Name"]').text().trim(),
            DateOfBirth: formattedDate,
            Married: row.find('input[type="checkbox"]').is(':checked'),
            Phone: row.find('[data-column="Phone"]').text().trim(),
            Salary: salary || 0
        };
    }

    updateContact(contact) {
        console.log('Sending update:', contact);

        fetch('/Contact/UpdateContact', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(contact)
        })
            .then(async response => {
                if (!response.ok) {
                    const errorText = await response.text();
                    console.error('Server error response:', errorText);
                    throw new Error(`Failed to update: ${errorText}`);
                }
                return response.json().catch(() => ({}));
            })
            .then(() => {
                $(`tr[data-id='${contact.id}']`).attr('data-modified', 'false');
            })
            .catch(error => {
                console.error('Update error:', error);
                alert('Error updating contact: ' + error.message);
            });
    }

    deleteContact(id) {
        if (confirm('Are you sure you want to delete this contact?')) {
            fetch(`/Contact/DeleteContact?id=${id}`, {
                method: 'POST'
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Failed to delete');
                    }
                    $('#contactsTable').DataTable()
                        .row($(`tr[data-id='${id}']`))
                        .remove()
                        .draw();
                })
                .catch(error => {
                    console.error('Delete error:', error);
                    alert('Error deleting contact. Please try again.');
                });
        }
    }
}

$(document).ready(() => {
    window.contactManager = new ContactManager();
});