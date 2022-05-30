import React from 'react';

const ButtonApplication = ({ handleClickButton, textButton }) => {
  return (
    <button className="button yellow" onClick={() => handleClickButton()}>
      {textButton}
    </button>
  );
};

export default ButtonApplication;
