[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com) [![forthebadge](https://forthebadge.com/images/badges/ctrl-c-ctrl-v.svg)](https://forthebadge.com)

A simple application that allows you to lock the monitor screen using the `/lock` command in the telegram bot.

To start the bot, you need to specify the bot's telegram API key in the `"TelebotKey"` parameter in `appsettings.json`. You can get an API key by registering your bot at [BotFather](https://t.me/botfather).
Next, you need to find out your identifier (in `appsettings.json` parameter `"UserId"`) in Telegram. The easiest way is [Get My ID](https://t.me/getmyid_bot).

Changing the language is done by setting a value for the `"Language"` parameter. Available language settings:
* `ru` - Russian language
* `en` - English

If the `"Language"` parameter is not specified, the default language will be English.

During development, if necessary, you can specify the API key and user ID in the environment variables:
`TelegramSettings__TelebotKey=api-key` `TelegramSettings__UserId=1115542254`