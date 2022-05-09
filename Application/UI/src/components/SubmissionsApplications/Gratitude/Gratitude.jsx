import React from 'react';
import { Link } from 'react-router-dom';

const Gratitude = () => {
  return (
    <main>
      <div className="container">
        <div className="gratitude_page">
          <div className="info">
            <h1>Спасибо за вашу заявку</h1>
            <p className="info_title">
              Депутат как можно скорее изучит ваш вопрос и поможет вам!
              <br />
              Ожидайте уведомления на почте.
            </p>
            <p className="info_title info_title_two">
              А пока что можете ознакомиться с другими вопросами, которые депутаты уже решили.
            </p>
            <Link to="/list_applications" className="button view_applications">
              Хочу посмотреть все заявки
            </Link>
          </div>
          <div className="div_img">
            <img src="img/main_img.jpg" alt="main_img" />
          </div>
        </div>
      </div>
    </main>
  );
};

export default Gratitude;
