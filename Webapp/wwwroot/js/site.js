// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Initialiser DataTable pour les tableaux avec l'id equipementsTable
    $('#equipementsTable').DataTable();
});


function showSuccessMessage(title = "Succès!", message = "Opération réussie!", timer = 3000) {
    Swal.fire({
        title: title,
        text: message,
        icon: "success",
        timer: timer,
        showConfirmButton: false,
        toast: true,
        position: 'top-end'
    });
}


function showErrorMessage(title = "Erreur!", message = "Une erreur est survenue!", timer = 5000) {
    Swal.fire({
        title: title,
        text: message,
        icon: "error",
        timer: timer,
        showConfirmButton: true,
        toast: true,
        position: 'top-end'
    });
}