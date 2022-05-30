import React from 'react';
import { useSelector } from 'react-redux';

const Popup = (props) => {
  const popupRef = React.useRef();
  const popupInnerRef = React.useRef();
  const currentUser = useSelector((state) => state.user.user);

  const handleOutsideClick = (e) => {
    if (e.path.includes(popupRef.current) && !e.path.includes(popupInnerRef.current)) {
      props.setIsPopupVisible(false);
    }
  };

  React.useEffect(() => {
    document.body.addEventListener('click', handleOutsideClick);
  }, []);

  return (
    <div className="popup" ref={popupRef}>
      <div className={currentUser ? 'popup_inner unuser_popup' : 'popup_inner'} ref={popupInnerRef}>
        {props.children}
        <button className="close_popup" onClick={() => props.setIsPopupVisible(false)}>
          X
        </button>
      </div>
    </div>
  );
};

export default Popup;
