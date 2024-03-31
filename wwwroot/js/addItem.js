/* 
  ADD 
  Pilla los datos del formulario.
  Cambia los necesarios dependiendo del tipo Android/Robot
  Los convierte a JSON y envia una request a  /Home/Create con ellos.

*/

const addObjectForm = document.getElementById("add-object-form");
// EventListener para añadir un item.
addObjectForm.addEventListener("submit", (event) => {
  // Pilla datos
  const name = document.getElementById("name").value;
  const price = document.getElementById("price").value;
  const type =
    addItemModal.dataset.objectType === "robot"
      ? document.getElementById("type-robot").value
      : document.getElementById("type-android").value;
  const category = addItemModal.dataset.objectType === "robot" ? 1 : 2;
  const descCategory =
    addItemModal.dataset.objectType === "robot" ? "Robot" : "Android";

  const data = {
    // JSON entrada
    tableName: descCategory === "Robot" ? "Robots" : "Androids",
    name: name,
    type: type,
    category: category,
    descCategory: descCategory,
    price: parseFloat(price),
  };

  fetch("/Home/Create", {
    // HttpRequest
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  }) // diferenes escenarios
    .then((response) => {
      console.log("Response:", response.status);
      if (response.ok) {
        let message = `${name} ha sido creado en ${tableName}`;
        updateMessageLogger(message, "success");
        return response.json();
      } else {
        let message = `Error al crear ${name} en ${tableName}`;
        updateMessageLogger(message, "error");
        throw new Error("Error al añadir el objeto");
      }
    })
    .then((data) => {
      console.log("Response data:", data);
    })
    .catch((error) => {
      console.error("Error al añadir el objeto:", error);
    });
  // window.location.href = "/Home/";
  // window.location.reload();
});
