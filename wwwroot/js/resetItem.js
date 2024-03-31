/*  
  RESET
  Reseta de fabrica con valores vacíos el objeto. 
*/

document
  .getElementById("edit-item-reset")
  .addEventListener("click", function (event) {
    try {
      const itemId = editItemForm.dataset.itemId;
      const tableName = editItemForm.dataset.itemTable;

      const response = fetch(
        `/Home/Reset?tableName=${tableName}&id=${itemId}`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      if (!response.ok) {
        let message = `No ha sido posible resetear el item ${itemId}`;
        updateMessageLogger(message, "error");
        throw new Error(`No ha sido posible resetear el item ${itemId}`);
      } else {
        let message = `El objeto con id ${objectId} ha sido reseteado de fábrica.`;
        updateMessageLogger(message, "success");
      }
    } catch (error) {
      console.error("Error al realizar la request:", error);
    }
    // window.location.href = "/Home/";
    // window.location.reload();
  });
