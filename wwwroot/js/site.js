// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Validar(event) {
  event.preventDefault();

  let password = document.getElementById("password").value.trim();
  let email = document.getElementById("email").value.trim();
  let usuario = document.getElementById("username").value.trim();
  let tieneMayuscula = /[A-Z]/.test(password);

  if (!tieneMayuscula && password.length < 4) {
    alert("⚠️ La contraseña debe tener al menos una letra mayúscula y 4 caracteres.");
    return false;
  }
  if (!tieneMayuscula) {
    alert("⚠️ La contraseña debe tener al menos una letra mayúscula.");
    return false;
  }
  if (password.length < 4) {
    alert("⚠️ La contraseña debe tener al menos 4 caracteres.");
    return false;
  }
  if (usuario === password) {
    alert("⚠️ La contraseña debe ser distinta al nombre de usuario.");
    return false;
  }

  if (!email) {
    alert("⚠️ Falta el correo electrónico.");
    return false;
  }

  document.querySelector("form").submit();
}
function abrirOverlay() {
    document.getElementById('tutorial-overlay').style.display = 'flex';
}
function cerrarOverlay() {
    document.getElementById('tutorial-overlay').style.display = 'none';
}