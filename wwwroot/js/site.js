// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ValidarContraseña()
{
  event.preventDefault();

  let password = document.getElementById("password").value;
  let tieneMayuscula = /[A-Z]/.test(password);

  if (password.length < 4 && !tieneMayuscula) {
    alert("⚠️ La contraseña debe tener al menos una letra mayúscula y al menos 4 caracteres.");
    return false;
  }else if (!tieneMayuscula) {
    alert("⚠️ La contraseña debe tener al menos una letra mayúscula.");
    return false;
  }else if (password.length < 4){
    alert("⚠️ La contraseña debe tener al menos 4 caracteres.");
    return false;
  }

  document.querySelector("form").submit();

}