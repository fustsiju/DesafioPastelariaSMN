// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById('formularioLogin').addEventListener('focusin', function () {
    document.getElementById('back-imagem').classList.add('blur');
});

document.getElementById('formularioLogin').addEventListener('focusout', function () {
    document.getElementById('back-imagem').classList.remove('blur');
});
