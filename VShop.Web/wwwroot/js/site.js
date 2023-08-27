// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



// Adicionar

$(document).ready(function () {
    var storedCount = localStorage.getItem('carrinhoCount');
    count = storedCount ? parseInt(storedCount) : 0;
    carrinhoIndex.innerHTML = count;
});


function acrescentar() {
    count++;
    carrinhoIndex.innerHTML = count;

    // Armazenar o valor no localStorage
    localStorage.setItem('carrinhoCount', count);
}

function remover() {
    count--;
    carrinhoIndex.InnerHTML = count;

    localStorage.setItem('carrinhoCount', count);
}


