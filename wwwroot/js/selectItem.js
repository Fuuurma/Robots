/*
    Cambia el item seleccionado. 
    Al darle al botón del ojo. Aparece ese Item en la parte superior.
    Se pasan sus datos para poder utilizarlos en el edit y delete.

*/

const seeObjectBtns = document.querySelectorAll(".see-object");

const selectedNameContainer = document.querySelector(
  ".selected-name-container"
);

const selectedTypeIcon = document.getElementById("selected-type-icon");

const deleteItemBtn = document.getElementById("delete-object");

const modalCreateBtn = document.getElementById("edit-item-create");
const modalUpdateBtn = document.getElementById("edit-item-update");
const modalResetBtn = document.getElementById("edit-item-reset");

const handleSeeObjectClick = (e) => {
  const clickedBtn = e.target;

  const clickedIcon = clickedBtn.querySelector("i");

  // Cambia el icono del ojo
  seeObjectBtns.forEach((btn) => {
    const icon = btn.querySelector("i");
    icon.classList.remove("fa-eye");
    icon.classList.add("fa-eye-slash");
  });

  clickedIcon.classList.remove("fa-eye-slash");
  clickedIcon.classList.add("fa-eye");

  // Pilla los datos del objeto y los pasa para el modal
  const itemId = clickedBtn.dataset.itemId;
  const itemName = clickedBtn.dataset.itemName;
  const itemType = clickedBtn.dataset.itemType;
  const itemCategory = clickedBtn.dataset.itemCategory;
  const itemPrice = clickedBtn.dataset.itemPrice;
  const itemTable = clickedBtn.dataset.itemTable;

  changeItemSelected(
    itemId,
    itemName,
    itemType,
    itemCategory,
    itemPrice,
    itemTable
  );
};

seeObjectBtns.forEach((btn) => {
  btn.addEventListener("click", handleSeeObjectClick);
});

const selectedObjectContainer = document.getElementById(
  "selected-object-container"
);
const objectName = document.getElementById("selected-name");

// pasa los datos al selector y al modal
function changeItemSelected(
  itemId,
  itemName,
  itemType,
  itemCategory,
  itemPrice,
  itemTable
) {
  selectedObjectContainer.dataset.itemSelectedId = editItemForm.dataset.itemId =
    itemId;

  selectedObjectContainer.dataset.itemSelectedType =
    editItemForm.dataset.itemType = itemType;

  selectedObjectContainer.dataset.itemSelectedName =
    editItemForm.dataset.itemName =
    objectName.textContent =
      itemName;

  selectedObjectContainer.dataset.itemSelectedCategory =
    editItemForm.dataset.itemCategory = itemCategory;

  selectedObjectContainer.dataset.itemSelectedPrice =
    editItemForm.dataset.itemPrice = itemPrice;

  selectedObjectContainer.dataset.itemSelectedTable =
    editItemForm.dataset.itemTable = itemTable;

  itemCategory === "Robot"
    ? (selectedNameContainer.style.background = "var(--robot-gradient)")
    : (selectedNameContainer.style.background = "var(--android-gradient)");

  itemCategory === "Robot"
    ? (selectedTypeIcon.className = "fa fa-robot")
    : (selectedTypeIcon.className = "fa fa-user");

  itemCategory === "Robot"
    ? (editItemBtn.className = "hover-robot")
    : (editItemBtn.className = "hover-android");

  itemCategory === "Robot"
    ? (deleteItemBtn.className = "hover-robot")
    : (deleteItemBtn.className = "hover-android");

  itemCategory === "Robot"
    ? (modalCreateBtn.className = "hover-robot")
    : (modalCreateBtn.className = "hover-android");

  itemCategory === "Robot"
    ? (modalUpdateBtn.className = "hover-robot")
    : (modalUpdateBtn.className = "hover-android");

  itemCategory === "Robot"
    ? (modalResetBtn.className = "hover-robot")
    : (modalResetBtn.className = "hover-android");
}
