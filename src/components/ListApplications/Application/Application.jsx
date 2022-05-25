import React from "react";
import { GetNameStatus, getShortApplicationId } from "../UsefulMethods";

const Application = ({ application, handleReadAnswer }) => {
  const getClassStatus = () => {
    let classStatus = "status";
    switch (application.status) {
      case 0:
        classStatus += " status_process";
        break;
      case 2:
        classStatus += " status_answered";
        break;
    }
    return classStatus;
  };

  const getShortMessage = () => {
    if (application.description.length < 150) return application.description;
    return `${application.description.slice(0, 150).trim()}...`;
  };

  return (
    <div className="application">
      <div className="application_top">
        <p className="id">{`ID-${getShortApplicationId(application.id)}`}</p>
        <p className={getClassStatus()}>{GetNameStatus(application.status)}</p>
      </div>
      <div className="title">{application.name}</div>
      <div className="message">{getShortMessage()}</div>
      {application.status == 2 ? (
        <button
          className="button yellow"
          onClick={() => handleReadAnswer(application)}
        >
          Прочитать ответ
        </button>
      ) : (
        ""
      )}
    </div>
  );
};

export default Application;
