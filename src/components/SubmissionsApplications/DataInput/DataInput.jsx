import React from 'react';
import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import api from '../../../api/api';

const DataInput = ({ activeCategory, activeAreas, activeDeputy, setApplicationId }) => {
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors, isValid },
  } = useForm({
    mode: 'onBlur',
  });

  const onSubmit = async (data) => {
    const user = getUser(data);
    postApplication(user, data.name, data.description);
    navigate('/gratitude');
  };

  const getUser = (data) => {
    const user = {
      firstname: data.firstname,
      surname: data.surname,
      patronymic: data.patronymic,
      email: data.email,
      role: 0,
    };

    return user;
  };

  const postApplication = async (user, name, description) => {
    const application = {
      name,
      description,
      applicant: user,
    };

    try {
      const response = await api({
        method: 'POST',
        url: '/Application/parameters',
        params: {
          caterogyIds: [activeCategory.id],
          districtIds: activeAreas.map((area) => area.id),
          deputyId: activeDeputy.id,
        },
        data: application,
      });
      setApplicationId(response.data.id);
    } catch (error) {
      console.log(error);
    }
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
                name="surname"
                placeholder={errors.surname ? errors.surname.message : 'Фамилия'}
                {...register('surname', {
                  required: 'Вы не ввели фамилию',
                })}
                className={errors.surname ? 'input_error' : ''}
              />
              <input
                type="text"
                name="firstname"
                placeholder={errors.firstname ? errors.firstname.message : 'Имя'}
                {...register('firstname', { required: 'Вы не ввели имя' })}
                className={errors.firstname ? 'input_error' : ''}
              />
              <input
                type="text"
                name="patronymic"
                placeholder={errors.patronymic ? errors.patronymic.message : 'Отчество'}
                {...register('patronymic', {
                  required: 'Вы не ввели отчество',
                })}
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
                name="name"
                placeholder={errors.name ? errors.name.message : 'Тема вопроса'}
                {...register('name', { required: 'Вы не ввели тему' })}
                className={errors.name ? 'input_error' : ''}
              />
              <textarea
                name="description"
                placeholder={
                  errors.description ? errors.description.message : 'Поделитесь, что вас беспокоит?'
                }
                {...register('description', {
                  required: 'Вы не ввели вопрос',
                })}
                className={errors.description ? 'input_error' : ''}
              />
            </div>
            <div>
              <div className="agree_checkbox">
                <input
                  type="checkbox"
                  name="agree"
                  id="agree"
                  {...register('agree', { required: 'Вы не приняли соглашение' })}
                  className={errors.agree ? 'input_error' : ''}
                />
                <label for="agree">
                  <p>Я согласен на обработку персональных данных</p>
                </label>
              </div>
              <button className="button yellow">Отправить запрос</button>
            </div>
          </form>
        </div>
      </div>
    </main>
  );
};

export default DataInput;
