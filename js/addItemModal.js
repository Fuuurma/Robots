/*
  Enseña / Oculta el modal para añadir un objeto.
  Y pasa los datos necesarios para añadirlo luego.
*/

const addItemModal = document.getElementById("add-object-modal");
const closeModalBtn = document.querySelector("#add-object-modal .close");
const objectForm = document.getElementById("add-object-form");

const addItemTittle = document.getElementById("add-item-form-tittle");
const categorySelectorRobot = document.getElementById("type-robot");
const categorySelectorAndroid = document.getElementById("type-android");
const addItemNameInput = document.getElementById("name");

const createItemCreateBtn = document.getElementById("create-item-create-btn");
const createItemUpdateBtn = document.getElementById("create-item-update-btn");
const createItemResetBtn = document.getElementById("create-item-reset-btn");

addItemBtn.addEventListener("click", () => {
  showAddItemModal(selectedType);
});

let showAddItemModal = (type) => {
  addItemModal.style.display = "block";
  addItemModal.dataset.objectType = type;
  let tableName;

  switch (type) {
    case "robot":
      tableName = "Robots";
      addItemTittle.textContent = "Nuevo Robot";
      addItemTittle.parentElement.style.background = "var(--robot-gradient)";
      categorySelectorRobot.style.display = "inline";
      categorySelectorAndroid.style.display = "none";
      let robotName = suggestName("robot");
      addItemNameInput.value = robotName;
      addItemNameInput.placeholder = robotName;
      createItemCreateBtn.className = "hover-robot";
      break;

    case "android":
      tableName = "Androids";
      addItemTittle.textContent = "Nuevo Androide";
      addItemTittle.parentElement.style.background = "var(--android-gradient)";
      categorySelectorRobot.style.display = "none";
      categorySelectorAndroid.style.display = "inline";
      let androidName = suggestName("android");
      addItemNameInput.value = androidName;
      addItemNameInput.placeholder = androidName;
      createItemCreateBtn.className = "hover-android";
      break;

    default:
      break;
  }
  // deshabilitados
  createItemUpdateBtn.style.backgroundColor = "var(--darker)";
  createItemUpdateBtn.style.color = "var(--neutral)";
  createItemUpdateBtn.style.pointerEvents = "none";
  createItemResetBtn.style.backgroundColor = "var(--darker)";
  createItemResetBtn.style.color = "var(--neutral)";
  createItemResetBtn.style.pointerEvents = "none";

  addItemModal.dataset.objectTable = tableName;

  closeModalBtn.addEventListener("click", closeAddItemModal);
  window.addEventListener("click", windowOnClick);
};

// Para cerrar el modal. Clickar X o fuera del Modal
let closeAddItemModal = () => {
  addItemModal.style.display = "none";
  closeModalBtn.removeEventListener("click", closeAddItemModal);
  window.removeEventListener("click", windowOnClick);
};

let windowOnClick = (event) => {
  if (event.target === addItemModal) {
    closeAddItemModal();
  }
};
