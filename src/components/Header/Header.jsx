import React from "react";
import { Link } from "react-router-dom";

const Header = () => {
  return (
    <header>
      <div className="header">
        <div className="header_logo_block">
          <Link to="/">
            <img
              src="img/rej_logo.svg"
              width="91"
              height="111"
              className="header_logo"
              alt="rej_logo"
            />
          </Link>
        </div>
        <p className="header_title">
          Официальный сайт подачи заявок Режевского городского округа
        </p>
      </div>
    </header>
  );
};

export default Header;
