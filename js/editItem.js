/*     
    EDIT     
    Abre el modal de editar objecto.
    Pasa los datos necesarios. 
    Tambien lleva la logica para crear un objeto desde ese modal.
*/

const editItemBtn = document.getElementById("edit-object");
const editItemModal = document.getElementById("edit-object-modal");
const editCloseBtn = document.querySelector("#edit-object-modal .close");
const editItemForm = document.getElementById("edit-object-form");

const editItemTittle = document.getElementById("edit-item-tittle");
const editCategorySelectorRobot = document.getElementById("edit-type-robot");
const editCategorySelectorAndroid =
  document.getElementById("edit-type-android");
const editPriceInput = document.getElementById("edit-price");
const editNameInput = document.getElementById("edit-name");
const editFormCreateItemBtn = document.getElementById("edit-item-create");
const editFormUpdateItemBtn = document.getElementById("edit-item-update");
const editFormResetItemBtn = document.getElementById("edit-item-reset");

editItemBtn.addEventListener("click", () => {
  let category = selectedObjectContainer.dataset.itemSelectedCategory;
  showEditItemModal(category);
});

let showEditItemModal = (category) => {
  // Muestra / Oculta el modal y pasa los datos
  editItemModal.style.display = "block";

  editCloseBtn.onclick = function () {
    editItemModal.style.display = "none";
  };

  window.onclick = function (event) {
    if (event.target === editItemModal) {
      editItemModal.style.display = "none";
    }
  };

  let name = editItemForm.dataset.itemName;
  editItemTittle.textContent = `Editar ${name}`;
  editNameInput.placeholder = name;
  editPriceInput.placeholder = editItemForm.dataset.itemPrice;

  switch (category) {
    case "Robot":
      editItemTittle.parentElement.style.background = "var(--robot-gradient)";
      editCategorySelectorRobot.style.display = "inline";
      editCategorySelectorAndroid.style.display = "none";
      editCategorySelectorRobot.placeholder = editItemForm.dataset.itemType;
      break;

    case "Android":
      editItemTittle.parentElement.style.background = "var(--android-gradient)";
      editCategorySelectorRobot.style.display = "none";
      editCategorySelectorAndroid.style.display = "inline";
      editCategorySelectorAndroid.placeholder = editItemForm.dataset.itemType;
      break;

    default:
      console.log("Categoria no valida: " + category);
      break;
  }
};

// Crear Item.
editFormCreateItemBtn.addEventListener("click", () => {
  createItem();
});

let createItem = () => {
  const itemId = editItemForm.dataset.itemId;
  const tableName = editItemForm.dataset.itemTable;
  const itemName = document.getElementById("edit-name").value;
  let itemType, itemCategory, itemDescCategory;

  if (tableName === "Robots") {
    itemType = document.getElementById("edit-type-robot").value;
    itemCategory = 1;
    itemDescCategory = "Robot";
  } else if (tableName === "Androids") {
    itemType = document.getElementById("edit-type-android").value;
    itemCategory = 2;
    itemDescCategory = "Android";
  }
  const itemPrice = document.getElementById("edit-price").value;

  const data = {
    tableName: tableName,
    name: itemName,
    type: itemType,
    category: itemCategory,
    descCategory: itemDescCategory,
    price: parseFloat(itemPrice),
  };
  fetch("/Home/Create", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })
    .then((response) => {
      if (response.ok) {
        let message = `${itemName} ha sido creado en ${tableName}`;
        updateMessageLogger(message, "success");
        window.location.reload();
        return response.json();
      } else {
        let message = `No ha sido posible crear a ${itemName} en ${tableName}`;
        updateMessageLogger(message, "error");
        throw new Error("No ha sido posible crear el objeto.");
      }
    })
    .then((data) => {
      console.log("Datos añaidods:", data);
      window.location.reload();
    })
    .catch((error) => {
      console.error("Error al añadir item:", error);
    });
  // window.location.href = "/Home/";
  // window.location.reload();
};
