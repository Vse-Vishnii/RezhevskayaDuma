import React from 'react';
import SecondStepInfo from './SecondStepInfo';
import ChoiceStep from './../ChoiceStep/ChoiceStep';

const SecondStep = ({ activeItem, setActiveItem }) => {
  return (
    <ChoiceStep
      activeItem={activeItem}
      setActiveItem={setActiveItem}
      ulList={SecondStepInfo}
      choiceText="район"
      numberStep={2}
    />
  );
};

export default SecondStep;
