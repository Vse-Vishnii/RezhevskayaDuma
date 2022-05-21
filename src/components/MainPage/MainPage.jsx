import React from "react";
import { Link } from "react-router-dom";

const MainPage = () => {
  return (
    <main>
      <div className="container">
        <div className="main_page">
          <div className="info">
            <h1>
              Текст, побуждающий
              <br /> к действию: задать
              <br /> вопрос
            </h1>
            <p className="info_title">
              Мы поможем найти ответы на все вопросы (здесь будет объяснение что
              это за сервис и зачем вы зашли сюда)
            </p>
            <Link
              to="/submission_application"
              className="button ask_question yellow"
            >
              Хочу задать вопрос
            </Link>
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

export default MainPage;
