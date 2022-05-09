import React from 'react';
import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';

const FourthStep = ({ activeCategory, activeArea, activeDeputy }) => {
  const [data, setData] = React.useState({});
  const navigate = useNavigate();

  const setValues = (values) => {
    setData((prevData) => ({
      ...prevData,
      ...values,
    }));
  };

  const {
    register,
    handleSubmit,
    formState: { errors, isValid },
  } = useForm({
    mode: 'onBlur',
  });

  const onSubmit = (data) => {
    setValues(data);
    console.log(activeCategory, activeArea, activeDeputy, data);
    navigate('/gratitude');
  };

  return (
    <main>
      <div className="container">
        <div className="form">
          <form onSubmit={handleSubmit(onSubmit)}>
            <div className="form_left">
              <p>Ваши данные</p>
              <input
                type="text"
                name="lastName"
                placeholder={errors.lastName ? errors.lastName.message : 'Фамилия'}
                {...register('lastName', {
                  required: 'Вы не ввели фамилию',
                })}
                className={errors.lastName ? 'input_error' : ''}
              />
              <input
                type="text"
                name="firstName"
                placeholder={errors.firstName ? errors.firstName.message : 'Имя'}
                {...register('firstName', { required: 'Вы не ввели имя' })}
                className={errors.firstName ? 'input_error' : ''}
              />
              <input
                type="text"
                name="patronymic"
                placeholder={errors.patronymic ? errors.patronymic.message : 'Отчество'}
                {...register('patronymic', { required: 'Вы не ввели отчество' })}
                className={errors.patronymic ? 'input_error' : ''}
              />
              <input
                type="email"
                name="email"
                placeholder={errors.email ? errors.email.message : 'Электронная почта'}
                {...register('email', { required: 'Вы не ввели почту' })}
                className={errors.email ? 'input_error' : ''}
              />
            </div>
            <div className="form_right">
              <p>Тема вопроса</p>
              <input
                type="text"
                name="questionTitle"
                placeholder={errors.questionTitle ? errors.questionTitle.message : 'Тема вопроса'}
                {...register('questionTitle', { required: 'Вы не ввели тему' })}
                className={errors.questionTitle ? 'input_error' : ''}
              />
              <textarea
                name="questionText"
                placeholder={
                  errors.questionText
                    ? errors.questionText.message
                    : 'Поделитесь, что вас беспокоит?'
                }
                {...register('questionText', { required: 'Вы не ввели вопрос' })}
                className={errors.questionText ? 'input_error' : ''}
              />
            </div>
            <button className="button yellow">Отправить запрос</button>
          </form>
        </div>
      </div>
    </main>
  );
};

export default FourthStep;
