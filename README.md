# RezhevskayaDuma
Проект “ВЛАСТЬ - ЭТО Я”.

## Актуальность проекта
Упростить получение информации по вопросам местного самоуправления, сделать более доступными обращения жителей к депутатам.
Упор на жителей: максимальная простота, доступность для жителей

## Цель
Создать IT-решение для взаимодействия жителей и депутатов города Реж.

## Словарь данных (глоссарий)
Заявка (обращение) -  это заявление с указанием на потребность в чём-либо, требование на получение чего-либо. 
Заявка имеет обобщенную категорию по проблеме. Например, ЖКХ, дороги и т.п. Чтобы пользователь выбирал категорию заявки.
Заявка имеет статус, например, новая, в работе у депутата, решена.

## Пользователи системы
### Жители города всех возврастов
Требования к функционалу:
Подача заявки: в администрацию РГО, в профильные организации, в компании, которые подключены к системе
Выбор категории заявки. Если нет подходящей категории, выбирает категорию “другое”.
Выбор района (вопрос касательно какого района города)
Выбирает похожий вопрос из предложенных (ранее заданных, частично совпадающих по тексту). Если вопрос другой, отправить заявку все равно.
выбор кому отправляется заявка (специалисту/депутату/отделу/иное). То есть заявка может быть подана без указания получателя.
Может посмотреть статус заявки
Получить ответ по заявке
Проголосовать в опросе.

### Обработчики заявок (специальные сотрудники)
Требования к функционалу:
Обработка заявок. Что могут сделать обработчики заявок: отклонить, направить заявку депутату/отделу (если не была направлена автоматически в нужное место)

### Депутаты
Видит заявки в нашей системе.
Обработка заявок. Что могут сделать депутаты: написать ответ автору заявки (автоматическое сообщение из нашей системы на контакт автора), вернуть заявку обработчику.
Просмотр статистики по заявкам. 

## Требования к функционалу:
аналитика (статистика) по темам (для будущего правильного формирования бюджета, исходя из проблем населения). Статистика показывает, какие категории вопросов часто выбираемые), к кому чаще адресуются, кто сколько вопросов решил.
голосования (например, на какой из парков выделить бюджет. Т.е. надо подсчитывать общую сумму голосов). Еще вопрос .
Список часто задаваемые вопросов и ответы 
Поиск информации с сайта РГО, который будет выдавать бот по ключевым словам.
Новостная лента
История Думы
Фирменный стиль - герб Реж
*без интеграции с текущим сайтом думы

## Предложения по реализации, только приоритетный функционал, в порядке убывания:
Голосование с идентификацией по номеру телефона.
Сайт делаем в первую очередь. Бот во вторую очередь, он дублирует функционал, но не является приоритетным.
Подачу заявки и всё о заявках планируем делать в первую очередь с упором на качество. Остальное, если останется время.
Сайт
Подача заявки.
Голосование.
Если обращение направляется к депутату напрямую, оно дублируется ему на почту/мессенджер (можно через сторонние интеграции сделать переадресацию дополнительную для дублирования заявки)
Список часто задаваемые вопросов
Бот в мессенджере.
Подача заявки.
Голосование.
Список часто задаваемые вопросов

## Стек технологий.
Frontend: React
Backend: C#, .NET Core

## Какие данные вам нужны от заказчика для реализации. 
Пока что только ответы на вопросы в этом документе, все вопросы в комментариях к тексту.
