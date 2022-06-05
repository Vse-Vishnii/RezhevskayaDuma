import React from 'react';

export const GetNameStatus = (status) => {
  if (!isNaN(status)) {
    switch (status) {
      case 0:
        return 'В очереди';
      case 1:
        return 'Назначен депутат';
      case 2:
        return 'Дан ответ';
    }
  }
};

export const GetValueStatus = (status) => {
  if (status) {
    switch (status.toLowerCase()) {
      case 'в очереди':
      case 'новые заявки':
        return 0;
      case 'назначен депутат':
        return 1;
      case 'дан ответ':
        return 2;
    }
  }
};

export const getStringDeputy = (deputy) => {
  return `${deputy.surname && deputy.surname} 
  ${deputy.firstname && deputy.firstname[0]}. 
  ${deputy.patronymic && deputy.patronymic[0]}.`;
};

export const getShortApplicationId = (applicationId) => {
  return applicationId ? applicationId.split('-')[0].toUpperCase() : '';
};

export const getFormatDate = (application) => {
  const [year, month, day] = application.created.substr(0, 10).split('-');
  return `${day}.${month}.${year}`;
};

export const sortByTextApplications = (applications, text) => {
  text = text.toLowerCase();
  return applications.filter(
    (application) =>
      application.id.toLowerCase().includes(text) || application.name.toLowerCase().includes(text),
  );
};
