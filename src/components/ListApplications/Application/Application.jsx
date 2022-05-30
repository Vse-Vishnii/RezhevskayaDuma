import React from 'react';
import { GetNameStatus, getShortApplicationId } from '../UsefulMethods';
import ButtonApplication from './ButtonApplication/ButtonApplication';
import { useSelector } from 'react-redux';

const Application = ({ application, handleClickButton, textButton }) => {
  const currentUser = useSelector((state) => state.user.user);

  const getClassStatus = () => {
    let classStatus = 'status';
    switch (application.status) {
      case 0:
        classStatus += ' status_process';
        break;
      case 2:
        classStatus += ' status_answered';
        break;
    }
    return classStatus;
  };

  const getShortMessage = () => {
    if (application.description.length < 150) return application.description;
    return `${application.description.slice(0, 150).trim()}...`;
  };

  const clickButton = () => handleClickButton(application);

  return (
    <div className="application">
      <div className="application_top">
        <p className="id">{`ID-${getShortApplicationId(application.id)}`}</p>
        <p className={getClassStatus()}>{GetNameStatus(application.status)}</p>
      </div>
      <div className="title">{application.name}</div>
      <div className="message">{getShortMessage()}</div>
      {application.status != 0 || currentUser ? (
        <ButtonApplication handleClickButton={clickButton} textButton={textButton} />
      ) : (
        ''
      )}
    </div>
  );
};

export default Application;
