import React from 'react';
import { ListApplicationsInfo } from '../ListApplicationsInfo';
import axios from 'axios';

const Aside = ({ handlerFilterButton }) => {
  const [activeStatus, setActiveStatus] = React.useState(null);
  const [activeDeputy, setActiveDeputy] = React.useState(null);

  const [deputies, setDeputies] = React.useState([]);

  React.useEffect(() => {
    axios.get('http://localhost:5000/user').then(({ data }) => setDeputies(data));
  }, []);

  return (
    <aside>
      <p className="filter">Статус</p>
      <ul>
        {ListApplicationsInfo.status.map((item) => (
          <li
            className={activeStatus == item ? 'active' : ''}
            key={item}
            onClick={() => setActiveStatus(item == activeStatus ? null : item)}>
            {item}
          </li>
        ))}
      </ul>
      <p className="filter filter_deputy">Депутат</p>
      <ul>
        {deputies.map((deputy) => (
          <li
            className={activeDeputy == deputy ? 'active' : ''}
            key={deputy.id}
            onClick={() => setActiveDeputy(deputy == activeDeputy ? null : deputy)}>
            {`${deputy.surname} 
              ${deputy.firstname && deputy.firstname[0]}. 
              ${deputy.patronymic && deputy.patronymic[0]}.`}
          </li>
        ))}
      </ul>
      <button
        className="button blue"
        onClick={() => handlerFilterButton(activeDeputy, activeStatus)}>
        Применить
      </button>
    </aside>
  );
};

export default Aside;
