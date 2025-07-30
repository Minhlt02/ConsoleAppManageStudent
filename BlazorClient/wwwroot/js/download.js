<script>
window.downloadFileFromBytes = (fileName, byteBase64) => {
    const link = document.createElement('a');
    link.download = fileName;
    const blob = new Blob([Uint8Array.from(atob(byteBase64), c => c.charCodeAt(0))], {type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
    link.href = URL.createObjectURL(blob);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};
</script>
