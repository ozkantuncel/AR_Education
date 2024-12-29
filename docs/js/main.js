document.addEventListener('DOMContentLoaded', function() {
    const downloadButton = document.getElementById('download-button');

    downloadButton.addEventListener('click', function() {
        // Yeni sayfaya yönlendir
        window.location.href = 'qr.html';
    });
});