import React from "react";
import { getStringDeputy } from "./../ListApplications/UsefulMethods";

const ViewApplication = ({
  activeItem,
  setActiveItem,
  items,
  numberStep,
  setNumberStep,
}) => {
  const getItemClass = (item) => {
    let itemClass = "button button_option blue";
    if (Array.isArray(activeItem)) {
      if (activeItem.find((i) => i.id == item.id))
        itemClass += " button_option_active";
    } else if (item.id == activeItem.id) itemClass += " button_option_active";

    return itemClass;
  };

  const handleClickItem = (item) => {
    if (Array.isArray(activeItem)) {
      if (activeItem.find((i) => i.id == item.id)) {
        setActiveItem(
          activeItem.filter((currentItem) => currentItem.id != item.id)
        );
      } else {
        setActiveItem([...activeItem, item]);
      }
    } else {
      setActiveItem(item);
    }
  };

  const getChoiceText = {
    1: "категорию",
    2: "район",
    3: "депутата",
  };

  const doesCanNextStep = () => {
    if (activeItem) {
      if (!(Array.isArray(activeItem) && activeItem.length == 0)) return true;
    }
    return false;
  };

  return (
    <main>
      <div className="container">
        <div className="step_choose">
          <div className="choose_category">
            <p className="title">
              Совсем скоро ты сможешь задать вопрос, но сначала
            </p>
            <p className="choose_cat">Выбери {getChoiceText[numberStep]}:</p>
            <ul className="list">
              {items.map((item) => (
                <li
                  key={item.id}
                  className={getItemClass(item)}
                  onClick={() => {
                    handleClickItem(item);
                  }}
                >
                  {numberStep !== 3 ? item.name : getStringDeputy(item)}
                </li>
              ))}
            </ul>
            <div className="buttons_next_back">
              {numberStep != 1 && (
                <button
                  className="button button_next_back yellow"
                  onClick={() => setNumberStep(numberStep - 1)}
                >
                  Назад
                </button>
              )}
              <button
                className="button button_next_back yellow"
                onClick={() =>
                  doesCanNextStep() ? setNumberStep(numberStep + 1) : ""
                }
              >
                Далее
              </button>
            </div>
          </div>
          <div className="div_img">
            <img src="img/main_img.jpg" alt="main_img" />
          </div>
        </div>
      </div>
    </main>
  );
};

export default ViewApplication;
