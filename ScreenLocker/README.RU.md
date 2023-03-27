[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com) [![forthebadge](https://forthebadge.com/images/badges/ctrl-c-ctrl-v.svg)](https://forthebadge.com)

Простое приложение, которое позволяет при помощи команды `/lock` в телеграм-боте заблокировать экран монитора.

Для запуска бота необходимо в `appsettings.json` в параметр `"TelegramSettings.TelebotKey"` указать API-ключ телеграм бота. API-ключ можно получить, зарегистрировав своего бота в [BotFather](https://t.me/botfather). 
Далее необходимо узнать свой идентификатор (в `appsettings.json` параметр `"TelegramSettings.UserId"`) в Telegram. Самый простой способ - [Get My ID](https://t.me/getmyid_bot).

Смена языка осуществляется выставлением значения для параметра `"Language"`. Доступные языковые настройки:
* `ru` - русский язык
* `en` - английский язык

Если параметр `"Language"` не задан, то по умолчанию будет использоваться английский язык.

В период разработки, при необходимости, можно указать в переменных окружения API-ключ и идентификатор пользователя:
`TelegramSettings__TelebotKey=api-key` `TelegramSettings__UserId=1115542254`