import React from "react";
import api from "../../api/api";
import ViewApplication from "./ViewApplication";
import DataInput from "./DataInput/DataInput";
import LoadingSpinner from "../Loading/LoadingSpinner";

const SubmissionApplication = ({ setApplicationId }) => {
  const [numberStep, setNumberStep] = React.useState(1);
  const [isLoadingPage, setIsLoadingPage] = React.useState(true);

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
    api.get("/category").then(({ data }) => {
      setCategories(data);
      setIsLoadingPage(false);
    });
    api.get("/district").then(({ data }) => setAreas(data));
  }, []);

  React.useEffect(() => {
    api
      .get(
        `/User/deputies/filters?categories=${
          activeCategory.id
        }&districts=${activeAreas.map((area) => area.id).join("&districts=")}`
      )
      .then(({ data }) => {
        setDeputies(data);
        console.log(activeAreas.map((activeArea) => activeArea.id));
      });
  }, [numberStep]);

  return (
    <>
      {isLoadingPage ? (
        <LoadingSpinner />
      ) : numberStep != 4 ? (
        <ViewApplication {...getPropertysApplication()} />
      ) : (
        <DataInput {...getPropertysApplication()} />
      )}
    </>
  );
};

export default SubmissionApplication;
