import React from 'react';
import ThirdStepInfo from './ThirdStepInfo';
import ChoiceStep from './../ChoiceStep/ChoiceStep';

const ThirdStep = ({ activeItem, setActiveItem }) => {
  return (
    <ChoiceStep
      activeItem={activeItem}
      setActiveItem={setActiveItem}
      ulList={ThirdStepInfo}
      choiceText="депутата"
      numberStep={3}
    />
  );
};

export default ThirdStep;
