import React from "react";
import { GetValueStatus } from "../UsefulMethods";
import { getStringDeputy } from "./../UsefulMethods";
import { useSelector } from "react-redux";

const Aside = ({ handleFilterButton }) => {
  const statusesCitizen = ["В очереди", "Назначен депутат", "Дан ответ"];
  const statusesOperator = ["Новые заявки", "Назначен депутат", "Дан ответ"];
  const [activeStatus, setActiveStatus] = React.useState(null);
  const [activeDeputy, setActiveDeputy] = React.useState(null);
  const deputies = useSelector((state) => state.deputies.deputies);
  const currentUser = useSelector((state) => state.user.user);

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

  const getStatusesLi = (array) => {
    return array.map((item) => (
      <li
        className={getClassStatuesLi(item)}
        key={item.name}
        onClick={() => setActiveStatus(item == activeStatus ? null : item)}
      >
        {item}
      </li>
    ));
  };

  const getClassStatuesLi = (li) => {
    let classLi = "";
    if (activeStatus == li) classLi += "active";
    if (li.toLowerCase() == "новые заявки") classLi += " new_applications";
    return classLi;
  };

  return (
    <aside>
      <p className="filter">Статус</p>
      <ul>
        {getStatusesLi(
          currentUser && currentUser.role == 1
            ? statusesOperator
            : statusesCitizen
        )}
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
