import React from "react";

export const GetNameStatus = (status) => {
  if (!isNaN(status)) {
    switch (status) {
      case 0:
        return "В процессе";
      case 1:
        return "Назначен депутат";
      case 2:
        return "Дан ответ";
    }
  }
};

export const GetValueStatus = (status) => {
  if (status) {
    switch (status.toLowerCase()) {
      case "в процессе":
        return 0;
      case "назначен депутат":
        return 1;
      case "дан ответ":
        return 2;
    }
  }
};

export const getStringDeputy = (deputy) => {
  return `${deputy.surname} 
  ${deputy.firstname && deputy.firstname[0]}. 
  ${deputy.patronymic && deputy.patronymic[0]}.`;
};
