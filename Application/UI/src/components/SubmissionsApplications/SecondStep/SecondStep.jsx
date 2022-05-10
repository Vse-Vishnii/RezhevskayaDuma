import React from 'react';
import ChoiceStep from './../ChoiceStep/ChoiceStep';
import axios from 'axios';

const SecondStep = ({ activeItem, setActiveItem }) => {
  const [districts, setDistricts] = React.useState([]);

  React.useEffect(() => {
    axios.get('http://localhost:5000/district').then(({ data }) => setDistricts(data));
  }, []);

  return (
    <ChoiceStep
      activeItem={activeItem}
      setActiveItem={setActiveItem}
      items={districts}
      choiceText="район"
      numberStep={2}
    />
  );
};

export default SecondStep;
