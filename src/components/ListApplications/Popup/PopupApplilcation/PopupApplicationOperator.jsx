import React from "react";
import { getShortApplicationId } from "../../UsefulMethods";
import Popup from "./../Popup";
import { getFormatDate, getStringDeputy } from "./../../UsefulMethods";
import api from "../../../../api/api";

const PopupApplicationOperator = ({ application, setIsPopupVisible }) => {
  const [choiceDeputy, setChoiceDeputy] = React.useState(null);
  const [deputies, setDeputies] = React.useState([]);

  React.useEffect(() => {
    api.get(`/User/deputies/filters`).then(({ data }) => {
      setDeputies(data);
    });
    api.get(`/Application/${application.id}`).then(({ data }) => {
      setChoiceDeputy(data.deputy.id);
    });
  }, []);

  return (
    <Popup setIsPopupVisible={setIsPopupVisible}>
      <p className="id">{`ID-${getShortApplicationId(application.id)}`}</p>
      <p className="title">{application.name}</p>
      <p className="message">{application.description}</p>
      <p className="who_submitted">
        Житель района, {getFormatDate(application)}
      </p>
      <p className="appoint_deputy">Назначить депутата:</p>
      <div className="deputies">
        <ul>
          {deputies.map((deputy) => (
            <li
              className={deputy.id == choiceDeputy ? "active" : ""}
              onClick={() => setChoiceDeputy(deputy.id)}
            >
              {getStringDeputy(deputy)}
            </li>
          ))}
        </ul>
      </div>
      <div className="buttons_popup_deputy">
        <div className="button send_reply" onClick={""}>
          Принять
        </div>
        <div className="button return_operator" onClick={""}>
          Отклонить заявку
        </div>
      </div>
    </Popup>
  );
};

export default PopupApplicationOperator;
