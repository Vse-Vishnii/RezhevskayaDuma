import React from "react";
import ThirdStepInfo from "./ThirdStepInfo";
import ChoiceStep from "./../ChoiceStep/ChoiceStep";
import axios from "axios";

const ThirdStep = ({ activeItem, setActiveItem }) => {
  const [deputies, setDeputies] = React.useState([]);

  React.useEffect(() => {
    axios
      .get("http://localhost:5000/user")
      .then(({ data }) => setDeputies(data));
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