import React from 'react';
import { Link } from 'react-router-dom';
import { useSelector } from 'react-redux';

const Header = () => {
  const user = useSelector((state) => state.user.user);
  return (
    <header>
      <div className="header">
        <div className="header_logo_block">
          <Link to="/">
            <img
              src="img/rej_logo.png"
              width="91"
              height="111"
              className="header_logo"
              alt="rej_logo"
            />
          </Link>
        </div>
        <div className="text">
          <p className="header_title">
            Официальный сайт подачи заявок Режевского городского округа
          </p>
          {user && user.role == 2 ? (
            <p className="name_deputy">{`${user.firstName} ${user.lastName}`}</p>
          ) : (
            ''
          )}
        </div>
      </div>
    </header>
  );
};

export default Header;
