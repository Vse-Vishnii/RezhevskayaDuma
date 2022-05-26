import React from "react";
import { GetValueStatus } from "../UsefulMethods";
import { getStringDeputy } from "./../UsefulMethods";
import { useSelector } from "react-redux";

const Aside = ({ handleFilterButton }) => {
  const statuses = ["В процессе", "Назначен депутат", "Дан ответ"];
  const [activeStatus, setActiveStatus] = React.useState(null);
  const [activeDeputy, setActiveDeputy] = React.useState(null);
  const deputies = useSelector((state) => state.deputies.deputies);

  const handleClickFilter = () => {
    if (activeDeputy || activeStatus) {
      handleFilterButton(
        activeDeputy && activeDeputy.id,
        GetValueStatus(activeStatus)
      );
    } else {
      handleFilterButton();
    }
  };

  return (
    <aside>
      <p className="filter">Статус</p>
      <ul>
        {statuses.map((item) => (
          <li
            className={activeStatus == item ? "active" : ""}
            key={item.name}
            onClick={() => setActiveStatus(item == activeStatus ? null : item)}
          >
            {item}
          </li>
        ))}
      </ul>
      <p className="filter filter_deputy">Депутат</p>
      <ul>
        {deputies.map((deputy) => (
          <li
            className={
              activeDeputy && activeDeputy.id == deputy.id ? "active" : ""
            }
            key={deputy.id}
            onClick={() =>
              setActiveDeputy(
                activeDeputy && activeDeputy.id == deputy.id ? null : deputy
              )
            }
          >
            {getStringDeputy(deputy)}
          </li>
        ))}
      </ul>
      <button className="button blue" onClick={handleClickFilter}>
        Применить
      </button>
    </aside>
  );
};

export default Aside;
