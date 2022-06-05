import React from 'react';
import api from '../../../../api/api';
import { getFormatDate, getShortApplicationId } from '../../UsefulMethods';
import Popup from './../Popup';

const PopupApplicationDeputy = ({ application, setIsPopupVisible, changedStatusApplication }) => {
  const [answer, setAnswer] = React.useState('');

  const getCurrentTime = () => {
    const date = new Date();
    const days = [date.getFullYear(), date.getMonth() + 1, date.getDate()]
      .map(function(x) {
        return x < 10 ? '0' + x : x;
      })
      .join('-');
    const time = [date.getHours(), date.getMinutes(), date.getSeconds()]
      .map(function(x) {
        return x < 10 ? '0' + x : x;
      })
      .join(':');
    return `${days}T${time}.${date.getMilliseconds()}`;
  };

  console.log(getCurrentTime());

  const sendReply = async () => {
    if (answer.trim().length == 0) return;
    try {
      await api.put(`/Application/${application.id}`, {
        ...application,
        answer: {
          ...application.answer,
          name: `Ответ по заявке ${getShortApplicationId(application.id)}`,
          description: answer,
          created: `${getCurrentTime()}`,
          applicationId: application.id,
        },
        status: 2,
      });
      setIsPopupVisible(false);
      changedStatusApplication();
    } catch (error) {
      console.log(error);
    }
  };

  const returnToOperator = async () => {
    try {
      await api.put(`/Application/${application.id}`, {
        ...application,
        status: 0,
      });
      setIsPopupVisible(false);
      changedStatusApplication();
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <Popup setIsPopupVisible={setIsPopupVisible}>
      <p className="id">{`ID-${getShortApplicationId(application.id)}`}</p>
      <p className="title">{application.name}</p>
      <p className="message">{application.description}</p>
      <p className="who_submitted">Житель района, {getFormatDate(application)}</p>
      <textarea
        type="text"
        placeholder="Напишите ваш ответ..."
        onChange={(event) => setAnswer(event.target.value)}
      />
      <div className="buttons_popup_deputy">
        <div className="button send_reply" onClick={sendReply}>
          Отправить ответ
        </div>
        <div className="button return_operator" onClick={returnToOperator}>
          Вернуть оператору
        </div>
      </div>
    </Popup>
  );
};

export default PopupApplicationDeputy;
