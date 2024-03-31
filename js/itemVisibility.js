/*
  Permite  ocultar  y enseños los dataContainers de Robots y Androides, 
  en base al type seleccionado y activo. 
  Actualiza el color del botón también.
*/

const showObjectBtn = document.getElementById("show-object");

const robotDataContainer = document.querySelector(".robot-data-container");
const androidDataContainer = document.querySelector(".android-data-container");

showObjectBtn.addEventListener("click", () => {
  changeSeeItemsColors(selectedType, showObjectBtn);
  changeDataContainersVisibility(selectedType);
});

// 4 estados posibles. robots, androides, robots y androides y ninguno.
let changeSeeItemsColors = (selectedType, btn) => {
  if (selectedType === "robot") {
    if (btn.classList.contains("show-robot")) {
      btn.classList.remove("show-robot");
    } else if (btn.classList.contains("show-android")) {
      btn.classList.add("show-robot");
    } else if (
      btn.classList.contains("show-robot") &&
      btn.classList.contains("show-android")
    ) {
      btn.classList.remove("show-robot");
    } else if (!btn.classList.contains("")) {
      btn.classList.add("show-robot");
    }
  } else if (selectedType === "android") {
    if (btn.classList.contains("show-android")) {
      btn.classList.remove("show-android");
    } else if (btn.classList.contains("show-robot")) {
      btn.classList.add("show-android");
    } else if (
      btn.classList.contains("show-robot") &&
      btn.classList.contains("show-android")
    ) {
      btn.classList.remove("show-android");
    } else if (!btn.classList.contains("")) {
      btn.classList.add("show-android");
    }
  }
};

let changeDataContainersVisibility = (selectedType) => {
  if (selectedType === "robot") {
    if (robotDataContainer.classList.contains("no-show")) {
      robotDataContainer.classList.remove("no-show");
    } else {
      robotDataContainer.classList.add("no-show");
    }
  } else if (selectedType === "android") {
    if (androidDataContainer.classList.contains("no-show")) {
      androidDataContainer.classList.remove("no-show");
    } else {
      androidDataContainer.classList.add("no-show");
    }
  }
};
