function confirmDelete(beneficiaryId) {
    Swal.fire({
        title: '¿Estás seguro que quieres eliminar al beneficiario?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById('deleteForm_' + beneficiaryId).submit();
        }
    });
}

function confirmPayment() {
    var accountNumber = document.getElementById('accountNumber').value;
    var paymentAmount = document.getElementById('paymentAmount').value;
    var originAccount = document.getElementById('originAccount').value;

    if (!accountNumber || !paymentAmount || !originAccount) {
        Swal.fire({
            title: 'Error',
            text: 'Todos los campos son requeridos.',
            icon: 'error',
            confirmButtonColor: '#3085d6',
        });
        return;
    }

    fetch(`/Express/GetAccountOwnerName?accountNumber=${originAccount}`)
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                const ownerName = data.ownerName;
                
                Swal.fire({
                    title: `¿Estás seguro de realizar este pago?`,
                    text: `Estás a punto de transferir $${paymentAmount} a la cuenta ${accountNumber} perteneciente a ${ownerName}.`,
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, realizar pago',
                    cancelButtonText: 'Cancelar',
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                }).then((result) => {
                    if (result.isConfirmed) {
                        document.getElementById('paymentForm').submit();
                    }
                });
            } else {
                Swal.fire({
                    title: 'Error',
                    text: data.message,
                    icon: 'error',
                    confirmButtonColor: '#3085d6',
                });
            }
        })
        .catch(error => {
            Swal.fire({
                title: 'Error',
                text: 'Ocurrió un error al obtener la información del dueño de la cuenta.',
                icon: 'error',
                confirmButtonColor: '#3085d6',
            });
        });
}



function confirmPayCredit() {
    Swal.fire({
        title: '¿Estás seguro de realizar este pago?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, realizar pago',
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
    }).then((result) => {
        if (result.isConfirmed) {
            // Envía el formulario si el usuario confirma
            document.getElementById("creditCardPaymentForm").submit();
        }
    });
}

function confirmPayLoan() {
    Swal.fire({
        title: '¿Estás seguro de realizar este pago?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, realizar pago',
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
    }).then((result) => {
        if (result.isConfirmed) {
            // Envía el formulario si el usuario confirma
            document.getElementById("paymentForm").submit();
        }
    });
}

