import React from "react";
import api from "../../../api/api";
import { getShortApplicationId, getStringDeputy } from "./../UsefulMethods";

const Popup = ({ application, setIsPopupVisible }) => {
  const popupRef = React.useRef();
  const popupInnerRef = React.useRef();
  const [answer, setAnswer] = React.useState(null);

  const handleOutsideClick = (e) => {
    if (
      e.path.includes(popupRef.current) &&
      !e.path.includes(popupInnerRef.current)
    ) {
      setIsPopupVisible(false);
    }
  };

  React.useEffect(() => {
    document.body.addEventListener("click", handleOutsideClick);
    api.get("/Answer").then(({ data }) => {
      setAnswer(data.find((answer) => answer.applicationId == application.id));
    });
  }, []);

  return (
    <div className="popup" ref={popupRef}>
      <div className="popup_inner" ref={popupInnerRef}>
        <p className="id">{`ID-${getShortApplicationId(application.id)}`}</p>
        <p className="title">{application.name}</p>
        <p className="message">{application.description}</p>
        <p className="who_submitted">Житель района data</p>
        <p className="answer">{answer && answer.description}</p>
        <p className="who_answered">{`${getStringDeputy(
          application.deputy
        )} data`}</p>
        <button
          className="close_popup"
          onClick={() => setIsPopupVisible(false)}
        >
          X
        </button>
      </div>
    </div>
  );
};

export default Popup;
