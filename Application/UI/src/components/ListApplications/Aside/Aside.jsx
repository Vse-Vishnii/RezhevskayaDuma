import React from 'react';
import { ListApplicationsInfo } from '../ListApplicationsInfo';

const Aside = ({ handlerFilterButton }) => {
  const [status, getStatus] = React.useState(null);
  const [deputy, getDeputy] = React.useState(null);

  return (
    <aside>
      <p className="filter">Статус</p>
      <ul>
        {ListApplicationsInfo.status.map((item) => (
          <li
            className={status == item ? 'active' : ''}
            key={item}
            onClick={() => getStatus(item == status ? null : item)}>
            {item}
          </li>
        ))}
      </ul>
      <p className="filter filter_deputy">Депутат</p>
      <ul>
        {ListApplicationsInfo.deputies.map((item) => (
          <li
            className={deputy == item ? 'active' : ''}
            key={item}
            onClick={() => getDeputy(item == deputy ? null : item)}>
            {item}
          </li>
        ))}
      </ul>
      <button className="button blue" onClick={() => handlerFilterButton(deputy, status)}>
        Применить
      </button>
    </aside>
  );
};

export default Aside;
