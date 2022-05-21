import React from "react";
import api from "../../api/api";
import ViewApplication from "./ViewApplication";
import DataInput from "./DataInput/DataInput";

const SubmissionApplication = ({ setApplicationId }) => {
  const [numberStep, setNumberStep] = React.useState(1);

  const [categories, setCategories] = React.useState([]);
  const [areas, setAreas] = React.useState([]);
  const [deputies, setDeputies] = React.useState([]);

  const [activeCategory, setActiveCategory] = React.useState("");
  const [activeAreas, setActiveAreas] = React.useState([]);
  const [activeDeputy, setActiveDeputy] = React.useState("");

  const getPropertysApplication = () => {
    const obj = { numberStep, setNumberStep };
    switch (numberStep) {
      case 1:
        return {
          ...obj,
          activeItem: activeCategory,
          setActiveItem: setActiveCategory,
          items: categories,
        };
      case 2:
        return {
          ...obj,
          activeItem: activeAreas,
          setActiveItem: setActiveAreas,
          items: areas,
        };
      case 3:
        return {
          ...obj,
          activeItem: activeDeputy,
          setActiveItem: setActiveDeputy,
          items: deputies,
        };
      case 4:
        return {
          activeCategory,
          activeAreas,
          activeDeputy,
          setApplicationId,
        };
    }
  };

  React.useEffect(() => {
    api.get("/category").then(({ data }) => setCategories(data));
    api.get("/district").then(({ data }) => setAreas(data));
    api.get("/User/deputies/filters").then(({ data }) => setDeputies(data));
  }, []);

  return (
    <>
      {numberStep != 4 ? (
        <ViewApplication {...getPropertysApplication()} />
      ) : (
        <DataInput {...getPropertysApplication()} />
      )}
    </>
  );
};

export default SubmissionApplication;
