import React from 'react';

const Application = ({ application, handlerReadAnswer }) => {
  const getClassStatus = () => {
    let classStatus = 'status';
    switch (application.status.toLowerCase()) {
      case 'в процессе':
        classStatus += ' status_process';
        break;
      case 'дан ответ':
        classStatus += ' status_answered';
        break;
    }
    return classStatus;
  };
  const getShortMessage = () => {
    if (application.message.length < 150) return application.message;
    return `${application.message.slice(0, 150).trim()}...`;
  };

  return (
    <div className="application">
      <div className="application_top">
        <p className="id">{application.id}</p>
        <p className={getClassStatus()}>{application.status}</p>
      </div>
      <div className="title">{application.title}</div>
      <div className="message">{getShortMessage()}</div>
      <button className="button yellow" onClick={() => handlerReadAnswer(application)}>
        Прочитать ответ
      </button>
    </div>
  );
};

export default Application;
