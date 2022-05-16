import React from 'react';
import ChoiceStep from './../ChoiceStep/ChoiceStep';
import api from '../../../api/api';

const ThirdStep = ({ activeItem, setActiveItem }) => {
  const [deputies, setDeputies] = React.useState([]);

  React.useEffect(() => {
    api.get('/User/deputies/filters').then(({ data }) => setDeputies(data));
  }, []);

  return (
    <ChoiceStep
      activeItem={activeItem}
      setActiveItem={setActiveItem}
      items={deputies}
      choiceText="депутата"
      numberStep={3}
    />
  );
};

export default ThirdStep;
