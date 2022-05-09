import React from 'react';
import { Link } from 'react-router-dom';

const ChoiceStep = ({ activeItem, setActiveItem, ulList, choiceText, numberStep }) => {
  const getItemClass = (item) => {
    let itemClass = 'button button_option blue';
    if (item === activeItem) itemClass += ' button_option_active';
    return itemClass;
  };

  const getPreviousNextUrl = () => {
    const urls = {
      1: '/first_step',
      2: '/second_step',
      3: '/third_step',
      4: '/fourth_step',
      5: '/gratitude',
    };

    return {
      previousUrl: numberStep > 1 ? urls[numberStep - 1] : '',
      nextUrl: numberStep < 5 ? urls[numberStep + 1] : '',
    };
  };

  return (
    <main>
      <div className="container">
        <div className="step_choose first_step">
          <div className="choose_category">
            <p className="title">Совсем скоро ты сможешь задать вопрос, но сначала</p>
            <p className="choose_cat">Выбери {choiceText}:</p>
            <ul className="list">
              {ulList.map((item, index) => (
                <li
                  key={`${item}_${index}`}
                  className={getItemClass(item)}
                  onClick={() => {
                    setActiveItem(item);
                  }}>
                  {item}
                </li>
              ))}
            </ul>
            <div className="buttons_next_back">
              {numberStep != 1 && (
                <Link
                  to={getPreviousNextUrl()['previousUrl']}
                  className="button button_next_back yellow">
                  Назад
                </Link>
              )}
              {numberStep != 5 && (
                <Link
                  to={getPreviousNextUrl()['nextUrl']}
                  className="button button_next_back yellow">
                  Далее
                </Link>
              )}
            </div>
          </div>
          <div className="hint_block">
            <div className="hint">Подсказка</div>
          </div>
        </div>
      </div>
    </main>
  );
};

export default ChoiceStep;
