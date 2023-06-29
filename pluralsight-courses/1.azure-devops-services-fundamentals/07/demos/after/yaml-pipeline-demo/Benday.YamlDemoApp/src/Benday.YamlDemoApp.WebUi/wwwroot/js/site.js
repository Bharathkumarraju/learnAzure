// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function handleRowClick(source) {
    var value = source.getAttribute('data-href');

    if (value) {
        window.location = value;
    } else {
        console.log('handleRowClick(): no data-href attribute');
    }
}