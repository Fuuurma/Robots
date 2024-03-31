/*
    Establece la categoria selecionada en base a los botones de Robot y Androide
    Cambia los hovers y los colores acorde a su categoria.
*/

const robotBtn = document.getElementById("robot-selector");
const androidBtn = document.getElementById("android-selector");
const typeSelector = document.getElementById("selected-type");
const addItemBtn = document.getElementById("add-object");
let selectedType = typeSelector.dataset.selectedType;

const actionResultLogger = document.getElementById("action-result-logger");
const actionResultLoggerMessage = document.getElementById(
  "action-result-message"
);

robotBtn.addEventListener("click", () => {
  updateSelectedType(selectedType, "robot");
  changeSelector(robotBtn);
  updateHovers(selectedType);
});

androidBtn.addEventListener("click", () => {
  updateSelectedType(selectedType, "android");
  changeSelector(androidBtn);
  updateHovers(selectedType);
});

let updateSelectedType = (oldType, newType) => {
  if (oldType === newType) {
    console.log(`Mismo tipo: ${oldType}`);
  } else {
    selectedType = newType;
  }
};

let changeSelector = (btn) => {
  const selectedButton = document.querySelector(".selected");
  if (selectedButton) {
    selectedButton.classList.remove("selected");
  }
  btn.classList.add("selected");
};

let updateHovers = (newType) => {
  addItemBtn.className = `hover-${newType}`;
};
