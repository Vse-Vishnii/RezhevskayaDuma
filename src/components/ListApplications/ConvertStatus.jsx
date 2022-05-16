import React from 'react';

export const GetNameStatus = (status) => {
  if (status == 1) return 'Назначен депутат';
  else if (status == 2) return 'Дан ответ';
  else return 'В процессе';
};

export const GetValueStatus = (status) => {
  if (status.toLowerCase() == 'назначен депутат') return 1;
  else if (status.toLowerCase() == 'дан ответ') return 2;
  else return 0;
};
