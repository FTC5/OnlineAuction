# -Online-Auction
Не используйте команду преобразование по отношению к даним с удаленого репозитория(git rebase <название>).
--------------------------------------------------------------------
Нужно использовать только git merege для слияния веток (git merege <название>)

Лучше виполнять две команды при переносе даних из репозиторя git fetch <название> и git merge <назвение> Чем команду git pull <название>
Но можно и git pull

В ветку integration будем проводить слияния, если все будет с табильно переносим даных проводим слияния в ветку develop и только стабильные варианты в ветку master
