// Da el nombre para el roboty el androide

let suggestName = (type) => {
  let name = "";
  const letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  const numbers = "0123456789";
  let nameLength;

  switch (type) {
    case "robot":
      nameLength = 6;
      for (let i = 0; i < nameLength; i++) {
        if (i < 2) {
          name += letters.charAt(Math.floor(Math.random() * letters.length));
        } else if (i === 2) {
          name += "-";
        } else {
          name += numbers.charAt(Math.floor(Math.random() * numbers.length));
        }
      }
      return name;

    case "android":
      nameLength = 9;
      for (let i = 0; i < nameLength; i++) {
        if (i < 4) {
          name += letters.charAt(Math.floor(Math.random() * letters.length));
        } else if (i === 4) {
          name += "-";
        } else {
          name += numbers.charAt(Math.floor(Math.random() * numbers.length));
        }
      }
      return name;

    default:
      return "";
  }
};
