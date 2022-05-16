import React from 'react';
import { ListApplicationsInfo } from '../ListApplicationsInfo';
import api from '../../../api/api';

const Aside = ({ handleFilterButton }) => {
  const [activeStatus, setActiveStatus] = React.useState({});
  const [activeDeputy, setActiveDeputy] = React.useState(null);
  const [deputies, setDeputies] = React.useState([]);

  React.useEffect(() => {
    api.get('/User/deputies/filters').then(({ data }) => setDeputies(data));
  }, []);

  return (
    <aside>
      <p className="filter">Статус</p>
      <ul>
        {ListApplicationsInfo.status.map((item) => (
          <li
            className={activeStatus == item ? 'active' : ''}
            key={item.name}
            onClick={() => setActiveStatus(item == activeStatus ? null : item)}>
            {item}
          </li>
        ))}
      </ul>
      <p className="filter filter_deputy">Депутат</p>
      <ul>
        {deputies.map((deputy) => (
          <li
            className={activeDeputy && activeDeputy.id == deputy.id ? 'active' : ''}
            key={deputy.id}
            onClick={() =>
              setActiveDeputy(activeDeputy && activeDeputy.id == deputy.id ? null : deputy)
            }>
            {`${deputy.surname} 
              ${deputy.firstname && deputy.firstname[0]}. 
              ${deputy.patronymic && deputy.patronymic[0]}.`}
          </li>
        ))}
      </ul>
      <button
        className="button blue"
        onClick={() => handleFilterButton(activeDeputy, activeStatus)}>
        Применить
      </button>
    </aside>
  );
};

export default Aside;
