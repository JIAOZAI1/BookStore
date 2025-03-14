<!-- TOC -->
- [BookStore 在线书籍商店](#bookstore-在线书籍商店)
    - [介绍](#介绍)
    - [功能](#功能)
    - [项目结构](#项目结构)
    - [本地安装](#本地安装)
    - [docker安装](#docker安装)
    - [展示效果](#展示效果)
<!-- /TOC -->
# BookStore 在线书籍商店
### 介绍
 一个基于.net8平台实现的在线书籍商店的项目，使用内存数据库暂存数据，RESTful API的设计风格。

### 功能
* 1.jwt验证
* 2.购物车
* 3.书籍管理

### 项目结构
```
+---Controllers     --控制器层
+---Entities        --数据实体层
+---Models          --视图模型
+---Repository      --DAL
+---Services        --BAL
```

### 本地安装
* git clone [git@github.com:JIAOZAI1/BookStore.git](https://github.com/JIAOZAI1/BookStore.git)
* 运行 cd BookStore & dotnet run --urls=https://localhost:5001
* 浏览器访问：https://localhost:5001/swagger
  

### docker安装
* git clone [git@github.com:JIAOZAI1/BookStore.git](https://github.com/JIAOZAI1/BookStore.git)
* 运行 cd BookStore & docker build -t bookstore:latest . & docker run -p 8080:8080 bookstore
* 浏览器访问：http://localhost:5001/swagger

### 展示效果
![alt text](image.png)