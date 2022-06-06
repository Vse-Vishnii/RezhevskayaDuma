import React from 'react';
import api from '../../../../api/api';
import { getShortApplicationId, getStringDeputy, getFormatDate } from './../../UsefulMethods';
import Popup from '../Popup';

const PopupApplication = ({ application, setIsPopupVisible }) => {
  const [answer, setAnswer] = React.useState(null);

  React.useEffect(() => {
    api.get(`/Answer/${application.id}`).then(({ data }) => {
      setAnswer(data);
    });
  }, []);

  console.log(application);

  return (
    <Popup setIsPopupVisible={setIsPopupVisible}>
      <p className="id">{`ID-${getShortApplicationId(application.id)}`}</p>
      <p className="title">{application.name}</p>
      <p className="message">{application.description}</p>
      <p className="who_submitted">Житель района, {getFormatDate(application)}</p>
      <p className="answer">{answer && answer.description}</p>
      <p className="who_answered">{`${getStringDeputy(application.deputy)}, ${getFormatDate(
        application,
      )}`}</p>
    </Popup>
  );
};

export default PopupApplication;
