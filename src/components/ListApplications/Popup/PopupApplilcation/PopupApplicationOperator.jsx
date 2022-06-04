import React from 'react';
import { getShortApplicationId } from '../../UsefulMethods';
import Popup from './../Popup';
import { getFormatDate, getStringDeputy, GetValueStatus } from './../../UsefulMethods';
import api from '../../../../api/api';
import { useSelector } from 'react-redux';

const PopupApplicationOperator = ({ application, setIsPopupVisible, handleFilterButton }) => {
  const [choiceDeputy, setChoiceDeputy] = React.useState(null);
  const [deputies, setDeputies] = React.useState([]);
  const { activeDeputy, activeStatus } = useSelector((state) => ({
    activeDeputy: state.aside.activeDeputy,
    activeStatus: state.aside.activeStatus,
  }));

  React.useEffect(() => {
    api.get(`/User/deputies/filters`).then(({ data }) => {
      setDeputies(data);
    });
    api.get(`/Application/${application.id}`).then(({ data }) => {
      setChoiceDeputy(data.deputy.id);
    });
  }, []);

  const uploadData = () => {
    if (activeDeputy || activeStatus) {
      handleFilterButton(activeDeputy && activeDeputy.id, GetValueStatus(activeStatus));
    } else {
      handleFilterButton();
    }
  };

  const acceptApplication = async () => {
    const deputyFromServer = api.get(`/User/${application.id}`);
    try {
      await api.put(`/Application/${application.id}`, {
        ...application,
        status: 1,
        deputy: deputyFromServer,
      });
      setIsPopupVisible(false);
      uploadData();
    } catch (error) {
      console.log(error);
    }
  };

  const rejectApplication = () => {};

  return (
    <Popup setIsPopupVisible={setIsPopupVisible}>
      <p className="id">{`ID-${getShortApplicationId(application.id)}`}</p>
      <p className="title">{application.name}</p>
      <p className="message">{application.description}</p>
      <p className="who_submitted">Житель района, {getFormatDate(application)}</p>
      <p className="appoint_deputy">Назначить депутата:</p>
      <div className="deputies">
        <ul>
          {deputies.map((deputy) => (
            <li
              className={deputy.id == choiceDeputy ? 'active' : ''}
              onClick={() => setChoiceDeputy(deputy.id)}>
              {getStringDeputy(deputy)}
            </li>
          ))}
        </ul>
      </div>
      <div className="buttons_popup_operator">
        <div className="button accept_application" onClick={acceptApplication}>
          Принять
        </div>
        <div className="button reject_application" onClick={rejectApplication}>
          Отклонить заявку
        </div>
      </div>
    </Popup>
  );
};

export default PopupApplicationOperator;
