/*
     DELETE
     Hace una Request a/Home/Delete con los params de su tabla y id
     Despues actualiza el FE, asi no hace falta hacer refresh.
     Curiosamente el MessageLogger siempre funciona bien aqui. (el unico)
*/

document.addEventListener("click", function (event) {
  if (event.target && event.target.id === "delete-object") {
    let objectId = selectedObjectContainer.dataset.itemSelectedId;
    let tableName = selectedObjectContainer.dataset.itemSelectedTable;

    fetch(`/Home/Delete?tableName=${tableName}&id=${objectId}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((response) => {
        if (!response.ok) {
          let message = `El objeto con id ${objectId} no ha podido ser eliminado de ${tableName}`;
          updateMessageLogger(message, "error");
          throw new Error("No se ha podido eliminar.");
        }
        return response.status;
      })
      .then((status) => {
        if (status === 200) {
          // actualiza UI
          const deletedItem =
            tableName == "Robots"
              ? document.getElementById(`R-${objectId}`)
              : document.getElementById(`A-${objectId}`);
          let message = `El objeto con id ${objectId} ha sido eliminado de ${tableName}`;
          updateMessageLogger(message, "success");
          if (deletedItem) {
            deletedItem.remove();
          }
        }
      })
      .catch((error) => {
        console.error("Error en la request:", error);
      });
    // window.location.href = "/Home/";
    // window.location.reload();
  }
});
