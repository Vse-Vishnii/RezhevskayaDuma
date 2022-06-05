import React from 'react';
import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import api from '../../api/api';
import { useDispatch, useSelector } from 'react-redux';
import { setUser, clearUser } from '../../store/userSlice';
import Popup from './../ListApplications/Popup/Popup';

const Login = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const user = useSelector((state) => state.user.user);
  const [isPopupVisible, setIsPopupVisible] = React.useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors, isValid },
  } = useForm({
    mode: 'onBlur',
  });

  const handleLogout = () => {
    dispatch(clearUser());
    localStorage.clear();
  };

  const onSubmit = async (data) => {
    const user = { username: data.email, password: data.password };
    try {
      await api({
        method: 'POST',
        url: '/User/authenticate',
        data: user,
      }).then((data) => {
        const responseFirst = data.data;
        if (data.status == 200) {
          api.get(`/User/${responseFirst.id}`).then(({ data }) => {
            dispatch(setUser({ ...responseFirst, role: data.role }));
            localStorage.setItem('user', JSON.stringify({ ...responseFirst, role: data.role }));
            navigate('/list_applications');
          });
        }
      });
    } catch (error) {
      setIsPopupVisible(true);
    }
  };

  return (
    <div className="container_login">
      {!user ? (
        <>
          <img
            src="img/rej_logo.png"
            width="194"
            height="227"
            className="login_logo"
            alt="rej_logo"
          />
          <p className="login_title">Вход для сотрудников Думы</p>
          <div className="login_form">
            <form onSubmit={handleSubmit(onSubmit)}>
              <label className="login_email_text" for="login_email">
                E-mail
              </label>
              <input
                type="email"
                name="email"
                id="login_email"
                {...register('email', { required: 'Вы не ввели почту' })}
                className={errors.email ? 'login_input input_error' : 'login_input'}
              />
              <label className="login_password_text" for="login_password">
                Пароль
              </label>
              <input
                type="password"
                name="password"
                id="login_password"
                {...register('password', { required: 'Вы не ввели пароль' })}
                className={errors.password ? 'login_input input_error' : 'login_input'}
              />
              <div className="remember_forget_container">
                <div className="div_remember_me">
                  <input
                    type="checkbox"
                    name="remember_me"
                    id="remember_me"
                    {...register('remember_me')}
                    className={errors.remember_me ? 'input_error' : ''}
                  />
                  <label for="remember_me">Запомнить меня</label>
                </div>
                <div className="forget_password">
                  <a href="#">Забыли пароль?</a>
                </div>
              </div>
              <div className="to_main_login_container">
                <a href="#" className="button yellow">
                  На главную
                </a>
                <button className="button blue">Войти</button>
              </div>
            </form>
          </div>
          {isPopupVisible ? (
            <Popup setIsPopupVisible={setIsPopupVisible}>Неправильный логин или пароль</Popup>
          ) : (
            ''
          )}
        </>
      ) : (
        <div className="logout_div">
          <p>
            Вы успешно авторизованы, <br /> {user.firstName} {user.lastName}
          </p>
          <button onClick={handleLogout} className="button yellow">
            Выйти из профиля
          </button>
        </div>
      )}
    </div>
  );
};

export default Login;
