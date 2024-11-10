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
            document.getElementById('deleteForm' + beneficiaryId).submit();
        }
    });
}

function confirmPayment() {
    // Obtener los valores del formulario
    var accountNumber = document.getElementById('accountNumber').value;
    var paymentAmount = document.getElementById('paymentAmount').value;
    var originAccount = document.getElementById('originAccount').value;

    // Verificar si los campos están completos
    if (!accountNumber || !paymentAmount || !originAccount) {
        Swal.fire({
            title: 'Error',
            text: 'Todos los campos son requeridos.',
            icon: 'error',
            confirmButtonColor: '#3085d6',
        });
        return;
    }

    // Mostrar confirmación antes de realizar el pago
    Swal.fire({
        title: '¿Estás seguro de realizar este pago?',
        text: `Estás a punto de transferir $${paymentAmount} a la cuenta ${accountNumber}.`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, realizar pago',
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
    }).then((result) => {
        if (result.isConfirmed) {
            // Si el usuario confirma, enviar el formulario
            document.getElementById('paymentForm').submit();
        }
    });
}