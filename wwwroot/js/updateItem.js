/*
    Actualiza el objeto.
    Pilla los datos de los inouts i los envia en JSON a: /Home/Update
    El controllador recoge los datos, convierta a INotHuman y actualiza en bd.
*/

editFormUpdateItemBtn.addEventListener("click", () => {
  updateObject();
});

// UPDATE
let updateObject = () => {
  // recolecta Datos
  const id = editItemForm.dataset.itemId;
  const tableName = editItemForm.dataset.itemTable;
  const name = document.getElementById("edit-name").value;
  let type = "";
  if (tableName === "Robots") {
    type = document.getElementById("edit-type-robot").value;
  } else if (tableName === "Androids") {
    type = document.getElementById("edit-type-android").value;
  }
  const price = document.getElementById("edit-price").value;

  const data = {
    //JSON
    tableName: tableName,
    id: id,
    name: name,
    type: type,
    price: price,
  };

  fetch("/Home/Update", {
    // HttpRequest
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })
    // diferentes escenarios.
    .then((response) => {
      if (response.ok) {
        window.location.href = "/Home/";
        window.location.reload();
        let message = `${name} ha sido actualizado en ${tableName}`;
        updateMessageLogger(message, "success");
      } else {
        let message = `${name} no ha podido ser actualizado en ${tableName}`;
        updateMessageLogger(message, "error");
        throw new Error("Error al actualizar.");
      }
    })
    .catch((error) => {
      console.error("Error:", error);
    });
};
