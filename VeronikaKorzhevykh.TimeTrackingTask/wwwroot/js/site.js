document.getElementById("clockOutBtn").disabled = true;

document.getElementById("clockInForm").addEventListener("submit", function (event) {
    document.getElementById("clockOutBtn").disabled = false;
});
