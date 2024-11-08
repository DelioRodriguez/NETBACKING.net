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