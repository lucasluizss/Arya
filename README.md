﻿# 👨🏽‍💻 Arya.API

[![Build Status](https://dev.azure.com/lucasluizss/Arya.API/_apis/build/status/lucasluizss.Arya.API?branchName=master)](https://dev.azure.com/lucasluizss/Arya.API/_build/latest?definitionId=13&branchName=master)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/fab4f7a06ce848f5991460829dfb3d16)](https://www.codacy.com/gh/lucasluizss/Arya/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=lucasluizss/Arya&amp;utm_campaign=Badge_Grade)

[![Quality gate](https://sonarcloud.io/api/project_badges/quality_gate?project=lucasluizss_Arya)](https://sonarcloud.io/dashboard?id=lucasluizss_Arya)

Arya is a restful API developed using .Net Core. In your development was applied principles of DDD and SOLID.

## 🛠 Installation

Use the framework [dotnet core](https://dotnet.microsoft.com/download) to install Arya.

```bash
	dotnet restore
	dotnet build
	dotnet run
```

## 💾 Database configuration 

Using docker:
Run MySQL Server with docker:
```bash
docker run -p 3306:3306 --name mysql2 -e MYSQL_ROOT_PASSWORD=P@ssw0rd -e MYSQL_ROOT_HOST=% -d mysql/mysql-server:latest
```

## 🧾 Usage

After run, you can see the automatic documentation generated by [swagger](https://swagger.io/).

## 📝 Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## 🙋🏽‍♂️ Author
Follow me on twitter: [@lucasluizss](https://twitter.com/lucasluizss/)

## ⚖️  License
[MIT](https://choosealicense.com/licenses/mit/)
