/*
    Muestra y oculta el modal de más información para el usuario.
    Tambien contiene la logica para el ResultLogger, pero no funciona siempre.
*/

const infoModalBtn = document.getElementById("info-modal-i");
const infoModal = document.getElementById("info-modal");
const closeButton = document.querySelector("#info-modal .close");

infoModalBtn.addEventListener("click", () => {
  toggleInfoModal();
});

const toggleInfoModal = () => {
  infoModal.classList.toggle("display-info-modal");
  closeButton.addEventListener("click", closeInfoModal);
};

let closeInfoModal = () => {
  infoModal.className = "";
};

// Hace aparecer un mensaje con Feedback para el usuario.
// No acaba de funcionar bien, creo que por las requests
//se pìerde en el flujo y nollega a llamarse.
let updateMessageLogger = (message, messageType) => {
  actionResultLogger.className = `${messageType} display-action-result`;
  actionResultLoggerMessage.textContent = message;

  setTimeout(() => {
    actionResultLogger.className = `${messageType} display-action-result`;
    window.location.href = "/Home/";
  }, 5000);
};
