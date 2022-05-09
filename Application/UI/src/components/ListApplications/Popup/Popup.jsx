import React from 'react';

const Popup = ({ currentApplicationPopup, setIsPopupVisible }) => {
  const popupRef = React.useRef();
  const popupInnerRef = React.useRef();

  const handleOutsideClick = (e) => {
    if (e.path.includes(popupRef.current) && !e.path.includes(popupInnerRef.current)) {
      setIsPopupVisible(false);
    }
  };

  React.useEffect(() => {
    document.body.addEventListener('click', handleOutsideClick);
  }, []);

  return (
    <div className="popup" ref={popupRef}>
      <div className="popup_inner" ref={popupInnerRef}>
        <p className="id">{currentApplicationPopup.id}</p>
        <p className="title">{currentApplicationPopup.title}</p>
        <p className="message">{currentApplicationPopup.message}</p>
        <p className="who_submitted">Житель района, {currentApplicationPopup.dateApplication}</p>
        <p className="answer">{currentApplicationPopup.answer}</p>
        <p className="who_answered">
          {`${currentApplicationPopup.deputy} ${currentApplicationPopup.dateAnswer}`}
        </p>
        <button className="close_popup" onClick={() => setIsPopupVisible(false)}>
          X
        </button>
      </div>
    </div>
  );
};

export default Popup;
