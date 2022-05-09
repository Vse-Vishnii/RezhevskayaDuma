import React from 'react';
import FirstStepInfo from './FirstStepInfo';
import ChoiceStep from './../ChoiceStep/ChoiceStep';

const FirstStep = ({ activeItem, setActiveItem }) => {
  return (
    <ChoiceStep
      activeItem={activeItem}
      setActiveItem={setActiveItem}
      ulList={FirstStepInfo}
      choiceText="категорию"
      numberStep={1}
    />
  );
};

export default FirstStep;
