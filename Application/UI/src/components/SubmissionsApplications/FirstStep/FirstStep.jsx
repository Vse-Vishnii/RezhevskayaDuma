import React from "react";
import FirstStepInfo from "./FirstStepInfo";
import ChoiceStep from "./../ChoiceStep/ChoiceStep";
import axios from "axios";

const FirstStep = ({ activeItem, setActiveItem }) => {
  const [categories, setCategories] = React.useState([]);

  React.useEffect(() => {
    axios
      .get("http://localhost:5000/category")
      .then(({ data }) => setCategories(data));
  }, []);

  return (
    <ChoiceStep
      activeItem={activeItem}
      setActiveItem={setActiveItem}
      items={categories}
      choiceText="категорию"
      numberStep={1}
    />
  );
};

export default FirstStep;
